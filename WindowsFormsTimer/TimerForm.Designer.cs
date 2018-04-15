namespace WindowsFormsTimer
{
    partial class TimerForm
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
         this.components = new System.ComponentModel.Container();
         this.buttonStart = new System.Windows.Forms.Button();
         this.textBoxTimer = new System.Windows.Forms.TextBox();
         this.buttonStop = new System.Windows.Forms.Button();
         this.comboBoxListOfPrograms = new System.Windows.Forms.ComboBox();
         this.buttonAddNewProgram = new System.Windows.Forms.Button();
         this.timer = new System.Windows.Forms.Timer(this.components);
         this.buttonCorrectTime = new System.Windows.Forms.Button();
         this.button1 = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // buttonStart
         // 
         this.buttonStart.Location = new System.Drawing.Point(12, 71);
         this.buttonStart.Name = "buttonStart";
         this.buttonStart.Size = new System.Drawing.Size(171, 32);
         this.buttonStart.TabIndex = 0;
         this.buttonStart.Text = "Start";
         this.buttonStart.UseVisualStyleBackColor = true;
         this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
         // 
         // textBoxTimer
         // 
         this.textBoxTimer.Enabled = false;
         this.textBoxTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
         this.textBoxTimer.Location = new System.Drawing.Point(12, 12);
         this.textBoxTimer.Name = "textBoxTimer";
         this.textBoxTimer.Size = new System.Drawing.Size(184, 53);
         this.textBoxTimer.TabIndex = 4;
         this.textBoxTimer.Text = "000:00:00";
         this.textBoxTimer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // buttonStop
         // 
         this.buttonStop.Location = new System.Drawing.Point(12, 109);
         this.buttonStop.Name = "buttonStop";
         this.buttonStop.Size = new System.Drawing.Size(171, 32);
         this.buttonStop.TabIndex = 1;
         this.buttonStop.Text = "Stop";
         this.buttonStop.UseVisualStyleBackColor = true;
         this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
         // 
         // comboBoxListOfPrograms
         // 
         this.comboBoxListOfPrograms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxListOfPrograms.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
         this.comboBoxListOfPrograms.FormattingEnabled = true;
         this.comboBoxListOfPrograms.Location = new System.Drawing.Point(199, 70);
         this.comboBoxListOfPrograms.Name = "comboBoxListOfPrograms";
         this.comboBoxListOfPrograms.Size = new System.Drawing.Size(150, 32);
         this.comboBoxListOfPrograms.TabIndex = 5;
         this.comboBoxListOfPrograms.SelectedIndexChanged += new System.EventHandler(this.comboBoxcomboBoxListOfPrograms_SelectedIndexChanged);
         // 
         // buttonAddNewProgram
         // 
         this.buttonAddNewProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
         this.buttonAddNewProgram.Location = new System.Drawing.Point(291, 12);
         this.buttonAddNewProgram.Name = "buttonAddNewProgram";
         this.buttonAddNewProgram.Size = new System.Drawing.Size(61, 41);
         this.buttonAddNewProgram.TabIndex = 6;
         this.buttonAddNewProgram.Text = "Add new program";
         this.buttonAddNewProgram.UseVisualStyleBackColor = true;
         this.buttonAddNewProgram.Click += new System.EventHandler(this.buttonAddNewProgram_Click);
         // 
         // timer
         // 
         this.timer.Interval = 1000;
         this.timer.Tick += new System.EventHandler(this.timer_Tick);
         // 
         // buttonCorrectTime
         // 
         this.buttonCorrectTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
         this.buttonCorrectTime.Location = new System.Drawing.Point(202, 12);
         this.buttonCorrectTime.Name = "buttonCorrectTime";
         this.buttonCorrectTime.Size = new System.Drawing.Size(73, 41);
         this.buttonCorrectTime.TabIndex = 7;
         this.buttonCorrectTime.Text = "Correct time";
         this.buttonCorrectTime.UseVisualStyleBackColor = true;
         this.buttonCorrectTime.Click += new System.EventHandler(this.buttonCorrectTime_Click);
         // 
         // button1
         // 
         this.button1.Location = new System.Drawing.Point(202, 108);
         this.button1.Name = "button1";
         this.button1.Size = new System.Drawing.Size(75, 23);
         this.button1.TabIndex = 8;
         this.button1.Text = "button1";
         this.button1.UseVisualStyleBackColor = true;
         this.button1.Click += new System.EventHandler(this.button1_Click);
         // 
         // TimerForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(368, 150);
         this.Controls.Add(this.button1);
         this.Controls.Add(this.buttonCorrectTime);
         this.Controls.Add(this.buttonAddNewProgram);
         this.Controls.Add(this.comboBoxListOfPrograms);
         this.Controls.Add(this.buttonStop);
         this.Controls.Add(this.textBoxTimer);
         this.Controls.Add(this.buttonStart);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
         this.MaximizeBox = false;
         this.Name = "TimerForm";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Timer";
         this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StopRunningTimer);
         this.ResumeLayout(false);
         this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.TextBox textBoxTimer;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.ComboBox comboBoxListOfPrograms;
        private System.Windows.Forms.Button buttonAddNewProgram;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button buttonCorrectTime;
      private System.Windows.Forms.Button button1;
   }
}

