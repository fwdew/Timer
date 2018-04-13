using System;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Win32;

namespace WindowsFormsTimer
{
    public partial class FormTimer : Form
    {
        private const string setupfile_xml = "setup_timers.xml";
        private const string time_separator = ":";
        private static string[] time_separators = { time_separator };
        private bool correction = false;
        private string currentLanguage = "en";

        public FormTimer()
        {
            InitializeComponent();
            ConfigureForm();

            // stop timer on current event
            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged; // to intercept hibernate event
            SystemEvents.SessionEnded += SystemEvents_Power; // to intercept shutdown event
        }

        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            buttonStop_Click(this, EventArgs.Empty);
        }

        private void SystemEvents_Power(object sender, SessionEndedEventArgs e)
        {
            buttonStop_Click(this, EventArgs.Empty);
        }

        private void ConfigureForm()
        {
            ReadingConfigurationFromXml();

            this.buttonStart.Enabled = false;
            this.buttonStop.Enabled = false;
            this.buttonCorrectTime.Enabled = false;

            CreateMenu();

            TranslateForm();
        }

        private void ReadingConfigurationFromXml()
        {
            currentLanguage = GetCurrentLanguageFromXml();
            FormingComboBoxListOfPrograms();
        }

        private void CreateMenu()
        {
            // Create a main menu object.
            MainMenu mainMenu = new MainMenu();

            // Create empty menu item objects.
            // Menu -> File
            MenuItem topMenuItemFile = new MenuItem();
            // Menu -> File -> New
            MenuItem menuItemNew = new MenuItem();
            // Menu -> File -> Exit
            MenuItem menuItemExit = new MenuItem();
            // Menu -> Options
            MenuItem topMenuItemOptions = new MenuItem();
            // Menu -> Options -> Tools -> Language
            MenuItem menuItemLanguage = new MenuItem();
            // Menu -> Options -> Tools -> Language -> English
            MenuItem menuItemEnglish = new MenuItem();
            // Menu -> Options -> Tools -> Language -> Ukrainian
            MenuItem menuItemUkrainian = new MenuItem();
            // Menu -> Help
            MenuItem topMenuItemHelp = new MenuItem();
            // Menu -> Help -> About
            MenuItem menuItemAbout = new MenuItem();

            // Set the caption of the menu items on current language.
            switch (currentLanguage)
            {
                case "en":
                    {
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
                case "ua":
                    {
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
                default:
                    {
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

            // Add the menu items to the main menu.
            topMenuItemFile.MenuItems.Add(menuItemNew);
            topMenuItemFile.MenuItems.Add(menuItemExit);
            mainMenu.MenuItems.Add(topMenuItemFile);

            menuItemLanguage.MenuItems.Add(menuItemEnglish);
            menuItemLanguage.MenuItems.Add(menuItemUkrainian);
            topMenuItemOptions.MenuItems.Add(menuItemLanguage);
            mainMenu.MenuItems.Add(topMenuItemOptions);

            topMenuItemHelp.MenuItems.Add(menuItemAbout);
            mainMenu.MenuItems.Add(topMenuItemHelp);

            // Add functionality to the menu items using the Click event. 
            menuItemNew.Click += new EventHandler(this.menuItemNew_Click);
            menuItemExit.Click += new EventHandler(this.menuItemExit_Click);
            menuItemEnglish.Click += new EventHandler(this.menuItemEnglish_Click);
            menuItemUkrainian.Click += new EventHandler(this.menuItemUkrainian_Click);
            menuItemAbout.Click += new EventHandler(this.menuItemAbout_Click);

            // Assign mainMenu to the form.
            this.Menu = mainMenu;
        }

        private void TranslateForm()
        {
            CreateMenu();
            switch (currentLanguage)
            {
                case "en": SetEnglishLanguage(); break;
                case "ua": SetUkrainianLanguage(); break;
                default: SetEnglishLanguage(); break;
            }
        }

        private void SetEnglishLanguage()
        {
            this.buttonStart.Text = "Start";
            this.buttonStop.Text = "Stop";
            this.buttonAddNewProgram.Text = "Add new program";
            this.buttonCorrectTime.Text = (!correction) ? "Correct time" : "Ok";
            this.Text = "Timer";
        }

        private void SetUkrainianLanguage()
        {
            this.buttonStart.Text = "Старт";
            this.buttonStop.Text = "Стоп";
            this.buttonAddNewProgram.Text = "Додати програму";
            this.buttonCorrectTime.Text = (!correction) ? "Скорегувати час" : "Записати";
            this.Text = "Таймер";
        }

        // add event on menu's items click
        private void menuItemNew_Click(object sender, System.EventArgs e)
        {
            StopRunningTimer(this, EventArgs.Empty);
            buttonAddNewProgram_Click(this, EventArgs.Empty);
        }
        private void menuItemExit_Click(object sender, System.EventArgs e)
        {
            StopRunningTimer(this, EventArgs.Empty);
            this.Close();
        }
        private void menuItemEnglish_Click(object sender, System.EventArgs e)
        {
            currentLanguage = "en";
            TranslateForm();
            SetCurrentLanguageToXml();
        }
        private void menuItemUkrainian_Click(object sender, System.EventArgs e)
        {
            currentLanguage = "ua";
            TranslateForm();
            SetCurrentLanguageToXml();
        }
        private void menuItemAbout_Click(object sender, System.EventArgs e)
        {
            FormAboutBox ab = new FormAboutBox(currentLanguage);
            ab.ShowDialog();
        }

        private void StopRunningTimer(object sender, System.EventArgs e)
        {
            if ((this.comboBoxListOfPrograms.SelectedItem != null) && !correction)
            {
                buttonStop_Click(this, EventArgs.Empty);
            }
        }

        private string GetCurrentLanguageFromXml()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(setupfile_xml);

            XmlNode languageNode = (XmlNode)xmlDoc.SelectSingleNode("//main//options//language");
            string language = languageNode.InnerText;
            if (language != "")
                return language;
            return "en";
        }

        private void SetCurrentLanguageToXml()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(setupfile_xml);

            XmlNode languageNode = (XmlNode)xmlDoc.SelectSingleNode("//main//options//language");
            languageNode.InnerText = currentLanguage;

            xmlDoc.Save(setupfile_xml);
        }

        // take a list programs from the setup file
        private void FormingComboBoxListOfPrograms()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(setupfile_xml);

            comboBoxListOfPrograms.Items.Clear();

            // read data from each node "program" and fill combobox
            foreach (XmlNode node in xmlDoc.SelectSingleNode("//main//programs").ChildNodes)
            {
                string program_name = node.InnerText;
                comboBoxListOfPrograms.Items.Add(program_name);
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            this.timer.Enabled = true;
            this.buttonStart.Enabled = false;
            this.buttonStop.Enabled = true;
            this.buttonCorrectTime.Enabled = false;
            this.buttonAddNewProgram.Enabled = false;
            comboBoxListOfPrograms.Enabled = false;
        }

        private void timer_Tick(object Sender, EventArgs e)
        {
            string[] time = this.textBoxTimer.Text.Split(time_separators, StringSplitOptions.None);
            int seconds = Convert.ToInt32(time[2]) + 1;
            int minutes = Convert.ToInt32(time[1]);
            int hours = Convert.ToInt32(time[0]);

            if (seconds == 60)
            {
                seconds = 0;
                minutes += 1;
                if (minutes == 60)
                {
                    minutes = 0;
                    hours += 1;
                }
            }

            this.textBoxTimer.Text = hours.ToString("000") + time_separator + minutes.ToString("00") + time_separator + seconds.ToString("00");
        }

        private void buttonAddNewProgram_Click(object sender, EventArgs e)
        {
            FormAddProgram form2Dialog = new FormAddProgram(currentLanguage);
            string new_program_name = "";

            if (form2Dialog.ShowDialog(this) == DialogResult.OK)
            {
                new_program_name = form2Dialog.program_name;
            }

            form2Dialog.Dispose();

            if (new_program_name != "")
            {
                if (!comboBoxListOfPrograms.Items.Contains(new_program_name))
                {
                    this.comboBoxListOfPrograms.Items.Add(new_program_name);
                    AddNewProgramNameToXmlFile(new_program_name);
                    this.comboBoxListOfPrograms.SelectedItem = new_program_name;
                }
            }
        }

        private static void AddNewProgramNameToXmlFile(string program_name)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(setupfile_xml);

            XmlNode programs = (XmlNode)xmlDoc.SelectSingleNode("//main//programs");

            XmlElement program = xmlDoc.CreateElement("program");
            program.InnerText = program_name;

            program.SetAttribute("time", "000:00:00");

            programs.AppendChild(program);

            xmlDoc.Save(setupfile_xml);
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            this.buttonStart.Enabled = true;
            this.buttonStop.Enabled = false;
            this.timer.Enabled = false;
            this.buttonCorrectTime.Enabled = true;
            this.buttonAddNewProgram.Enabled = true;
            comboBoxListOfPrograms.Enabled = true;
            SaveTimeToFile();
        }

        private void SaveTimeToFile()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(setupfile_xml);

            foreach (XmlNode node in xmlDoc.SelectSingleNode("//main//programs").ChildNodes)
            {
                if (node.InnerText == comboBoxListOfPrograms.SelectedItem.ToString())
                {
                    node.Attributes["time"].InnerText = this.textBoxTimer.Text;
                    break;
                }
            }

            xmlDoc.Save(setupfile_xml);
        }

        private void buttonCorrectTime_Click(object sender, EventArgs e)
        {
            // set messagebox texts in current language
            string questionText = "Do you realize that you correct time manually?";
            string questionTitle = "Confirm";
            string questionButtonYes = "Yes";
            string questionButtonNo = "No";
            string questionButtonCancel = "Cancel";

            string warningTitle = "Warning!";
            string warningQuestion = "Enter valid time (Format \"XXX:XX:XX\")";
            string warningButton = "Ok";
            switch (currentLanguage)
            {
                case "en": { break; } // leave default values
                case "ua":
                    {
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
            if (!correction)
            {
                FormCorrectionConfirm confirm = new FormCorrectionConfirm(questionText, questionTitle, questionButtonYes, questionButtonNo, questionButtonCancel);
                if (confirm.ShowDialog() == DialogResult.Yes)
                {
                    correction = true;
                    EnableDisableButtons();
                }
            }
            else if (CorrectTime()) // save only in correct time
            {
                correction = false;
                SaveTimeToFile();
                EnableDisableButtons();
            }
            else // warning in case not valid time format
            {
                FormCorrectionWarning warningWindow = new FormCorrectionWarning(warningTitle, warningQuestion, warningButton);
                warningWindow.ShowDialog();
            }
        }

        // to correct time manually
        private bool CorrectTime()
        {
            string[] time = textBoxTimer.Text.Split(time_separators, StringSplitOptions.None);
            try
            {
                int seconds = Convert.ToInt32(time[2]);
                int minutes = Convert.ToInt32(time[1]);
                int hours = Convert.ToInt32(time[0]);
                if ((seconds >= 0) && (seconds <= 59) && (minutes >= 0) && (minutes <= 59) && (hours >= 0) && (hours <= 999))
                { return true; }
                else { return false; }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void comboBoxcomboBoxListOfPrograms_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.buttonStart.Enabled = true;
            this.buttonCorrectTime.Enabled = true;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(setupfile_xml);

            foreach (XmlNode node in xmlDoc.SelectSingleNode("//main//programs").ChildNodes)
            {
                if (node.InnerText == comboBoxListOfPrograms.SelectedItem.ToString())
                {
                    this.textBoxTimer.Text = (node.Attributes["time"] != null) ? node.Attributes["time"].InnerText : ""; ;
                    break;
                }
            }
        }

        private void EnableDisableButtons()
        {
            TranslateForm();
            if (correction)
            {
                textBoxTimer.Enabled = true;
                buttonStart.Enabled = false;
                buttonAddNewProgram.Enabled = false;
                comboBoxListOfPrograms.Enabled = false;
            }
            else
            {
                textBoxTimer.Enabled = false;
                buttonStart.Enabled = true;
                buttonAddNewProgram.Enabled = true;
                comboBoxListOfPrograms.Enabled = true;
            }
        }
    }
}