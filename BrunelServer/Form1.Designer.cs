namespace BrunelServer
{
    partial class Form1
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
            this.StatusTextBox = new System.Windows.Forms.RichTextBox();
            this.AccountBoxA = new System.Windows.Forms.RichTextBox();
            this.AccountBoxB = new System.Windows.Forms.RichTextBox();
            this.AccountBoxC = new System.Windows.Forms.RichTextBox();
            this.LabelA = new System.Windows.Forms.Label();
            this.LabelB = new System.Windows.Forms.Label();
            this.LabelC = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // StatusTextBox
            // 
            this.StatusTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.StatusTextBox.Location = new System.Drawing.Point(35, 12);
            this.StatusTextBox.Name = "StatusTextBox";
            this.StatusTextBox.ReadOnly = true;
            this.StatusTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.StatusTextBox.Size = new System.Drawing.Size(209, 129);
            this.StatusTextBox.TabIndex = 0;
            this.StatusTextBox.Text = "";
            // 
            // AccountBoxA
            // 
            this.AccountBoxA.Location = new System.Drawing.Point(26, 193);
            this.AccountBoxA.Name = "AccountBoxA";
            this.AccountBoxA.ReadOnly = true;
            this.AccountBoxA.Size = new System.Drawing.Size(40, 33);
            this.AccountBoxA.TabIndex = 1;
            this.AccountBoxA.Text = "";
            // 
            // AccountBoxB
            // 
            this.AccountBoxB.Location = new System.Drawing.Point(90, 193);
            this.AccountBoxB.Name = "AccountBoxB";
            this.AccountBoxB.ReadOnly = true;
            this.AccountBoxB.Size = new System.Drawing.Size(40, 33);
            this.AccountBoxB.TabIndex = 2;
            this.AccountBoxB.Text = "";
            // 
            // AccountBoxC
            // 
            this.AccountBoxC.Location = new System.Drawing.Point(151, 193);
            this.AccountBoxC.Name = "AccountBoxC";
            this.AccountBoxC.ReadOnly = true;
            this.AccountBoxC.Size = new System.Drawing.Size(40, 33);
            this.AccountBoxC.TabIndex = 3;
            this.AccountBoxC.Text = "";
            // 
            // LabelA
            // 
            this.LabelA.AutoSize = true;
            this.LabelA.Location = new System.Drawing.Point(38, 177);
            this.LabelA.Name = "LabelA";
            this.LabelA.Size = new System.Drawing.Size(14, 13);
            this.LabelA.TabIndex = 4;
            this.LabelA.Text = "A";
            // 
            // LabelB
            // 
            this.LabelB.AutoSize = true;
            this.LabelB.Location = new System.Drawing.Point(103, 177);
            this.LabelB.Name = "LabelB";
            this.LabelB.Size = new System.Drawing.Size(14, 13);
            this.LabelB.TabIndex = 5;
            this.LabelB.Text = "B";
            // 
            // LabelC
            // 
            this.LabelC.AutoSize = true;
            this.LabelC.Location = new System.Drawing.Point(163, 177);
            this.LabelC.Name = "LabelC";
            this.LabelC.Size = new System.Drawing.Size(14, 13);
            this.LabelC.TabIndex = 6;
            this.LabelC.Text = "C";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.LabelC);
            this.Controls.Add(this.LabelB);
            this.Controls.Add(this.LabelA);
            this.Controls.Add(this.AccountBoxC);
            this.Controls.Add(this.AccountBoxB);
            this.Controls.Add(this.AccountBoxA);
            this.Controls.Add(this.StatusTextBox);
            this.Name = "Form1";
            this.Text = "Brunel Banking Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.RichTextBox StatusTextBox;
        public System.Windows.Forms.RichTextBox AccountBoxA;
        public System.Windows.Forms.RichTextBox AccountBoxB;
        public System.Windows.Forms.RichTextBox AccountBoxC;
        private System.Windows.Forms.Label LabelA;
        private System.Windows.Forms.Label LabelB;
        private System.Windows.Forms.Label LabelC;
    }
}

