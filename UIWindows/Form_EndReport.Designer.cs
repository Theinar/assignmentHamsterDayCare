using System.Windows.Forms;

namespace UIWindows
{
    partial class Form_EndReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_EndReport));
            this.Label_Head_Choose = new System.Windows.Forms.Label();
            this.label_ID = new System.Windows.Forms.Label();
            this.textBox_For_ID = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Button_Submit = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label_Head_Choose
            // 
            this.Label_Head_Choose.Location = new System.Drawing.Point(10, 10);
            this.Label_Head_Choose.Name = "Label_Head_Choose";
            this.Label_Head_Choose.Size = new System.Drawing.Size(367, 23);
            this.Label_Head_Choose.TabIndex = 2;
            this.Label_Head_Choose.Click += new System.EventHandler(this.Form_EndReport_Load);
            // 
            // label_ID
            // 
            this.label_ID.AutoSize = true;
            this.label_ID.Location = new System.Drawing.Point(42, 96);
            this.label_ID.Name = "label_ID";
            this.label_ID.Size = new System.Drawing.Size(25, 16);
            this.label_ID.TabIndex = 1;
            this.label_ID.Text = "ID: ";
            // 
            // textBox_For_ID
            // 
            this.textBox_For_ID.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.textBox_For_ID.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBox_For_ID.Location = new System.Drawing.Point(82, 93);
            this.textBox_For_ID.Name = "textBox_For_ID";
            this.textBox_For_ID.Size = new System.Drawing.Size(100, 22);
            this.textBox_For_ID.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Button_Submit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(189, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 254);
            this.panel1.TabIndex = 3;
            // 
            // Button_Submit
            // 
            this.Button_Submit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Button_Submit.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Button_Submit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.Button_Submit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(57)))), ((int)(((byte)(75)))));
            this.Button_Submit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Submit.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Button_Submit.Image = ((System.Drawing.Image)(resources.GetObject("Button_Submit.Image")));
            this.Button_Submit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Button_Submit.Location = new System.Drawing.Point(0, 161);
            this.Button_Submit.Name = "Button_Submit";
            this.Button_Submit.Size = new System.Drawing.Size(200, 93);
            this.Button_Submit.TabIndex = 4;
            this.Button_Submit.Tag = "";
            this.Button_Submit.Text = "Submit";
            this.Button_Submit.UseVisualStyleBackColor = true;
            // 
            // Form_EndReport
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(23)))), ((int)(((byte)(26)))));
            this.ClientSize = new System.Drawing.Size(389, 254);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Label_Head_Choose);
            this.Controls.Add(this.textBox_For_ID);
            this.Controls.Add(this.label_ID);
            this.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(182)))), ((int)(((byte)(199)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form_EndReport";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label Label_Head_Choose;
        private Label label_ID;
        private TextBox textBox_For_ID;
        private Panel panel1;
        private Button Button_Submit;
    }
}

