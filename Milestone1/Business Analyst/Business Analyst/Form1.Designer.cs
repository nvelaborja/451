namespace Business_Analyst
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBoxZip = new System.Windows.Forms.ListBox();
            this.listBoxCity = new System.Windows.Forms.ListBox();
            this.boxState = new System.Windows.Forms.ComboBox();
            this.labelZip = new System.Windows.Forms.Label();
            this.labelCity = new System.Windows.Forms.Label();
            this.labelState = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label65 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.textBox24 = new System.Windows.Forms.TextBox();
            this.textBox44 = new System.Windows.Forms.TextBox();
            this.textBox65 = new System.Windows.Forms.TextBox();
            this.textBox64 = new System.Windows.Forms.TextBox();
            this.textBox18 = new System.Windows.Forms.TextBox();
            this.labelPercent = new System.Windows.Forms.Label();
            this.labelAge = new System.Windows.Forms.Label();
            this.textBoxAge = new System.Windows.Forms.TextBox();
            this.labelMedAge = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxIncome = new System.Windows.Forms.TextBox();
            this.textBoxPop = new System.Windows.Forms.TextBox();
            this.labelIncome = new System.Windows.Forms.Label();
            this.labelPop = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.listBoxZip);
            this.groupBox1.Controls.Add(this.listBoxCity);
            this.groupBox1.Controls.Add(this.boxState);
            this.groupBox1.Controls.Add(this.labelZip);
            this.groupBox1.Controls.Add(this.labelCity);
            this.groupBox1.Controls.Add(this.labelState);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(356, 417);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Business Location";
            // 
            // listBoxZip
            // 
            this.listBoxZip.BackColor = System.Drawing.SystemColors.InfoText;
            this.listBoxZip.Cursor = System.Windows.Forms.Cursors.Hand;
            this.listBoxZip.ForeColor = System.Drawing.SystemColors.Control;
            this.listBoxZip.FormattingEnabled = true;
            this.listBoxZip.Location = new System.Drawing.Point(96, 354);
            this.listBoxZip.Name = "listBoxZip";
            this.listBoxZip.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listBoxZip.ScrollAlwaysVisible = true;
            this.listBoxZip.Size = new System.Drawing.Size(249, 56);
            this.listBoxZip.TabIndex = 4;
            this.listBoxZip.SelectedValueChanged += new System.EventHandler(this.listBoxZip_SelectedValueChanged);
            // 
            // listBoxCity
            // 
            this.listBoxCity.BackColor = System.Drawing.SystemColors.InfoText;
            this.listBoxCity.Cursor = System.Windows.Forms.Cursors.Hand;
            this.listBoxCity.ForeColor = System.Drawing.SystemColors.Control;
            this.listBoxCity.FormattingEnabled = true;
            this.listBoxCity.Location = new System.Drawing.Point(96, 84);
            this.listBoxCity.Name = "listBoxCity";
            this.listBoxCity.ScrollAlwaysVisible = true;
            this.listBoxCity.Size = new System.Drawing.Size(249, 251);
            this.listBoxCity.TabIndex = 2;
            this.listBoxCity.SelectedValueChanged += new System.EventHandler(this.listBoxCity_SelectedValueChanged);
            // 
            // boxState
            // 
            this.boxState.BackColor = System.Drawing.SystemColors.ControlText;
            this.boxState.Cursor = System.Windows.Forms.Cursors.Hand;
            this.boxState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boxState.ForeColor = System.Drawing.SystemColors.Control;
            this.boxState.FormattingEnabled = true;
            this.boxState.Location = new System.Drawing.Point(96, 43);
            this.boxState.Name = "boxState";
            this.boxState.Size = new System.Drawing.Size(249, 21);
            this.boxState.TabIndex = 3;
            this.boxState.DropDown += new System.EventHandler(this.boxState_DropDown);
            this.boxState.SelectionChangeCommitted += new System.EventHandler(this.boxState_SelectionChangeCommitted);
            // 
            // labelZip
            // 
            this.labelZip.AutoSize = true;
            this.labelZip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelZip.Location = new System.Drawing.Point(21, 354);
            this.labelZip.Name = "labelZip";
            this.labelZip.Size = new System.Drawing.Size(65, 16);
            this.labelZip.TabIndex = 2;
            this.labelZip.Text = "Zipcode";
            // 
            // labelCity
            // 
            this.labelCity.AutoSize = true;
            this.labelCity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCity.Location = new System.Drawing.Point(21, 84);
            this.labelCity.Name = "labelCity";
            this.labelCity.Size = new System.Drawing.Size(34, 16);
            this.labelCity.TabIndex = 1;
            this.labelCity.Text = "City";
            // 
            // labelState
            // 
            this.labelState.AutoSize = true;
            this.labelState.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelState.Location = new System.Drawing.Point(21, 44);
            this.labelState.Name = "labelState";
            this.labelState.Size = new System.Drawing.Size(44, 16);
            this.labelState.TabIndex = 0;
            this.labelState.Text = "State";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox2.Location = new System.Drawing.Point(374, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(318, 417);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Demographics Summary";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label65);
            this.groupBox4.Controls.Add(this.label64);
            this.groupBox4.Controls.Add(this.label44);
            this.groupBox4.Controls.Add(this.label24);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.textBox24);
            this.groupBox4.Controls.Add(this.textBox44);
            this.groupBox4.Controls.Add(this.textBox65);
            this.groupBox4.Controls.Add(this.textBox64);
            this.groupBox4.Controls.Add(this.textBox18);
            this.groupBox4.Controls.Add(this.labelPercent);
            this.groupBox4.Controls.Add(this.labelAge);
            this.groupBox4.Controls.Add(this.textBoxAge);
            this.groupBox4.Controls.Add(this.labelMedAge);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBox4.Location = new System.Drawing.Point(6, 138);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(306, 273);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Age Distribution";
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label65.Location = new System.Drawing.Point(41, 188);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(80, 16);
            this.label65.TabIndex = 13;
            this.label65.Text = "65 and Over";
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label64.Location = new System.Drawing.Point(55, 157);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(53, 16);
            this.label64.TabIndex = 12;
            this.label64.Text = "45 to 64";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(55, 126);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(53, 16);
            this.label44.TabIndex = 11;
            this.label44.Text = "25 to 44";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(55, 95);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(53, 16);
            this.label24.TabIndex = 10;
            this.label24.Text = "18 to 24";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(50, 64);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(62, 16);
            this.label18.TabIndex = 9;
            this.label18.Text = "Under 18";
            // 
            // textBox24
            // 
            this.textBox24.BackColor = System.Drawing.SystemColors.InfoText;
            this.textBox24.ForeColor = System.Drawing.SystemColors.Control;
            this.textBox24.Location = new System.Drawing.Point(157, 92);
            this.textBox24.Name = "textBox24";
            this.textBox24.ReadOnly = true;
            this.textBox24.Size = new System.Drawing.Size(102, 22);
            this.textBox24.TabIndex = 8;
            this.textBox24.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox44
            // 
            this.textBox44.BackColor = System.Drawing.SystemColors.InfoText;
            this.textBox44.ForeColor = System.Drawing.SystemColors.Control;
            this.textBox44.Location = new System.Drawing.Point(157, 123);
            this.textBox44.Name = "textBox44";
            this.textBox44.ReadOnly = true;
            this.textBox44.Size = new System.Drawing.Size(102, 22);
            this.textBox44.TabIndex = 7;
            this.textBox44.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox65
            // 
            this.textBox65.BackColor = System.Drawing.SystemColors.InfoText;
            this.textBox65.ForeColor = System.Drawing.SystemColors.Control;
            this.textBox65.Location = new System.Drawing.Point(157, 185);
            this.textBox65.Name = "textBox65";
            this.textBox65.ReadOnly = true;
            this.textBox65.Size = new System.Drawing.Size(102, 22);
            this.textBox65.TabIndex = 6;
            this.textBox65.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox64
            // 
            this.textBox64.BackColor = System.Drawing.SystemColors.InfoText;
            this.textBox64.ForeColor = System.Drawing.SystemColors.Control;
            this.textBox64.Location = new System.Drawing.Point(157, 154);
            this.textBox64.Name = "textBox64";
            this.textBox64.ReadOnly = true;
            this.textBox64.Size = new System.Drawing.Size(102, 22);
            this.textBox64.TabIndex = 5;
            this.textBox64.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox18
            // 
            this.textBox18.BackColor = System.Drawing.SystemColors.InfoText;
            this.textBox18.ForeColor = System.Drawing.SystemColors.Control;
            this.textBox18.Location = new System.Drawing.Point(157, 61);
            this.textBox18.Name = "textBox18";
            this.textBox18.ReadOnly = true;
            this.textBox18.Size = new System.Drawing.Size(102, 22);
            this.textBox18.TabIndex = 4;
            this.textBox18.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelPercent
            // 
            this.labelPercent.AutoSize = true;
            this.labelPercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPercent.Location = new System.Drawing.Point(164, 29);
            this.labelPercent.Name = "labelPercent";
            this.labelPercent.Size = new System.Drawing.Size(88, 16);
            this.labelPercent.TabIndex = 3;
            this.labelPercent.Text = "Percentage";
            // 
            // labelAge
            // 
            this.labelAge.AutoSize = true;
            this.labelAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAge.Location = new System.Drawing.Point(63, 29);
            this.labelAge.Name = "labelAge";
            this.labelAge.Size = new System.Drawing.Size(36, 16);
            this.labelAge.TabIndex = 2;
            this.labelAge.Text = "Age";
            // 
            // textBoxAge
            // 
            this.textBoxAge.BackColor = System.Drawing.SystemColors.ControlText;
            this.textBoxAge.ForeColor = System.Drawing.SystemColors.Control;
            this.textBoxAge.Location = new System.Drawing.Point(107, 230);
            this.textBoxAge.Name = "textBoxAge";
            this.textBoxAge.ReadOnly = true;
            this.textBoxAge.Size = new System.Drawing.Size(193, 22);
            this.textBoxAge.TabIndex = 1;
            this.textBoxAge.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelMedAge
            // 
            this.labelMedAge.AutoSize = true;
            this.labelMedAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMedAge.Location = new System.Drawing.Point(8, 231);
            this.labelMedAge.Name = "labelMedAge";
            this.labelMedAge.Size = new System.Drawing.Size(91, 16);
            this.labelMedAge.TabIndex = 0;
            this.labelMedAge.Text = "Median Age";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxIncome);
            this.groupBox3.Controls.Add(this.textBoxPop);
            this.groupBox3.Controls.Add(this.labelIncome);
            this.groupBox3.Controls.Add(this.labelPop);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox3.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Location = new System.Drawing.Point(6, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(306, 113);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Zipcode Demographics";
            // 
            // textBoxIncome
            // 
            this.textBoxIncome.BackColor = System.Drawing.SystemColors.InfoText;
            this.textBoxIncome.ForeColor = System.Drawing.SystemColors.Control;
            this.textBoxIncome.Location = new System.Drawing.Point(107, 74);
            this.textBoxIncome.Name = "textBoxIncome";
            this.textBoxIncome.ReadOnly = true;
            this.textBoxIncome.Size = new System.Drawing.Size(193, 20);
            this.textBoxIncome.TabIndex = 3;
            this.textBoxIncome.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxPop
            // 
            this.textBoxPop.BackColor = System.Drawing.SystemColors.ControlText;
            this.textBoxPop.ForeColor = System.Drawing.SystemColors.Control;
            this.textBoxPop.Location = new System.Drawing.Point(107, 28);
            this.textBoxPop.Name = "textBoxPop";
            this.textBoxPop.ReadOnly = true;
            this.textBoxPop.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBoxPop.Size = new System.Drawing.Size(193, 20);
            this.textBoxPop.TabIndex = 2;
            this.textBoxPop.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelIncome
            // 
            this.labelIncome.AutoSize = true;
            this.labelIncome.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelIncome.Location = new System.Drawing.Point(6, 75);
            this.labelIncome.Name = "labelIncome";
            this.labelIncome.Size = new System.Drawing.Size(93, 16);
            this.labelIncome.TabIndex = 1;
            this.labelIncome.Text = "Avg. Income";
            // 
            // labelPop
            // 
            this.labelPop.AutoSize = true;
            this.labelPop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPop.Location = new System.Drawing.Point(6, 29);
            this.labelPop.Name = "labelPop";
            this.labelPop.Size = new System.Drawing.Size(82, 16);
            this.labelPop.TabIndex = 0;
            this.labelPop.Text = "Population";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(704, 441);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(720, 480);
            this.MinimumSize = new System.Drawing.Size(720, 480);
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Business Analyst";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox boxState;
        private System.Windows.Forms.Label labelState;
        private System.Windows.Forms.Label labelCity;
        private System.Windows.Forms.Label labelZip;
        private System.Windows.Forms.Label labelIncome;
        private System.Windows.Forms.Label labelPop;
        private System.Windows.Forms.TextBox textBoxIncome;
        private System.Windows.Forms.TextBox textBoxPop;
        private System.Windows.Forms.ListBox listBoxCity;
        private System.Windows.Forms.TextBox textBoxAge;
        private System.Windows.Forms.Label labelMedAge;
        private System.Windows.Forms.ListBox listBoxZip;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBox24;
        private System.Windows.Forms.TextBox textBox44;
        private System.Windows.Forms.TextBox textBox65;
        private System.Windows.Forms.TextBox textBox64;
        private System.Windows.Forms.TextBox textBox18;
        private System.Windows.Forms.Label labelPercent;
        private System.Windows.Forms.Label labelAge;
    }
}

