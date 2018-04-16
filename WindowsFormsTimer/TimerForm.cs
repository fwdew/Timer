using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Win32;

namespace WindowsFormsTimer
{
   public partial class TimerForm : Form
   {
      const string SETUP_FILE = "setup.xml";
      const string mainNode = "main";
      const string programsNode = "programs";
      const string programNode = "program";
      const string timeAttr = "time";
      const string TIME_SEPARATOR = ":";
      const string TIME_FORMAT = "000:00:00";

      private static string[] time_separators = { TIME_SEPARATOR };
      private bool correction = false;

      #region Constructors
      public TimerForm()
      {
         InitializeComponent();
         ConfigureForm();
      }

      private void ConfigureForm()
      {
         PopulateProgramList();

         CreateMenu();

         buttonStart.Enabled = false;
         buttonStop.Enabled = false;
         buttonCorrectTime.Enabled = false;

         // update stop events
         SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged; // to intercept hibernate event
         SystemEvents.SessionEnded += SystemEvents_Power; // to intercept shutdown event
      }

      private void PopulateProgramList()
      {
         if (!File.Exists(SETUP_FILE)) {
            CreateBlankConfigurationFile();
         }
         GetProgramList();
      }

      private void CreateBlankConfigurationFile()
      {
         /*
         Create empty file with following structure
         <?xml version=1.0 encoding=utf-8?>
         <main>
            <programs>
            </programs>
         </main>
         */
         using (var xmlWriter = XmlWriter.Create(SETUP_FILE)) {
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement(mainNode);

            xmlWriter.WriteStartElement(programsNode);
            xmlWriter.WriteString("");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndDocument();
         }
      }

      private void GetProgramList()
      {
         var xmlDoc = new XmlDocument();
         xmlDoc.Load(SETUP_FILE);

         comboBoxListOfPrograms.Items.Clear();

         // read data from each node "program" and fill combobox
         foreach (XmlNode node in xmlDoc.SelectSingleNode($@"/{mainNode}/{programsNode}").ChildNodes) {
            var program_name = node.InnerText;
            comboBoxListOfPrograms.Items.Add(program_name);
         }
      }

      #region Menu
      private void CreateMenu()
      {
         /* Create menu items in current structure.
         Menu
            File
               New
               Exit
            Help
               About
         */

         var mainMenu = new MainMenu();
         var topMenuItemFile = new MenuItem();
         var menuItemNew = new MenuItem();
         var menuItemExit = new MenuItem();
         var topMenuItemHelp = new MenuItem();
         var menuItemAbout = new MenuItem();
         // TODO:
         var menuItemAbout1 = new MenuItem("someText");

         topMenuItemFile.Text = "&Файл";
         menuItemNew.Text = "&Нова";
         menuItemExit.Text = "В&ихід";

         topMenuItemHelp.Text = "&Допомога";
         menuItemAbout.Text = "&Про програму";

         topMenuItemFile.MenuItems.Add(menuItemNew);
         topMenuItemFile.MenuItems.Add(menuItemExit);
         mainMenu.MenuItems.Add(topMenuItemFile);

         topMenuItemHelp.MenuItems.Add(menuItemAbout);
         topMenuItemHelp.MenuItems.Add(menuItemAbout1);
         mainMenu.MenuItems.Add(topMenuItemHelp);

         menuItemNew.Click += new EventHandler(menuItemNew_Click);
         menuItemExit.Click += new EventHandler(menuItemExit_Click);
         menuItemAbout.Click += new EventHandler(menuItemAbout_Click);

         Menu = mainMenu;
      }

      private void menuItemNew_Click(object sender, EventArgs e)
      {
         StopRunningTimer(this, EventArgs.Empty);
         buttonAddNewProgram_Click(this, EventArgs.Empty);
      }
      private void menuItemExit_Click(object sender, EventArgs e)
      {
         StopRunningTimer(this, EventArgs.Empty);
         Close();
      }
      private void menuItemAbout_Click(object sender, EventArgs e)
      {
         new AboutBoxForm().ShowDialog();
      }
      #endregion

      #region Events
      private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
      {
         Stop();
      }

      private void SystemEvents_Power(object sender, SessionEndedEventArgs e)
      {
         Stop();
      }
      #endregion
      #endregion

      // TODO:
      private void SetUkrainianLanguage()
      {
         buttonCorrectTime.Text = (!correction) ? "Скорегувати час" : "Записати";
      }

      private void StopRunningTimer(object sender, EventArgs e)
      {
         if ((comboBoxListOfPrograms.SelectedItem != null) && !correction) {
            Stop();
         }
      }

      private void buttonStart_Click(object sender, EventArgs e)
      {
         timer.Enabled = true;
         buttonStart.Enabled = false;
         buttonStop.Enabled = true;
         buttonCorrectTime.Enabled = false;
         buttonAddNewProgram.Enabled = false;
         comboBoxListOfPrograms.Enabled = false;
      }

      // TODO:
      private void timer_Tick(object Sender, EventArgs e)
      {
         var time = textBoxTimer.Text.Split(time_separators, StringSplitOptions.None);
         var seconds = Convert.ToInt32(time[2]) + 1;
         var minutes = Convert.ToInt32(time[1]);
         var hours = Convert.ToInt32(time[0]);

         if (seconds == 60) {
            seconds = 0;
            minutes += 1;
            if (minutes == 60) {
               minutes = 0;
               hours += 1;
            }
         }

         textBoxTimer.Text = hours.ToString("000") + TIME_SEPARATOR + minutes.ToString("00") + TIME_SEPARATOR + seconds.ToString("00");
      }

      private void buttonAddNewProgram_Click(object sender, EventArgs e)
      {
         var addNewProgramForm = new AddNewProgramForm();
         var new_program_name = "";

         if (addNewProgramForm.ShowDialog(this) == DialogResult.OK) {
            new_program_name = addNewProgramForm.programName;
         }

         addNewProgramForm.Dispose();

         if (new_program_name != "") {
            if (!comboBoxListOfPrograms.Items.Contains(new_program_name)) {
               comboBoxListOfPrograms.Items.Add(new_program_name);
               UpdateXmlFile(new_program_name);
               comboBoxListOfPrograms.SelectedItem = new_program_name;
            }
         }
      }

      private static void UpdateXmlFile(string programName)
      {
         var xmlDoc = new XmlDocument();
         xmlDoc.Load(SETUP_FILE);

         var programs = xmlDoc.SelectSingleNode($@"/{mainNode}/{programsNode}");

         var program = xmlDoc.CreateElement(programNode);
         program.InnerText = programName;
         program.SetAttribute(timeAttr, TIME_FORMAT);
         programs.AppendChild(program);

         xmlDoc.Save(SETUP_FILE);
      }

      private void buttonStop_Click(object sender, EventArgs e)
      {
         Stop();
      }

      // TODO: Rename
      private void Stop()
      {
         buttonStart.Enabled
            = buttonCorrectTime.Enabled
            = buttonAddNewProgram.Enabled
            = comboBoxListOfPrograms.Enabled
            = true;
         buttonStop.Enabled
            = timer.Enabled
            = false;

         UpdateProgramTime(comboBoxListOfPrograms.SelectedItem.ToString(), textBoxTimer.Text);
      }

      private void UpdateProgramTime(string programName, string time)
      {
         var xmlDoc = new XmlDocument();
         xmlDoc.Load(SETUP_FILE);

         foreach (XmlNode node in xmlDoc.SelectSingleNode($@"/{mainNode}/{programsNode}").ChildNodes) {
            if (node.InnerText == programName) {
               node.Attributes[timeAttr].InnerText = time;
               break;
            }
         }

         xmlDoc.Save(SETUP_FILE);
      }

      private void buttonCorrectTime_Click(object sender, EventArgs e)
      {
         if (!correction) {
            if (new ConfirmForm().ShowDialog() == DialogResult.Yes) {
               correction = true;
               EnableDisableButtons();
            }
         } else if (CorrectTime()) {
            correction = false;
            UpdateProgramTime(comboBoxListOfPrograms.SelectedItem.ToString(), textBoxTimer.Text);
            EnableDisableButtons();
         } else {
            MessageBox.Show("Введіть коректний час (Формат \"XXX: XX:XX\")", "Увага!");
         }
      }

      // to correct time manually
      private bool CorrectTime()
      {
         var time = textBoxTimer.Text.Split(time_separators, StringSplitOptions.None);
         try {
            var seconds = Convert.ToInt32(time[2]);
            var minutes = Convert.ToInt32(time[1]);
            var hours = Convert.ToInt32(time[0]);

            return seconds >= 0
               && seconds <= 59
               && minutes >= 0
               && minutes <= 59
               && hours >= 0
               && hours <= 999;
         }
         catch (Exception) {
            return false;
         }
      }

      private void comboBoxListOfPrograms_SelectedIndexChanged(object sender, EventArgs e)
      {
         var xmlDoc = new XmlDocument();
         xmlDoc.Load(SETUP_FILE);

         foreach (XmlNode node in xmlDoc.SelectSingleNode($@"/{mainNode}/{programsNode}").ChildNodes) {
            if (node.InnerText == comboBoxListOfPrograms.SelectedItem.ToString()) {
               textBoxTimer.Text = node.Attributes[timeAttr].InnerText;
               break;
            }
         }

         buttonStart.Enabled = true;
         buttonCorrectTime.Enabled = true;
      }

      private void EnableDisableButtons()
      {
         if (correction) {
            textBoxTimer.Enabled = true;
            buttonStart.Enabled = false;
            buttonAddNewProgram.Enabled = false;
            comboBoxListOfPrograms.Enabled = false;
         } else {
            textBoxTimer.Enabled = false;
            buttonStart.Enabled = true;
            buttonAddNewProgram.Enabled = true;
            comboBoxListOfPrograms.Enabled = true;
         }
      }

      // TODO:
      private void button1_Click(object sender, EventArgs e)
      {
         foreach (var item in Menu.MenuItems) {
            var mItem = (MenuItem)item;
            MessageBox.Show(mItem.Text);
            if (mItem.Text == "&Help") {
               mItem.Text = "111";
            }
         }
      }
   }
}