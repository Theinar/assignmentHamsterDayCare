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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_Endreport_Head = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label_EndReportTypes = new System.Windows.Forms.Label();
            this.label_Endreport_vauess = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label_Endreport_Head);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(734, 94);
            this.panel1.TabIndex = 0;
            // 
            // label_Endreport_Head
            // 
            this.label_Endreport_Head.AutoSize = true;
            this.label_Endreport_Head.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_Endreport_Head.Location = new System.Drawing.Point(309, 28);
            this.label_Endreport_Head.Name = "label_Endreport_Head";
            this.label_Endreport_Head.Size = new System.Drawing.Size(134, 25);
            this.label_Endreport_Head.TabIndex = 1;
            this.label_Endreport_Head.Text = "END REPORT";
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(38, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(679, 569);
            this.panel2.TabIndex = 1;
            // 
            // label_EndReportTypes
            // 
            this.label_EndReportTypes.AutoSize = true;
            this.label_EndReportTypes.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_EndReportTypes.Location = new System.Drawing.Point(38, 100);
            this.label_EndReportTypes.Name = "label_EndReportTypes";
            this.label_EndReportTypes.Size = new System.Drawing.Size(0, 19);
            this.label_EndReportTypes.TabIndex = 1;
            // 
            // label_Endreport_vauess
            // 
            this.label_Endreport_vauess.AutoSize = true;
            this.label_Endreport_vauess.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_Endreport_vauess.Location = new System.Drawing.Point(479, 100);
            this.label_Endreport_vauess.Name = "label_Endreport_vauess";
            this.label_Endreport_vauess.Size = new System.Drawing.Size(0, 19);
            this.label_Endreport_vauess.TabIndex = 2;
            // 
            // Form_EndReport
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(23)))), ((int)(((byte)(26)))));
            this.ClientSize = new System.Drawing.Size(734, 711);
            this.Controls.Add(this.label_Endreport_vauess);
            this.Controls.Add(this.label_EndReportTypes);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(182)))), ((int)(((byte)(199)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form_EndReport";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private Label label_Endreport_Head;
        private Panel panel2;
        private Label label_EndReportTypes;
        private Label label_Endreport_vauess;
    }
}

