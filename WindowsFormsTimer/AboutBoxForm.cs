using System;
using System.Windows.Forms;

namespace WindowsFormsTimer
{
   partial class AboutBoxForm : Form
   {
      public AboutBoxForm(LanguageType language)
      {
         InitializeComponent();
         TranslateFormControls(language);
      }

      private void TranslateFormControls(LanguageType language)
      {
         switch (language) {
            case LanguageType.UA: {
                  Text = "Про програму";
                  label1.Text = @"Ця програма розроблена мною і зроблена
тільки для мене. Будь-яке копіювання,
продавання чи інше репродукування без
дозволу автора - дозволене (навіть без
письмового чи усного попередження
автора).";
                  buttonClose.Text = "Закрити";
                  break;
               }
            case LanguageType.EN:
            default: {
                  Text = "About";
                  label1.Text = @"This program my own creation and suitable
for me. Any copyrighting, selling and another
reproducting without permission from the
author is allowed (even without written or
verbal warning).";
                  buttonClose.Text = "Close";
                  break;
               }
         }
      }

      private void buttonClose_Click(object sender, EventArgs e)
      {
         Close();
      }
   }
}
