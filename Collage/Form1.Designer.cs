namespace Collage
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
            this.bntChooseDir = new System.Windows.Forms.Button();
            this.btnCollage = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cbCropAndFlip = new System.Windows.Forms.CheckBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnSelectOutputDir = new System.Windows.Forms.Button();
            this.nudColumns = new System.Windows.Forms.NumericUpDown();
            this.nudRows = new System.Windows.Forms.NumericUpDown();
            this.nudItemHeight = new System.Windows.Forms.NumericUpDown();
            this.nudItemWidth = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudItemHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudItemWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // bntChooseDir
            // 
            this.bntChooseDir.AutoEllipsis = true;
            this.bntChooseDir.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.bntChooseDir.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.bntChooseDir.Location = new System.Drawing.Point(3, 141);
            this.bntChooseDir.Name = "bntChooseDir";
            this.bntChooseDir.Size = new System.Drawing.Size(71, 25);
            this.bntChooseDir.TabIndex = 0;
            this.bntChooseDir.Text = "1. Add files";
            this.bntChooseDir.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.bntChooseDir.UseVisualStyleBackColor = false;
            this.bntChooseDir.Click += new System.EventHandler(this.bntChooseDir_Click);
            // 
            // btnCollage
            // 
            this.btnCollage.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnCollage.Location = new System.Drawing.Point(216, 142);
            this.btnCollage.Name = "btnCollage";
            this.btnCollage.Size = new System.Drawing.Size(98, 24);
            this.btnCollage.TabIndex = 1;
            this.btnCollage.Text = "3. Create collage";
            this.btnCollage.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCollage.UseVisualStyleBackColor = false;
            this.btnCollage.Click += new System.EventHandler(this.btnCollage_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(3, 2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(311, 134);
            this.listBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(110, 176);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Tile width:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(106, 200);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tile height:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Rows:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Columns:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(214, 176);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "px";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(214, 200);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "px";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "(*.jpg)|*jpg";
            this.openFileDialog1.Multiselect = true;
            // 
            // cbCropAndFlip
            // 
            this.cbCropAndFlip.AutoSize = true;
            this.cbCropAndFlip.Location = new System.Drawing.Point(6, 224);
            this.cbCropAndFlip.Name = "cbCropAndFlip";
            this.cbCropAndFlip.Size = new System.Drawing.Size(116, 17);
            this.cbCropAndFlip.TabIndex = 14;
            this.cbCropAndFlip.Text = "Rotate and flip tiles";
            this.cbCropAndFlip.UseVisualStyleBackColor = true;
            // 
            // btnSelectOutputDir
            // 
            this.btnSelectOutputDir.Location = new System.Drawing.Point(80, 142);
            this.btnSelectOutputDir.Name = "btnSelectOutputDir";
            this.btnSelectOutputDir.Size = new System.Drawing.Size(130, 24);
            this.btnSelectOutputDir.TabIndex = 15;
            this.btnSelectOutputDir.Text = "2. Specify output folder";
            this.btnSelectOutputDir.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSelectOutputDir.UseVisualStyleBackColor = true;
            this.btnSelectOutputDir.Click += new System.EventHandler(this.btnSelectOutputDir_Click);
            // 
            // nudColumns
            // 
            this.nudColumns.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::Collage.Properties.Settings.Default, "DefaultColsNo", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.nudColumns.Location = new System.Drawing.Point(59, 198);
            this.nudColumns.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudColumns.Name = "nudColumns";
            this.nudColumns.Size = new System.Drawing.Size(45, 20);
            this.nudColumns.TabIndex = 10;
            this.nudColumns.Value = global::Collage.Properties.Settings.Default.DefaultColsNo;
            // 
            // nudRows
            // 
            this.nudRows.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::Collage.Properties.Settings.Default, "DefaultRowsNo", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.nudRows.Location = new System.Drawing.Point(59, 174);
            this.nudRows.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRows.Name = "nudRows";
            this.nudRows.Size = new System.Drawing.Size(45, 20);
            this.nudRows.TabIndex = 9;
            this.nudRows.Value = global::Collage.Properties.Settings.Default.DefaultRowsNo;
            // 
            // nudItemHeight
            // 
            this.nudItemHeight.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::Collage.Properties.Settings.Default, "DefaultItemHeight", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.nudItemHeight.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudItemHeight.Location = new System.Drawing.Point(170, 198);
            this.nudItemHeight.Maximum = new decimal(new int[] {
            800,
            0,
            0,
            0});
            this.nudItemHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudItemHeight.Name = "nudItemHeight";
            this.nudItemHeight.Size = new System.Drawing.Size(44, 20);
            this.nudItemHeight.TabIndex = 6;
            this.nudItemHeight.Value = global::Collage.Properties.Settings.Default.DefaultItemHeight;
            // 
            // nudItemWidth
            // 
            this.nudItemWidth.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::Collage.Properties.Settings.Default, "DefaultItemWidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.nudItemWidth.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudItemWidth.Location = new System.Drawing.Point(170, 174);
            this.nudItemWidth.Maximum = new decimal(new int[] {
            800,
            0,
            0,
            0});
            this.nudItemWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudItemWidth.Name = "nudItemWidth";
            this.nudItemWidth.Size = new System.Drawing.Size(44, 20);
            this.nudItemWidth.TabIndex = 3;
            this.nudItemWidth.Value = global::Collage.Properties.Settings.Default.DefaultItemWidth;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ClientSize = new System.Drawing.Size(319, 248);
            this.Controls.Add(this.btnSelectOutputDir);
            this.Controls.Add(this.cbCropAndFlip);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nudColumns);
            this.Controls.Add(this.nudRows);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nudItemHeight);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudItemWidth);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnCollage);
            this.Controls.Add(this.bntChooseDir);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Collage";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudItemHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudItemWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bntChooseDir;
        private System.Windows.Forms.Button btnCollage;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.NumericUpDown nudItemWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudItemHeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudRows;
        private System.Windows.Forms.NumericUpDown nudColumns;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox cbCropAndFlip;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnSelectOutputDir;
    }
}

