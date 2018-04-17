using System;
using System.Diagnostics;
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
      const string TIME_FORMAT = "000:00:00";

      private static string[] timeSeparators = { ":" };
      private bool correction = false;

      private TimeSpan timeSpan = new TimeSpan();
      private Stopwatch stopwatch = new Stopwatch();

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

         buttonStartStop.Enabled = false;
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
         var mainMenu = new MainMenu();
         var topMenuItemFile = new MenuItem("Файл");
         var menuItemNew = new MenuItem("Нова програма");
         var menuItemExit = new MenuItem("Вихід");

         topMenuItemFile.MenuItems.Add(menuItemNew);
         topMenuItemFile.MenuItems.Add(menuItemExit);
         mainMenu.MenuItems.Add(topMenuItemFile);

         menuItemNew.Click += new EventHandler(menuItemNew_Click);
         menuItemExit.Click += new EventHandler(menuItemExit_Click);

         Menu = mainMenu;
      }

      private void menuItemNew_Click(object sender, EventArgs e)
      {
         Stop();
         buttonAddNewProgram_Click(this, EventArgs.Empty);
      }

      private void menuItemExit_Click(object sender, EventArgs e)
      {
         Stop();
         Close();
      }
      #endregion

      #region Shutdown events
      private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
      {
         Stop();
      }

      private void SystemEvents_Power(object sender, SessionEndedEventArgs e)
      {
         Stop();
      }

      private void TimerForm_FormClosing(object sender, FormClosingEventArgs e)
      {
         if ((comboBoxListOfPrograms.SelectedItem != null) && !correction) {
            Stop();
         }
      }
      #endregion
      #endregion

      #region Work with xml
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
      #endregion

      #region Button events
      private void buttonStart_Click(object sender, EventArgs e)
      {
         if (buttonStartStop.Text == "Старт") {
            buttonStartStop.Text = "Стоп";
            Start();
         } else {
            buttonStartStop.Text = "Старт";
            Stop();
         }
      }

      private void buttonCorrectTime_Click(object sender, EventArgs e)
      {
         if (!correction) {
            if (new ConfirmForm().ShowDialog() == DialogResult.Yes) {
               correction = true;
               EnableManualCorrection();
            }
         } else if (CheckTimeFormat()) {
            correction = false;
            UpdateProgramTime(comboBoxListOfPrograms.SelectedItem.ToString(), textBoxTimer.Text);
            EnableManualCorrection();
         } else {
            MessageBox.Show("Введіть коректний час (Формат \"XXX: XX:XX\")", "Увага!");
         }
      }

      private void buttonAddNewProgram_Click(object sender, EventArgs e)
      {
         var addNewProgramForm = new AddNewProgramForm();

         if (addNewProgramForm.ShowDialog(this) == DialogResult.OK) {
            var newProgramName = addNewProgramForm.programName;

            if (!string.IsNullOrEmpty(newProgramName)
               && !comboBoxListOfPrograms.Items.Contains(newProgramName)) {
               comboBoxListOfPrograms.Items.Add(newProgramName);
               comboBoxListOfPrograms.SelectedItem = newProgramName;
               UpdateXmlFile(newProgramName);
            }
         }
         addNewProgramForm.Dispose();
      }
      #endregion

      private void comboBoxListOfPrograms_SelectedIndexChanged(object sender, EventArgs e)
      {
         var xmlDoc = new XmlDocument();
         xmlDoc.Load(SETUP_FILE);

         foreach (XmlNode node in xmlDoc.SelectSingleNode($@"/{mainNode}/{programsNode}").ChildNodes) {
            if (node.InnerText == comboBoxListOfPrograms.SelectedItem.ToString()) {
               var time = node.Attributes[timeAttr].InnerText.Split(timeSeparators, StringSplitOptions.None);
               var hours = Convert.ToInt32(time[0]);
               var minutes = Convert.ToInt32(time[1]);
               var seconds = Convert.ToInt32(time[2]);

               timeSpan = new TimeSpan(hours, minutes, seconds);
               UpdateTimerTextBox(timeSpan);
               break;
            }
         }

         buttonStartStop.Enabled = true;
         buttonCorrectTime.Enabled = true;
      }

      private void Start()
      {
         stopwatch.Reset();
         stopwatch.Start();

         timer.Enabled = true;
         textBoxTimer.Enabled
            = buttonCorrectTime.Enabled
            = buttonAddNewProgram.Enabled
            = comboBoxListOfPrograms.Enabled
            = false;
      }

      private void Stop()
      {
         timeSpan += stopwatch.Elapsed;
         stopwatch.Stop();

         buttonCorrectTime.Enabled
            = buttonAddNewProgram.Enabled
            = comboBoxListOfPrograms.Enabled
            = true;
         timer.Enabled
            = false;

         UpdateProgramTime(comboBoxListOfPrograms.SelectedItem.ToString(), textBoxTimer.Text);
      }

      private void timer_Tick(object Sender, EventArgs e)
      {
         UpdateTimerTextBox(timeSpan.Add(stopwatch.Elapsed));
      }

      private void UpdateTimerTextBox(TimeSpan time)
      {
         textBoxTimer.Text = $"{time.Hours:000}:{time.Minutes:00}:{time.Seconds:00}";
      }

      private bool CheckTimeFormat()
      {
         var time = textBoxTimer.Text.Split(timeSeparators, StringSplitOptions.None);
         try {
            var hours = Convert.ToInt32(time[0]);
            var minutes = Convert.ToInt32(time[1]);
            var seconds = Convert.ToInt32(time[2]);

            return seconds >= 0
               && seconds <= 59
               && minutes >= 0
               && minutes <= 59
               && hours >= 0
               && hours <= 999;
         }
         catch {
            return false;
         }
      }

      private void EnableManualCorrection()
      {
         textBoxTimer.Enabled = correction;
         buttonStartStop.Enabled
            = buttonAddNewProgram.Enabled
            = comboBoxListOfPrograms.Enabled
            = !correction;
         buttonCorrectTime.Text = (correction) ? "Записати" : "Скорегувати час";
      }
   }
}