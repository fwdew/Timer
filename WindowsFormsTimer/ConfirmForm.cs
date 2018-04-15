using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsTimer
{
  public partial class ConfirmForm : Form
  {
    public ConfirmForm(string text, string title, string yes, string no, string cancel)
    {
      InitializeComponent();
      Text = title;
      labelQuestion.Text = text;
      buttonYes.Text = yes;
      buttonNo.Text = no;
      buttonCancel.Text = cancel;

      // align label on center
      var l = Size.Width - labelQuestion.Size.Width;
      labelQuestion.Location = new Point(l / 2, labelQuestion.Location.Y);

      // align button's panel on center
      l = Size.Width - tableLayoutPanel.Size.Width;
      tableLayoutPanel.Location = new Point(l / 2, tableLayoutPanel.Location.Y);
    }
  }
}
