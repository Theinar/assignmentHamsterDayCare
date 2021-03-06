
namespace UIWindows
{
    partial class Form_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_Run_Simulation = new System.Windows.Forms.Button();
            this.button_tick = new System.Windows.Forms.Button();
            this.button_Owner = new System.Windows.Forms.Button();
            this.Button_Hamster = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button_Settings = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.button_stopSimulation = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label0 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button_Show_EndReport = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.MainReport_Test_Values = new System.Windows.Forms.Label();
            this.MainReport_Test_Types = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_Run_Simulation);
            this.panel1.Controls.Add(this.button_tick);
            this.panel1.Controls.Add(this.button_Owner);
            this.panel1.Controls.Add(this.Button_Hamster);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(217, 734);
            this.panel1.TabIndex = 0;
            // 
            // button_Run_Simulation
            // 
            this.button_Run_Simulation.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_Run_Simulation.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_Run_Simulation.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.button_Run_Simulation.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(57)))), ((int)(((byte)(75)))));
            this.button_Run_Simulation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Run_Simulation.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button_Run_Simulation.Image = ((System.Drawing.Image)(resources.GetObject("button_Run_Simulation.Image")));
            this.button_Run_Simulation.Location = new System.Drawing.Point(0, 520);
            this.button_Run_Simulation.Name = "button_Run_Simulation";
            this.button_Run_Simulation.Size = new System.Drawing.Size(217, 222);
            this.button_Run_Simulation.TabIndex = 3;
            this.button_Run_Simulation.Tag = "";
            this.button_Run_Simulation.Text = "Run Simulation";
            this.button_Run_Simulation.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button_Run_Simulation.UseVisualStyleBackColor = true;
            this.button_Run_Simulation.Click += new System.EventHandler(this.button_Run_Simulation_Click);
            // 
            // button_tick
            // 
            this.button_tick.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_tick.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_tick.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.button_tick.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(57)))), ((int)(((byte)(75)))));
            this.button_tick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_tick.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button_tick.Image = ((System.Drawing.Image)(resources.GetObject("button_tick.Image")));
            this.button_tick.Location = new System.Drawing.Point(0, 400);
            this.button_tick.Name = "button_tick";
            this.button_tick.Size = new System.Drawing.Size(217, 120);
            this.button_tick.TabIndex = 2;
            this.button_tick.Tag = "";
            this.button_tick.Text = "Tick";
            this.button_tick.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button_tick.UseVisualStyleBackColor = true;
            this.button_tick.Click += new System.EventHandler(this.button_tick_Click);
            // 
            // button_Owner
            // 
            this.button_Owner.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_Owner.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_Owner.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.button_Owner.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(57)))), ((int)(((byte)(75)))));
            this.button_Owner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Owner.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button_Owner.Image = ((System.Drawing.Image)(resources.GetObject("button_Owner.Image")));
            this.button_Owner.Location = new System.Drawing.Point(0, 280);
            this.button_Owner.Name = "button_Owner";
            this.button_Owner.Size = new System.Drawing.Size(217, 120);
            this.button_Owner.TabIndex = 1;
            this.button_Owner.Tag = "";
            this.button_Owner.Text = "Cage";
            this.button_Owner.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button_Owner.UseVisualStyleBackColor = true;
            this.button_Owner.Click += new System.EventHandler(this.button_Owner_Click_1);
            // 
            // Button_Hamster
            // 
            this.Button_Hamster.Dock = System.Windows.Forms.DockStyle.Top;
            this.Button_Hamster.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Button_Hamster.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.Button_Hamster.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(57)))), ((int)(((byte)(75)))));
            this.Button_Hamster.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Hamster.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Button_Hamster.Image = ((System.Drawing.Image)(resources.GetObject("Button_Hamster.Image")));
            this.Button_Hamster.Location = new System.Drawing.Point(0, 160);
            this.Button_Hamster.Name = "Button_Hamster";
            this.Button_Hamster.Size = new System.Drawing.Size(217, 120);
            this.Button_Hamster.TabIndex = 0;
            this.Button_Hamster.Tag = "";
            this.Button_Hamster.Text = "Hamster";
            this.Button_Hamster.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Button_Hamster.UseVisualStyleBackColor = true;
            this.Button_Hamster.Click += new System.EventHandler(this.button_Hamster_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button_Settings);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(217, 160);
            this.panel3.TabIndex = 0;
            // 
            // button_Settings
            // 
            this.button_Settings.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_Settings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.button_Settings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(57)))), ((int)(((byte)(75)))));
            this.button_Settings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Settings.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button_Settings.Image = ((System.Drawing.Image)(resources.GetObject("button_Settings.Image")));
            this.button_Settings.Location = new System.Drawing.Point(0, 40);
            this.button_Settings.Name = "button_Settings";
            this.button_Settings.Size = new System.Drawing.Size(217, 120);
            this.button_Settings.TabIndex = 3;
            this.button_Settings.Tag = "";
            this.button_Settings.Text = "Settings";
            this.button_Settings.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button_Settings.UseVisualStyleBackColor = true;
            this.button_Settings.Click += new System.EventHandler(this.button_Settings_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(217, 441);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(887, 293);
            this.panel2.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(23)))), ((int)(((byte)(26)))));
            this.panel5.Controls.Add(this.button_stopSimulation);
            this.panel5.Controls.Add(this.panel7);
            this.panel5.Location = new System.Drawing.Point(7, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(868, 268);
            this.panel5.TabIndex = 3;
            // 
            // button_stopSimulation
            // 
            this.button_stopSimulation.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_stopSimulation.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.button_stopSimulation.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(57)))), ((int)(((byte)(75)))));
            this.button_stopSimulation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_stopSimulation.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button_stopSimulation.Image = ((System.Drawing.Image)(resources.GetObject("button_stopSimulation.Image")));
            this.button_stopSimulation.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button_stopSimulation.Location = new System.Drawing.Point(652, 156);
            this.button_stopSimulation.Name = "button_stopSimulation";
            this.button_stopSimulation.Size = new System.Drawing.Size(217, 112);
            this.button_stopSimulation.TabIndex = 2;
            this.button_stopSimulation.Tag = "";
            this.button_stopSimulation.Text = "Stop Simulation";
            this.button_stopSimulation.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button_stopSimulation.UseVisualStyleBackColor = true;
            this.button_stopSimulation.Visible = false;
            this.button_stopSimulation.Click += new System.EventHandler(this.button_stopSimulation_Click);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.label2);
            this.panel7.Controls.Add(this.label1);
            this.panel7.Controls.Add(this.label0);
            this.panel7.Location = new System.Drawing.Point(15, 19);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(837, 232);
            this.panel7.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(312, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "1";
            // 
            // label0
            // 
            this.label0.AutoSize = true;
            this.label0.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label0.Location = new System.Drawing.Point(2, 0);
            this.label0.Name = "label0";
            this.label0.Size = new System.Drawing.Size(157, 18);
            this.label0.TabIndex = 0;
            this.label0.Text = "User Choise Window";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(23)))), ((int)(((byte)(26)))));
            this.panel4.Controls.Add(this.button_Show_EndReport);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Location = new System.Drawing.Point(223, 12);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(869, 414);
            this.panel4.TabIndex = 2;
            // 
            // button_Show_EndReport
            // 
            this.button_Show_EndReport.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_Show_EndReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.button_Show_EndReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(57)))), ((int)(((byte)(75)))));
            this.button_Show_EndReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Show_EndReport.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button_Show_EndReport.Image = ((System.Drawing.Image)(resources.GetObject("button_Show_EndReport.Image")));
            this.button_Show_EndReport.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button_Show_EndReport.Location = new System.Drawing.Point(652, 302);
            this.button_Show_EndReport.Name = "button_Show_EndReport";
            this.button_Show_EndReport.Size = new System.Drawing.Size(217, 112);
            this.button_Show_EndReport.TabIndex = 3;
            this.button_Show_EndReport.Tag = "";
            this.button_Show_EndReport.Text = "Show End Report";
            this.button_Show_EndReport.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button_Show_EndReport.UseVisualStyleBackColor = true;
            this.button_Show_EndReport.Visible = false;
            this.button_Show_EndReport.VisibleChanged += new System.EventHandler(this.button_Show_EndReport_VisibleChanged);
            this.button_Show_EndReport.Click += new System.EventHandler(this.button_Show_EndReport_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.MainReport_Test_Values);
            this.panel6.Controls.Add(this.MainReport_Test_Types);
            this.panel6.Location = new System.Drawing.Point(16, 20);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(837, 378);
            this.panel6.TabIndex = 4;
            // 
            // MainReport_Test_Values
            // 
            this.MainReport_Test_Values.AutoSize = true;
            this.MainReport_Test_Values.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.MainReport_Test_Values.Location = new System.Drawing.Point(458, 2);
            this.MainReport_Test_Values.Name = "MainReport_Test_Values";
            this.MainReport_Test_Values.Size = new System.Drawing.Size(0, 18);
            this.MainReport_Test_Values.TabIndex = 1;
            // 
            // MainReport_Test_Types
            // 
            this.MainReport_Test_Types.AutoSize = true;
            this.MainReport_Test_Types.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.MainReport_Test_Types.Location = new System.Drawing.Point(0, 0);
            this.MainReport_Test_Types.Name = "MainReport_Test_Types";
            this.MainReport_Test_Types.Size = new System.Drawing.Size(0, 18);
            this.MainReport_Test_Types.TabIndex = 0;
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(30)))), ((int)(((byte)(33)))));
            this.ClientSize = new System.Drawing.Size(1104, 734);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(182)))), ((int)(((byte)(199)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form_Main";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Button_Hamster;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button_tick;
        private System.Windows.Forms.Button button_Run_Simulation;
        private System.Windows.Forms.Button button_Settings;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label0;
        private System.Windows.Forms.Label MainReport_Test_Types;
        private System.Windows.Forms.Button button_stopSimulation;
        private System.Windows.Forms.Button button_Show_EndReport;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label MainReport_Test_Values;
        private System.Windows.Forms.Button button_Owner;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

