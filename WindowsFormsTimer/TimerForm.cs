using System;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Win32;

namespace WindowsFormsTimer
{
   public enum LanguageType
   {
      EN,
      UA
   }

   public partial class TimerForm : Form
   {
      private const string SETUP_FILE = "setup_timers.xml";
      private const string TIME_SEPARATOR = ":";
      private static string[] time_separators = { TIME_SEPARATOR };
      private bool correction = false;
      private LanguageType currentLanguage = LanguageType.EN;

      #region Constructors
      public TimerForm()
      {
         InitializeComponent();
         ConfigureForm();

         // update stop events
         SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged; // to intercept hibernate event
         SystemEvents.SessionEnded += SystemEvents_Power; // to intercept shutdown event
      }

      private void ConfigureForm()
      {

         ReadingConfigurationFromXml();

         buttonStart.Enabled = false;
         buttonStop.Enabled = false;
         buttonCorrectTime.Enabled = false;

         CreateMenu();

         TranslateForm();
      }

      private void ReadingConfigurationFromXml()
      {
         // TODO: If not exist just create a new one
         currentLanguage = GetCurrentLanguageFromXml();
         FormingComboBoxListOfPrograms();
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

      private void TranslateForm()
      {
         CreateMenu();
         switch (currentLanguage) {
            case LanguageType.UA:
               SetUkrainianLanguage();
               break;
            case LanguageType.EN:
            default:
               SetEnglishLanguage();
               break;
         }
      }

      private void SetEnglishLanguage()
      {
         buttonStart.Text = "Start";
         buttonStop.Text = "Stop";
         buttonAddNewProgram.Text = "Add new program";
         buttonCorrectTime.Text = (!correction) ? "Correct time" : "Ok";
         Text = "Timer";
      }

      private void SetUkrainianLanguage()
      {
         buttonStart.Text = "Старт";
         buttonStop.Text = "Стоп";
         buttonAddNewProgram.Text = "Додати програму";
         buttonCorrectTime.Text = (!correction) ? "Скорегувати час" : "Записати";
         Text = "Таймер";
      }

      #region Menu
      private void CreateMenu()
      {
         /* Create menu items in current structure.
         Menu
            File
               New
               Exit
            Options
               Tools
                  Language
                     English
                     Ukrainian
            Help
               About
         */

         var mainMenu = new MainMenu();
         var topMenuItemFile = new MenuItem();
         var menuItemNew = new MenuItem();
         var menuItemExit = new MenuItem();
         var topMenuItemOptions = new MenuItem();
         var menuItemLanguage = new MenuItem();
         var menuItemEnglish = new MenuItem();
         var menuItemUkrainian = new MenuItem();
         var topMenuItemHelp = new MenuItem();
         var menuItemAbout = new MenuItem();
         // TODO:
         var menuItemAbout1 = new MenuItem("someText");

         switch (currentLanguage) {
            case LanguageType.UA: {
                  topMenuItemFile.Text = "&Файл";
                  menuItemNew.Text = "&Нова";
                  menuItemExit.Text = "В&ихід";

                  topMenuItemOptions.Text = "Нала&штування";
                  menuItemLanguage.Text = "&Мови";
                  menuItemEnglish.Text = "English";
                  menuItemUkrainian.Text = "Українська";

                  topMenuItemHelp.Text = "&Допомога";
                  menuItemAbout.Text = "&Про програму";
                  break;
               }
            case LanguageType.EN:
            default: {
                  topMenuItemFile.Text = "&File";
                  menuItemNew.Text = "&New";
                  menuItemExit.Text = "E&xit";

                  topMenuItemOptions.Text = "&Tools";
                  menuItemLanguage.Text = "&Language";
                  menuItemEnglish.Text = "English";
                  menuItemUkrainian.Text = "Українська";

                  topMenuItemHelp.Text = "&Help";
                  menuItemAbout.Text = "&About";
                  break;
               }
         }

         topMenuItemFile.MenuItems.Add(menuItemNew);
         topMenuItemFile.MenuItems.Add(menuItemExit);
         mainMenu.MenuItems.Add(topMenuItemFile);

         menuItemLanguage.MenuItems.Add(menuItemEnglish);
         menuItemLanguage.MenuItems.Add(menuItemUkrainian);
         topMenuItemOptions.MenuItems.Add(menuItemLanguage);
         mainMenu.MenuItems.Add(topMenuItemOptions);

         topMenuItemHelp.MenuItems.Add(menuItemAbout);
         topMenuItemHelp.MenuItems.Add(menuItemAbout1);
         mainMenu.MenuItems.Add(topMenuItemHelp);

         menuItemNew.Click += new EventHandler(menuItemNew_Click);
         menuItemExit.Click += new EventHandler(menuItemExit_Click);
         menuItemEnglish.Click += new EventHandler(menuItemEnglish_Click);
         menuItemUkrainian.Click += new EventHandler(menuItemUkrainian_Click);
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
      private void menuItemEnglish_Click(object sender, EventArgs e)
      {
         currentLanguage = LanguageType.EN;
         TranslateForm();
         SetCurrentLanguageToXml();
      }
      private void menuItemUkrainian_Click(object sender, EventArgs e)
      {
         currentLanguage = LanguageType.UA;
         TranslateForm();
         SetCurrentLanguageToXml();
      }
      private void menuItemAbout_Click(object sender, EventArgs e)
      {
         new AboutBoxForm(currentLanguage).ShowDialog();
      }
      #endregion

      private void StopRunningTimer(object sender, System.EventArgs e)
      {
         if ((comboBoxListOfPrograms.SelectedItem != null) && !correction) {
            Stop();
         }
      }

      private LanguageType GetCurrentLanguageFromXml()
      {
         var xmlDoc = new XmlDocument();
         xmlDoc.Load(SETUP_FILE);

         var languageNode = xmlDoc.SelectSingleNode("//main//options//language");
         var language = languageNode.InnerText.ToLower();

         switch (language) {
            case "ua":
               return LanguageType.UA;
            case "en":
            default:
               return LanguageType.EN;
         }
      }

      private void SetCurrentLanguageToXml()
      {
         var xmlDoc = new XmlDocument();
         xmlDoc.Load(SETUP_FILE);

         var languageNode = xmlDoc.SelectSingleNode("//main//options//language");
         languageNode.InnerText = currentLanguage.ToString().ToLower();

         xmlDoc.Save(SETUP_FILE);
      }

      // take a list programs from the setup file
      private void FormingComboBoxListOfPrograms()
      {
         var xmlDoc = new XmlDocument();
         xmlDoc.Load(SETUP_FILE);

         comboBoxListOfPrograms.Items.Clear();

         // read data from each node "program" and fill combobox
         foreach (XmlNode node in xmlDoc.SelectSingleNode("//main//programs").ChildNodes) {
            var program_name = node.InnerText;
            comboBoxListOfPrograms.Items.Add(program_name);
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
         var form2Dialog = new AddNewProgramForm(currentLanguage);
         var new_program_name = "";

         if (form2Dialog.ShowDialog(this) == DialogResult.OK) {
            new_program_name = form2Dialog.program_name;
         }

         form2Dialog.Dispose();

         if (new_program_name != "") {
            if (!comboBoxListOfPrograms.Items.Contains(new_program_name)) {
               comboBoxListOfPrograms.Items.Add(new_program_name);
               AddNewProgramNameToXmlFile(new_program_name);
               comboBoxListOfPrograms.SelectedItem = new_program_name;
            }
         }
      }

      private static void AddNewProgramNameToXmlFile(string program_name)
      {
         var xmlDoc = new XmlDocument();
         xmlDoc.Load(SETUP_FILE);

         var programs = xmlDoc.SelectSingleNode("//main//programs");

         var program = xmlDoc.CreateElement("program");
         program.InnerText = program_name;

         program.SetAttribute("time", "000:00:00");

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
         buttonStart.Enabled = true;
         buttonStop.Enabled = false;
         timer.Enabled = false;
         buttonCorrectTime.Enabled = true;
         buttonAddNewProgram.Enabled = true;
         comboBoxListOfPrograms.Enabled = true;
         SaveTimeToFile();
      }

      private void SaveTimeToFile()
      {
         var xmlDoc = new XmlDocument();
         xmlDoc.Load(SETUP_FILE);

         foreach (XmlNode node in xmlDoc.SelectSingleNode("//main//programs").ChildNodes) {
            if (node.InnerText == comboBoxListOfPrograms.SelectedItem.ToString()) {
               node.Attributes["time"].InnerText = textBoxTimer.Text;
               break;
            }
         }

         xmlDoc.Save(SETUP_FILE);
      }

      private void buttonCorrectTime_Click(object sender, EventArgs e)
      {
         // set messagebox texts in current language
         var questionText = "Do you realize that you correct time manually?";
         var questionTitle = "Confirm";
         var questionButtonYes = "Yes";
         var questionButtonNo = "No";
         var questionButtonCancel = "Cancel";

         var warningTitle = "Warning!";
         var warningQuestion = "Enter valid time (Format \"XXX:XX:XX\")";
         var warningButton = "Ok";

         switch (currentLanguage) {
            case LanguageType.EN: { break; } // leave default values
            case LanguageType.UA: {
                  questionText = "Ви усвідомлюєте, що ви коригуєте час вручну?";
                  questionTitle = "Підтвердження";
                  questionButtonYes = "Так";
                  questionButtonNo = "Ні";
                  questionButtonCancel = "Відмінити";

                  warningTitle = "Увага!";
                  warningQuestion = "Введіть коректний час (Формат \"XXX:XX:XX\")";
                  warningButton = "Ок";
                  break;
               }
            default: { break; } // leave default values
         }

         // show confirm dialog form
         if (!correction) {
            var confirm = new CorrectionConfirmForm(questionText, questionTitle, questionButtonYes, questionButtonNo, questionButtonCancel);
            if (confirm.ShowDialog() == DialogResult.Yes) {
               correction = true;
               EnableDisableButtons();
            }
         } else if (CorrectTime()) // save only in correct time
           {
            correction = false;
            SaveTimeToFile();
            EnableDisableButtons();
         } else // warning in case not valid time format
           {
            var warningWindow = new CorrectionWarningForm(warningTitle, warningQuestion, warningButton);
            warningWindow.ShowDialog();
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
            if ((seconds >= 0) && (seconds <= 59) && (minutes >= 0) && (minutes <= 59) && (hours >= 0) && (hours <= 999)) { return true; } else { return false; }
         }
         catch (Exception) {
            return false;
         }
      }

      private void comboBoxcomboBoxListOfPrograms_SelectedIndexChanged(object sender, EventArgs e)
      {
         buttonStart.Enabled = true;
         buttonCorrectTime.Enabled = true;

         var xmlDoc = new XmlDocument();
         xmlDoc.Load(SETUP_FILE);

         foreach (XmlNode node in xmlDoc.SelectSingleNode("//main//programs").ChildNodes) {
            if (node.InnerText == comboBoxListOfPrograms.SelectedItem.ToString()) {
               textBoxTimer.Text = (node.Attributes["time"] != null) ? node.Attributes["time"].InnerText : ""; ;
               break;
            }
         }
      }

      private void EnableDisableButtons()
      {
         TranslateForm();
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