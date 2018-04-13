using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsTimer
{
  public partial class FormCorrectionConfirm : Form
  {
    public FormCorrectionConfirm(string text, string title, string yes, string no, string cancel)
    {
      InitializeComponent();
      this.Text = title;
      this.labelQuestion.Text = text;
      this.buttonYes.Text = yes;
      this.buttonNo.Text = no;
      this.buttonCancel.Text = cancel;

      // align label on center
      int l = this.Size.Width - this.labelQuestion.Size.Width;
      this.labelQuestion.Location = new Point(l / 2, this.labelQuestion.Location.Y);

      // align button's panel on center
      l = this.Size.Width - this.tableLayoutPanel.Size.Width;
      this.tableLayoutPanel.Location = new Point(l / 2, this.tableLayoutPanel.Location.Y);
    }
  }
}
