using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsTimer
{
  public partial class FormCorrectionWarning : Form
  {
    public FormCorrectionWarning(string title, string warning, string button)
    {
      InitializeComponent();
      this.Text = title;
      this.labelWarning.Text = warning;
      this.buttonOk.Text = button;

      // align button on center
      int l = this.Size.Width - this.buttonOk.Size.Width;
      this.buttonOk.Location = new Point(l / 2, this.buttonOk.Location.Y);

      // align label on center
      l = this.Size.Width - this.labelWarning.Size.Width;
      this.labelWarning.Location = new Point(l / 2, this.labelWarning.Location.Y);
    }
  }
}
