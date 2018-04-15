using System;
using System.Windows.Forms;

namespace WindowsFormsTimer
{
    public partial class AddNewProgramForm : Form
    {
        public string program_name = "";

        public AddNewProgramForm()
        {
            InitializeComponent();
            TranslateFormControls();
        }

        private void TranslateFormControls()
        {
                Text = "Нова програма";
                buttonAdd.Text = "Додати";
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            program_name = textBoxNewProgram.Text;
        }
    }
}
