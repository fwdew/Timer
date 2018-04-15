namespace WindowsFormsTimer
{
  partial class CorrectionConfirmForm
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
      this.buttonYes = new System.Windows.Forms.Button();
      this.labelQuestion = new System.Windows.Forms.Label();
      this.buttonNo = new System.Windows.Forms.Button();
      this.buttonCancel = new System.Windows.Forms.Button();
      this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
      this.tableLayoutPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // buttonYes
      // 
      this.buttonYes.Anchor = System.Windows.Forms.AnchorStyles.Left;
      this.buttonYes.AutoSize = true;
      this.buttonYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
      this.buttonYes.Location = new System.Drawing.Point(3, 4);
      this.buttonYes.Name = "buttonYes";
      this.buttonYes.Size = new System.Drawing.Size(10, 23);
      this.buttonYes.TabIndex = 0;
      this.buttonYes.UseVisualStyleBackColor = true;
      // 
      // labelQuestion
      // 
      this.labelQuestion.AutoSize = true;
      this.labelQuestion.Location = new System.Drawing.Point(11, 9);
      this.labelQuestion.Name = "labelQuestion";
      this.labelQuestion.Size = new System.Drawing.Size(48, 13);
      this.labelQuestion.TabIndex = 1;
      this.labelQuestion.Text = "Confirm?";
      // 
      // buttonNo
      // 
      this.buttonNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonNo.AutoSize = true;
      this.buttonNo.DialogResult = System.Windows.Forms.DialogResult.No;
      this.buttonNo.Location = new System.Drawing.Point(19, 4);
      this.buttonNo.Name = "buttonNo";
      this.buttonNo.Size = new System.Drawing.Size(14, 23);
      this.buttonNo.TabIndex = 2;
      this.buttonNo.UseVisualStyleBackColor = true;
      // 
      // buttonCancel
      // 
      this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
      this.buttonCancel.AutoSize = true;
      this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonCancel.Location = new System.Drawing.Point(40, 4);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new System.Drawing.Size(10, 23);
      this.buttonCancel.TabIndex = 3;
      this.buttonCancel.UseVisualStyleBackColor = true;
      // 
      // tableLayoutPanel
      // 
      this.tableLayoutPanel.AutoSize = true;
      this.tableLayoutPanel.ColumnCount = 3;
      this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel.Controls.Add(this.buttonYes, 0, 0);
      this.tableLayoutPanel.Controls.Add(this.buttonNo, 1, 0);
      this.tableLayoutPanel.Controls.Add(this.buttonCancel, 2, 0);
      this.tableLayoutPanel.Location = new System.Drawing.Point(14, 25);
      this.tableLayoutPanel.Name = "tableLayoutPanel";
      this.tableLayoutPanel.RowCount = 1;
      this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel.Size = new System.Drawing.Size(53, 32);
      this.tableLayoutPanel.TabIndex = 5;
      // 
      // FormCorrectionConfirm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSize = true;
      this.ClientSize = new System.Drawing.Size(116, 64);
      this.Controls.Add(this.tableLayoutPanel);
      this.Controls.Add(this.labelQuestion);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormCorrectionConfirm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.tableLayoutPanel.ResumeLayout(false);
      this.tableLayoutPanel.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonYes;
    private System.Windows.Forms.Label labelQuestion;
    private System.Windows.Forms.Button buttonNo;
    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
  }
}