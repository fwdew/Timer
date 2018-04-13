using System;
using System.Windows.Forms;

namespace WindowsFormsTimer
{
  partial class FormAboutBox : Form
  {
    public FormAboutBox(string language = "en")
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
            this.Text = "About";
            this.label1.Text = "This program my own creation and suitable" + Environment.NewLine +
                          "for me. Any copyrighting, selling and another" + Environment.NewLine +
                          "reproducting without permission from the" + Environment.NewLine +
                          "author is allowed (even without written or" + Environment.NewLine +
                          "verbal warning).";
            this.buttonClose.Text = "Close";
            break;
          }
        case "ua":
          {
            this.Text = "Про програму";
            this.label1.Text = "Ця програма розроблена мною і зроблена" + Environment.NewLine +
                               "тільки для мене. Будь-яке копіювання," + Environment.NewLine +
                               "продавання чи інше репродукування без" + Environment.NewLine +
                               "дозволу автора - дозволене (навіть без" + Environment.NewLine +
                               "письмового чи усного попередження" + Environment.NewLine +
                               "автора).";
            this.buttonClose.Text = "Закрити";
            break;
          }
        default: // default language
          {
            this.Text = "About";
            this.label1.Text = "This program my own creation and suitable" + Environment.NewLine +
                          "for me. Any copyrighting, selling and another" + Environment.NewLine +
                          "reproducting without permission from the" + Environment.NewLine +
                          "author is allowed (even without written or" + Environment.NewLine +
                          "verbal warning).";
            this.buttonClose.Text = "Close";
            break;
          }
      }
    }

    private void buttonClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}
