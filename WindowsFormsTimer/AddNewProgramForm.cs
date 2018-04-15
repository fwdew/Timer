using System;
using System.Windows.Forms;

namespace WindowsFormsTimer
{
    public partial class AddNewProgramForm : Form
    {
        public string program_name = "";

        public AddNewProgramForm(LanguageType language)
        {
            InitializeComponent();
            TranslateFormControls(language);
        }

        private void TranslateFormControls(LanguageType language)
        {
          switch (language)
          {
            case LanguageType.UA:
              {
                Text = "Нова програма";
                buttonAdd.Text = "Додати";
                break;
              }
            case LanguageType.EN:
            default:
              {
                Text = "New Program";
                buttonAdd.Text = "Add";
                break;
              }
          }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            program_name = textBoxNewProgram.Text;
        }
    }
}
