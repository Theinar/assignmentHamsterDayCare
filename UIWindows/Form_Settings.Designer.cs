using System.Windows.Forms;

namespace UIWindows
{
    partial class Form_Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Settings));
            this.label_Initial_Head = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox_Change_EXArea_cap = new System.Windows.Forms.TextBox();
            this.checkBox_Change_ExArea_cap = new System.Windows.Forms.CheckBox();
            this.textBox_set_nr_of_days = new System.Windows.Forms.TextBox();
            this.textBox_Set_ticks_Per_Second = new System.Windows.Forms.TextBox();
            this.checkBox_Change_number_Of_ExAreas = new System.Windows.Forms.CheckBox();
            this.textBox_Change_nr_of_exAreas = new System.Windows.Forms.TextBox();
            this.textBox_Change_number_of_cages = new System.Windows.Forms.TextBox();
            this.textBox_set_Fictional_Date = new System.Windows.Forms.TextBox();
            this.textBox_Cage_cap = new System.Windows.Forms.TextBox();
            this.checkBox_Cange_nr_Of_exAreas = new System.Windows.Forms.CheckBox();
            this.checkBox_Change_nr_of_cages = new System.Windows.Forms.CheckBox();
            this.Button_Submit = new System.Windows.Forms.Button();
            this.checkBox_Import_csv = new System.Windows.Forms.CheckBox();
            this.radioButton_custom = new System.Windows.Forms.RadioButton();
            this.radioButton_Paulstandard = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label_mandetory = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_Set_Nr_Of_Days = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label_Set_Date = new System.Windows.Forms.Label();
            this.label_Change_Nr_ExAreas = new System.Windows.Forms.Label();
            this.label_Change_NR_Of_Cages = new System.Windows.Forms.Label();
            this.label_Change_Cage_Cap = new System.Windows.Forms.Label();
            this.label_Import_Clientele = new System.Windows.Forms.Label();
            this.label_Custom = new System.Windows.Forms.Label();
            this.label_PaulStandard = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_Initial_Head
            // 
            this.label_Initial_Head.AutoSize = true;
            this.label_Initial_Head.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_Initial_Head.Location = new System.Drawing.Point(67, 30);
            this.label_Initial_Head.Name = "label_Initial_Head";
            this.label_Initial_Head.Size = new System.Drawing.Size(288, 19);
            this.label_Initial_Head.TabIndex = 0;
            this.label_Initial_Head.Text = "Choose the settings for the simulation";
            this.label_Initial_Head.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox_Change_EXArea_cap);
            this.panel1.Controls.Add(this.checkBox_Change_ExArea_cap);
            this.panel1.Controls.Add(this.textBox_set_nr_of_days);
            this.panel1.Controls.Add(this.textBox_Set_ticks_Per_Second);
            this.panel1.Controls.Add(this.checkBox_Change_number_Of_ExAreas);
            this.panel1.Controls.Add(this.textBox_Change_nr_of_exAreas);
            this.panel1.Controls.Add(this.textBox_Change_number_of_cages);
            this.panel1.Controls.Add(this.textBox_set_Fictional_Date);
            this.panel1.Controls.Add(this.textBox_Cage_cap);
            this.panel1.Controls.Add(this.checkBox_Cange_nr_Of_exAreas);
            this.panel1.Controls.Add(this.checkBox_Change_nr_of_cages);
            this.panel1.Controls.Add(this.Button_Submit);
            this.panel1.Controls.Add(this.checkBox_Import_csv);
            this.panel1.Controls.Add(this.radioButton_custom);
            this.panel1.Controls.Add(this.radioButton_Paulstandard);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(272, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(238, 752);
            this.panel1.TabIndex = 2;
            // 
            // textBox_Change_EXArea_cap
            // 
            this.textBox_Change_EXArea_cap.Location = new System.Drawing.Point(20, 397);
            this.textBox_Change_EXArea_cap.Name = "textBox_Change_EXArea_cap";
            this.textBox_Change_EXArea_cap.Size = new System.Drawing.Size(100, 22);
            this.textBox_Change_EXArea_cap.TabIndex = 16;
            // 
            // checkBox_Change_ExArea_cap
            // 
            this.checkBox_Change_ExArea_cap.AutoSize = true;
            this.checkBox_Change_ExArea_cap.Location = new System.Drawing.Point(0, 403);
            this.checkBox_Change_ExArea_cap.Name = "checkBox_Change_ExArea_cap";
            this.checkBox_Change_ExArea_cap.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Change_ExArea_cap.TabIndex = 15;
            this.checkBox_Change_ExArea_cap.UseVisualStyleBackColor = true;
            // 
            // textBox_set_nr_of_days
            // 
            this.textBox_set_nr_of_days.Location = new System.Drawing.Point(20, 497);
            this.textBox_set_nr_of_days.Name = "textBox_set_nr_of_days";
            this.textBox_set_nr_of_days.Size = new System.Drawing.Size(100, 22);
            this.textBox_set_nr_of_days.TabIndex = 14;
            // 
            // textBox_Set_ticks_Per_Second
            // 
            this.textBox_Set_ticks_Per_Second.Location = new System.Drawing.Point(20, 547);
            this.textBox_Set_ticks_Per_Second.Name = "textBox_Set_ticks_Per_Second";
            this.textBox_Set_ticks_Per_Second.Size = new System.Drawing.Size(100, 22);
            this.textBox_Set_ticks_Per_Second.TabIndex = 13;
            // 
            // checkBox_Change_number_Of_ExAreas
            // 
            this.checkBox_Change_number_Of_ExAreas.AutoSize = true;
            this.checkBox_Change_number_Of_ExAreas.Location = new System.Drawing.Point(0, 353);
            this.checkBox_Change_number_Of_ExAreas.Name = "checkBox_Change_number_Of_ExAreas";
            this.checkBox_Change_number_Of_ExAreas.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Change_number_Of_ExAreas.TabIndex = 12;
            this.checkBox_Change_number_Of_ExAreas.UseVisualStyleBackColor = true;
            // 
            // textBox_Change_nr_of_exAreas
            // 
            this.textBox_Change_nr_of_exAreas.Location = new System.Drawing.Point(20, 347);
            this.textBox_Change_nr_of_exAreas.Name = "textBox_Change_nr_of_exAreas";
            this.textBox_Change_nr_of_exAreas.Size = new System.Drawing.Size(100, 22);
            this.textBox_Change_nr_of_exAreas.TabIndex = 11;
            // 
            // textBox_Change_number_of_cages
            // 
            this.textBox_Change_number_of_cages.Location = new System.Drawing.Point(20, 297);
            this.textBox_Change_number_of_cages.Name = "textBox_Change_number_of_cages";
            this.textBox_Change_number_of_cages.Size = new System.Drawing.Size(100, 22);
            this.textBox_Change_number_of_cages.TabIndex = 10;
            // 
            // textBox_set_Fictional_Date
            // 
            this.textBox_set_Fictional_Date.Location = new System.Drawing.Point(20, 447);
            this.textBox_set_Fictional_Date.Name = "textBox_set_Fictional_Date";
            this.textBox_set_Fictional_Date.Size = new System.Drawing.Size(100, 22);
            this.textBox_set_Fictional_Date.TabIndex = 9;
            // 
            // textBox_Cage_cap
            // 
            this.textBox_Cage_cap.Location = new System.Drawing.Point(20, 247);
            this.textBox_Cage_cap.Name = "textBox_Cage_cap";
            this.textBox_Cage_cap.ReadOnly = true;
            this.textBox_Cage_cap.Size = new System.Drawing.Size(100, 22);
            this.textBox_Cage_cap.TabIndex = 7;
            this.textBox_Cage_cap.ReadOnlyChanged += new System.EventHandler(this.textBox_Cage_cap_ReadOnlyChanged);
            // 
            // checkBox_Cange_nr_Of_exAreas
            // 
            this.checkBox_Cange_nr_Of_exAreas.AutoSize = true;
            this.checkBox_Cange_nr_Of_exAreas.Location = new System.Drawing.Point(0, 253);
            this.checkBox_Cange_nr_Of_exAreas.Name = "checkBox_Cange_nr_Of_exAreas";
            this.checkBox_Cange_nr_Of_exAreas.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Cange_nr_Of_exAreas.TabIndex = 6;
            this.checkBox_Cange_nr_Of_exAreas.UseVisualStyleBackColor = true;
            this.checkBox_Cange_nr_Of_exAreas.CheckedChanged += new System.EventHandler(this.textBox_Cage_cap_ReadOnlyChanged);
            // 
            // checkBox_Change_nr_of_cages
            // 
            this.checkBox_Change_nr_of_cages.AutoSize = true;
            this.checkBox_Change_nr_of_cages.Location = new System.Drawing.Point(0, 303);
            this.checkBox_Change_nr_of_cages.Name = "checkBox_Change_nr_of_cages";
            this.checkBox_Change_nr_of_cages.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Change_nr_of_cages.TabIndex = 5;
            this.checkBox_Change_nr_of_cages.UseVisualStyleBackColor = true;
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
            this.Button_Submit.Location = new System.Drawing.Point(0, 659);
            this.Button_Submit.Name = "Button_Submit";
            this.Button_Submit.Size = new System.Drawing.Size(238, 93);
            this.Button_Submit.TabIndex = 3;
            this.Button_Submit.Tag = "";
            this.Button_Submit.Text = "Submit";
            this.Button_Submit.UseVisualStyleBackColor = true;
            this.Button_Submit.Click += new System.EventHandler(this.Button_Submit_Click);
            // 
            // checkBox_Import_csv
            // 
            this.checkBox_Import_csv.AutoSize = true;
            this.checkBox_Import_csv.Location = new System.Drawing.Point(0, 203);
            this.checkBox_Import_csv.Name = "checkBox_Import_csv";
            this.checkBox_Import_csv.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Import_csv.TabIndex = 2;
            this.checkBox_Import_csv.UseVisualStyleBackColor = true;
            // 
            // radioButton_custom
            // 
            this.radioButton_custom.AutoSize = true;
            this.radioButton_custom.Location = new System.Drawing.Point(0, 153);
            this.radioButton_custom.Name = "radioButton_custom";
            this.radioButton_custom.Size = new System.Drawing.Size(14, 13);
            this.radioButton_custom.TabIndex = 1;
            this.radioButton_custom.TabStop = true;
            this.radioButton_custom.UseVisualStyleBackColor = true;
            this.radioButton_custom.CheckedChanged += new System.EventHandler(this.radioButton_Paulstandard_Click_Changed);
            // 
            // radioButton_Paulstandard
            // 
            this.radioButton_Paulstandard.AutoSize = true;
            this.radioButton_Paulstandard.Location = new System.Drawing.Point(0, 103);
            this.radioButton_Paulstandard.Name = "radioButton_Paulstandard";
            this.radioButton_Paulstandard.Size = new System.Drawing.Size(14, 13);
            this.radioButton_Paulstandard.TabIndex = 0;
            this.radioButton_Paulstandard.TabStop = true;
            this.radioButton_Paulstandard.UseVisualStyleBackColor = true;
            this.radioButton_Paulstandard.CheckedChanged += new System.EventHandler(this.radioButton_Cunstom_Click_Changed);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label_mandetory);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label_Set_Nr_Of_Days);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.label_Set_Date);
            this.panel3.Controls.Add(this.label_Change_Nr_ExAreas);
            this.panel3.Controls.Add(this.label_Change_NR_Of_Cages);
            this.panel3.Controls.Add(this.label_Change_Cage_Cap);
            this.panel3.Controls.Add(this.label_Import_Clientele);
            this.panel3.Controls.Add(this.label_Custom);
            this.panel3.Controls.Add(this.label_PaulStandard);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Font = new System.Drawing.Font("Century Gothic", 90F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(252, 752);
            this.panel3.TabIndex = 3;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // label_mandetory
            // 
            this.label_mandetory.AutoSize = true;
            this.label_mandetory.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.label_mandetory.Location = new System.Drawing.Point(12, 728);
            this.label_mandetory.Name = "label_mandetory";
            this.label_mandetory.Size = new System.Drawing.Size(221, 15);
            this.label_mandetory.TabIndex = 10;
            this.label_mandetory.Text = "* feilds are mandetory for new settings";
            this.label_mandetory.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(57, 400);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(195, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "Change Exersice Area Capacity";
            // 
            // label_Set_Nr_Of_Days
            // 
            this.label_Set_Nr_Of_Days.AutoSize = true;
            this.label_Set_Nr_Of_Days.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_Set_Nr_Of_Days.Location = new System.Drawing.Point(130, 500);
            this.label_Set_Nr_Of_Days.Name = "label_Set_Nr_Of_Days";
            this.label_Set_Nr_Of_Days.Size = new System.Drawing.Size(122, 16);
            this.label_Set_Nr_Of_Days.TabIndex = 8;
            this.label_Set_Nr_Of_Days.Text = "Set number of Days*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(134, 550);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Set Ticks / Second*";
            // 
            // label_Set_Date
            // 
            this.label_Set_Date.AutoSize = true;
            this.label_Set_Date.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_Set_Date.Location = new System.Drawing.Point(39, 450);
            this.label_Set_Date.Name = "label_Set_Date";
            this.label_Set_Date.Size = new System.Drawing.Size(214, 16);
            this.label_Set_Date.TabIndex = 6;
            this.label_Set_Date.Text = "Set fictional Start Date for simulation*";
            // 
            // label_Change_Nr_ExAreas
            // 
            this.label_Change_Nr_ExAreas.AutoSize = true;
            this.label_Change_Nr_ExAreas.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_Change_Nr_ExAreas.Location = new System.Drawing.Point(47, 350);
            this.label_Change_Nr_ExAreas.Name = "label_Change_Nr_ExAreas";
            this.label_Change_Nr_ExAreas.Size = new System.Drawing.Size(205, 16);
            this.label_Change_Nr_ExAreas.TabIndex = 5;
            this.label_Change_Nr_ExAreas.Text = "Change number of Exersice Areas";
            // 
            // label_Change_NR_Of_Cages
            // 
            this.label_Change_NR_Of_Cages.AutoSize = true;
            this.label_Change_NR_Of_Cages.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_Change_NR_Of_Cages.Location = new System.Drawing.Point(96, 300);
            this.label_Change_NR_Of_Cages.Name = "label_Change_NR_Of_Cages";
            this.label_Change_NR_Of_Cages.Size = new System.Drawing.Size(157, 16);
            this.label_Change_NR_Of_Cages.TabIndex = 4;
            this.label_Change_NR_Of_Cages.Text = "Change number of Cages";
            // 
            // label_Change_Cage_Cap
            // 
            this.label_Change_Cage_Cap.AutoSize = true;
            this.label_Change_Cage_Cap.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_Change_Cage_Cap.Location = new System.Drawing.Point(106, 250);
            this.label_Change_Cage_Cap.Name = "label_Change_Cage_Cap";
            this.label_Change_Cage_Cap.Size = new System.Drawing.Size(147, 16);
            this.label_Change_Cage_Cap.TabIndex = 3;
            this.label_Change_Cage_Cap.Text = "Change Cage Capacity";
            // 
            // label_Import_Clientele
            // 
            this.label_Import_Clientele.AutoSize = true;
            this.label_Import_Clientele.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_Import_Clientele.Location = new System.Drawing.Point(53, 200);
            this.label_Import_Clientele.Name = "label_Import_Clientele";
            this.label_Import_Clientele.Size = new System.Drawing.Size(199, 16);
            this.label_Import_Clientele.TabIndex = 2;
            this.label_Import_Clientele.Text = "Import Hamster Clientele from file";
            // 
            // label_Custom
            // 
            this.label_Custom.AutoSize = true;
            this.label_Custom.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_Custom.Location = new System.Drawing.Point(202, 150);
            this.label_Custom.Name = "label_Custom";
            this.label_Custom.Size = new System.Drawing.Size(50, 16);
            this.label_Custom.TabIndex = 1;
            this.label_Custom.Text = "Custom";
            // 
            // label_PaulStandard
            // 
            this.label_PaulStandard.AutoSize = true;
            this.label_PaulStandard.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_PaulStandard.Location = new System.Drawing.Point(166, 100);
            this.label_PaulStandard.Name = "label_PaulStandard";
            this.label_PaulStandard.Size = new System.Drawing.Size(87, 16);
            this.label_PaulStandard.TabIndex = 0;
            this.label_PaulStandard.Text = "Paul Standard";
            this.label_PaulStandard.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // Form_Settings
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(23)))), ((int)(((byte)(26)))));
            this.ClientSize = new System.Drawing.Size(510, 752);
            this.Controls.Add(this.label_Initial_Head);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(182)))), ((int)(((byte)(199)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form_Settings";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private Panel panel1;
        private RadioButton radioButton_custom;
        private RadioButton radioButton_Paulstandard;
        private CheckBox checkBox_Import_Hamsters;
        private Button Button_Submit;
        private Label label_Initial_Head;
        private CheckBox checkBox_Import_csv;
        private CheckBox checkBox_Change_nr_of_cages;
        private CheckBox checkBox_Cange_nr_Of_exAreas;
        private TextBox textBox_Cage_cap;
        private TextBox textBox_Change_nr_of_exAreas;
        private TextBox textBox_Change_number_of_cages;
        private TextBox textBox_set_Fictional_Date;
        private Panel panel3;
        private Label label_PaulStandard;
        private CheckBox checkBox_Change_number_Of_ExAreas;
        private Label label_Change_Nr_ExAreas;
        private Label label_Change_NR_Of_Cages;
        private Label label_Change_Cage_Cap;
        private Label label_Import_Clientele;
        private Label label_Custom;
        private TextBox textBox_Set_ticks_Per_Second;
        private Label label1;
        private Label label_Set_Date;
        private TextBox textBox_set_nr_of_days;
        private Label label_Set_Nr_Of_Days;
        private TextBox textBox_Change_EXArea_cap;
        private CheckBox checkBox_Change_ExArea_cap;
        private Label label2;
        private Label label_mandetory;
    }
}

