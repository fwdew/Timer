using System;
using System.Windows.Forms;

namespace WindowsFormsTimer
{
   partial class AboutBoxForm : Form
   {
      public AboutBoxForm()
      {
         InitializeComponent();
         TranslateFormControls();
      }

      private void TranslateFormControls()
      {
                  Text = "Про програму";
                  label1.Text = @"Ця програма розроблена мною і зроблена
тільки для мене. Будь-яке копіювання,
продавання чи інше репродукування без
дозволу автора - дозволене (навіть без
письмового чи усного попередження
автора).";
                  buttonClose.Text = "Закрити";
      }

      private void buttonClose_Click(object sender, EventArgs e)
      {
         Close();
      }
   }
}
