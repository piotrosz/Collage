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
            this.btnChooseFiles = new System.Windows.Forms.Button();
            this.btnCollage = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cbRotateAndFlip = new System.Windows.Forms.CheckBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnSelectOutputDir = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.nudScalePercent = new System.Windows.Forms.NumericUpDown();
            this.nudColumns = new System.Windows.Forms.NumericUpDown();
            this.nudRows = new System.Windows.Forms.NumericUpDown();
            this.nudItemHeight = new System.Windows.Forms.NumericUpDown();
            this.nudItemWidth = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbCutRandomly = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudScalePercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudItemHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudItemWidth)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnChooseFiles
            // 
            this.btnChooseFiles.AutoEllipsis = true;
            this.btnChooseFiles.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnChooseFiles.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnChooseFiles.Location = new System.Drawing.Point(1, 141);
            this.btnChooseFiles.Name = "btnChooseFiles";
            this.btnChooseFiles.Size = new System.Drawing.Size(71, 25);
            this.btnChooseFiles.TabIndex = 0;
            this.btnChooseFiles.Text = "1. Add files";
            this.btnChooseFiles.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnChooseFiles.UseVisualStyleBackColor = false;
            this.btnChooseFiles.Click += new System.EventHandler(this.bntChooseDir_Click);
            // 
            // btnCollage
            // 
            this.btnCollage.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnCollage.Enabled = false;
            this.btnCollage.Location = new System.Drawing.Point(209, 142);
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
            this.listBox1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.listBox1.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(3, 2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(356, 134);
            this.listBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(113, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Tile width:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(109, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tile height:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Rows:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Columns:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(217, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "px";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(217, 46);
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
            // cbRotateAndFlip
            // 
            this.cbRotateAndFlip.AutoSize = true;
            this.cbRotateAndFlip.Location = new System.Drawing.Point(239, 35);
            this.cbRotateAndFlip.Name = "cbRotateAndFlip";
            this.cbRotateAndFlip.Size = new System.Drawing.Size(116, 17);
            this.cbRotateAndFlip.TabIndex = 14;
            this.cbRotateAndFlip.Text = "Rotate and flip tiles";
            this.cbRotateAndFlip.UseVisualStyleBackColor = true;
            // 
            // btnSelectOutputDir
            // 
            this.btnSelectOutputDir.Location = new System.Drawing.Point(75, 142);
            this.btnSelectOutputDir.Name = "btnSelectOutputDir";
            this.btnSelectOutputDir.Size = new System.Drawing.Size(130, 24);
            this.btnSelectOutputDir.TabIndex = 15;
            this.btnSelectOutputDir.Text = "2. Specify output folder";
            this.btnSelectOutputDir.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSelectOutputDir.UseVisualStyleBackColor = true;
            this.btnSelectOutputDir.Click += new System.EventHandler(this.btnSelectOutputDir_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(241, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Scale:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(326, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(15, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "%";
            // 
            // nudScalePercent
            // 
            this.nudScalePercent.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::Collage.Properties.Settings.Default, "DefaultItemWidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.nudScalePercent.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudScalePercent.Location = new System.Drawing.Point(281, 10);
            this.nudScalePercent.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudScalePercent.Name = "nudScalePercent";
            this.nudScalePercent.Size = new System.Drawing.Size(44, 20);
            this.nudScalePercent.TabIndex = 22;
            this.nudScalePercent.Value = global::Collage.Properties.Settings.Default.DefaultScalePercent;
            // 
            // nudColumns
            // 
            this.nudColumns.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::Collage.Properties.Settings.Default, "DefaultColsNo", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.nudColumns.Location = new System.Drawing.Point(62, 44);
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
            this.nudRows.Location = new System.Drawing.Point(62, 20);
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
            this.nudItemHeight.Location = new System.Drawing.Point(173, 44);
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
            this.nudItemWidth.Location = new System.Drawing.Point(173, 20);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbCutRandomly);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.nudItemWidth);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.nudScalePercent);
            this.groupBox1.Controls.Add(this.nudItemHeight);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.nudRows);
            this.groupBox1.Controls.Add(this.cbRotateAndFlip);
            this.groupBox1.Controls.Add(this.nudColumns);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(3, 172);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(357, 77);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parameters";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(3, 255);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(357, 19);
            this.progressBar1.TabIndex = 26;
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(311, 142);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(49, 23);
            this.btnCancel.TabIndex = 27;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbCutRandomly
            // 
            this.cbCutRandomly.AutoSize = true;
            this.cbCutRandomly.Location = new System.Drawing.Point(240, 54);
            this.cbCutRandomly.Name = "cbCutRandomly";
            this.cbCutRandomly.Size = new System.Drawing.Size(108, 17);
            this.cbCutRandomly.TabIndex = 23;
            this.cbCutRandomly.Text = "Cut tiles randomly";
            this.cbCutRandomly.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ClientSize = new System.Drawing.Size(365, 277);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSelectOutputDir);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnCollage);
            this.Controls.Add(this.btnChooseFiles);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Collage";
            ((System.ComponentModel.ISupportInitialize)(this.nudScalePercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudItemHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudItemWidth)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnChooseFiles;
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
        private System.Windows.Forms.CheckBox cbRotateAndFlip;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnSelectOutputDir;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudScalePercent;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cbCutRandomly;
    }
}

