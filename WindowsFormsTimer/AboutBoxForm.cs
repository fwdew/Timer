using System;
using System.Windows.Forms;

namespace WindowsFormsTimer
{
   partial class AboutBoxForm : Form
   {
      public AboutBoxForm()
      {
         InitializeComponent();
      }

      private void buttonClose_Click(object sender, EventArgs e)
      {
         Close();
      }
   }
}
