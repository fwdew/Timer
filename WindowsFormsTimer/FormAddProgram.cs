using System;
using System.Windows.Forms;

namespace WindowsFormsTimer
{
    public partial class FormAddProgram : Form
    {
        public string program_name = "";
        public FormAddProgram(string language)
        {
            InitializeComponent();
            TranslateFormControls(language);
        }

        private void TranslateFormControls(string language = "en")
        {
          switch (language)
          {
            case "en":
              {
                this.Text = "New Program";
                this.buttonAdd.Text = "Add";
                break;
              }
            case "ua":
              {
                this.Text = "Нова програма";
                this.buttonAdd.Text = "Додати";
                break;
              }
            default: // default language
              {
                this.Text = "New Program";
                this.buttonAdd.Text = "Add";
                break;
              }
          }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            program_name = this.textBoxNewProgram.Text;
        }
    }
}
