namespace WindowsFormsTimer
{
  partial class CorrectionWarningForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.buttonOk = new System.Windows.Forms.Button();
      this.labelWarning = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // buttonOk
      // 
      this.buttonOk.AutoSize = true;
      this.buttonOk.Location = new System.Drawing.Point(12, 25);
      this.buttonOk.Name = "buttonYes";
      this.buttonOk.Size = new System.Drawing.Size(31, 23);
      this.buttonOk.TabIndex = 0;
      this.buttonOk.Text = "Ok";
      this.buttonOk.UseVisualStyleBackColor = true;
      this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      // 
      // labelWarning
      // 
      this.labelWarning.AutoSize = true;
      this.labelWarning.Location = new System.Drawing.Point(11, 9);
      this.labelWarning.Name = "labelWarning";
      this.labelWarning.Size = new System.Drawing.Size(47, 13);
      this.labelWarning.TabIndex = 1;
      this.labelWarning.Text = "Warning";
      // 
      // FormCorrectionWarning
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSize = true;
      this.ClientSize = new System.Drawing.Size(116, 59);
      this.Controls.Add(this.labelWarning);
      this.Controls.Add(this.buttonOk);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormCorrectionWarning";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonOk;
    private System.Windows.Forms.Label labelWarning;
  }
}