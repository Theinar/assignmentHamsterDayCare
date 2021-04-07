using System.Windows.Forms;

namespace UIWindows
{
    partial class Form_Choose
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Label_Head_Choose = new System.Windows.Forms.Label();
            this.label_ID = new System.Windows.Forms.Label();
            this.textBox_For_ID = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Label_Head_Choose
            // 
            this.Label_Head_Choose.Location = new System.Drawing.Point(10, 10);
            this.Label_Head_Choose.Name = "Label_Head_Choose";
            this.Label_Head_Choose.Size = new System.Drawing.Size(367, 23);
            this.Label_Head_Choose.TabIndex = 2;
            this.Label_Head_Choose.Click += new System.EventHandler(this.Form_Choose_Load);
            // 
            // label_ID
            // 
            this.label_ID.AutoSize = true;
            this.label_ID.Location = new System.Drawing.Point(83, 118);
            this.label_ID.Name = "label_ID";
            this.label_ID.Size = new System.Drawing.Size(25, 16);
            this.label_ID.TabIndex = 1;
            this.label_ID.Text = "ID: ";
            // 
            // textBox_For_ID
            // 
            this.textBox_For_ID.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.textBox_For_ID.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBox_For_ID.Location = new System.Drawing.Point(114, 115);
            this.textBox_For_ID.Name = "textBox_For_ID";
            this.textBox_For_ID.Size = new System.Drawing.Size(100, 22);
            this.textBox_For_ID.TabIndex = 0;
            this.textBox_For_ID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckEnter);
            // 
            // Form_Choose
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(23)))), ((int)(((byte)(26)))));
            this.ClientSize = new System.Drawing.Size(389, 254);
            this.Controls.Add(this.Label_Head_Choose);
            this.Controls.Add(this.textBox_For_ID);
            this.Controls.Add(this.label_ID);
            this.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(182)))), ((int)(((byte)(199)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form_Choose";
            this.Load += new System.EventHandler(this.Form_Choose_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label Label_Head_Choose;
        private Label label_ID;
        private TextBox textBox_For_ID;
    }
}

