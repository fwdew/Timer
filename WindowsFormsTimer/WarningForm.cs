using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsTimer
{
  public partial class WarningForm : Form
  {
    public WarningForm(string title, string warning, string button)
    {
      InitializeComponent();
      Text = title;
      labelWarning.Text = warning;
      buttonOk.Text = button;

      // align button on center
      var l = Size.Width - buttonOk.Size.Width;
      buttonOk.Location = new Point(l / 2, buttonOk.Location.Y);

      // align label on center
      l = Size.Width - labelWarning.Size.Width;
      labelWarning.Location = new Point(l / 2, labelWarning.Location.Y);
    }
  }
}
