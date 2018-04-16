using System;
using System.Windows.Forms;

namespace WindowsFormsTimer
{
    public partial class AddNewProgramForm : Form
    {
        public string programName = "";

        public AddNewProgramForm()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            programName = textBoxNewProgram.Text;
        }
    }
}
