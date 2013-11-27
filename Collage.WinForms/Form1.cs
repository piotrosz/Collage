using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Collage.Engine;

namespace Collage
{
    public partial class Form1 : Form
    {
        private List<string> imagesList = new List<string>();
        private string outputDirectory = "";
        private CollageEngine collage = new CollageEngine();

        public Form1()
        {
            InitializeComponent();
        }

        // Shows dialog with .jpg images selection
        private void bntChooseDir_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            imagesList.AddRange(openFileDialog1.FileNames);
            ShowInformation(string.Format("Number of images selected {0}.", imagesList.Count));

            if (imagesList.Count > 0)
                btnCollage.Enabled = true;
        }

        // Shows dialog with output directory selection
        private void btnSelectOutputDir_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.ShowDialog();
            this.outputDirectory = folderBrowserDialog1.SelectedPath;
            ShowInformation(string.Format("Selected output path: {0}.", this.outputDirectory));
        }

        private void btnCollage_Click(object sender, EventArgs e)
        {
            if (imagesList == null || imagesList.Count == 0)
            {
                ShowInformation("No images selected.");
                return;
            }

            ShowInformation("Work in progress...");
            DisableControls();

            var settings = new CollageSettings
            {
                InputImages = imagesList,
                NumberOfColumns = Convert.ToInt32(nudColumns.Value),
                NumberOfRows = Convert.ToInt32(nudRows.Value),
                TileHeight = Convert.ToInt32(nudItemHeight.Value),
                TileWidth = Convert.ToInt32(nudItemWidth.Value),
                RotateAndFlipRandomly = cbRotateAndFlip.Checked,
                OutputDirectory = folderBrowserDialog1.SelectedPath,
                ScalePercent = Convert.ToInt32(nudScalePercent.Value),
            };

            CreateCollage(settings);
        }

        // Shows the information for the user
        private void ShowInformation(string message)
        {
            listBox1.Items.Add(message);
        }

        // Cancels collage creation
        private void btnCancel_Click(object sender, EventArgs e)
        {
            collage.CancelAsync();
            EnableControls();
            progressBar1.Value = 0;
        }

        private void DisableControls()
        {
            btnChooseFiles.Enabled = false;
            btnSelectOutputDir.Enabled = false;
            btnCollage.Enabled = false;
            nudColumns.Enabled = false;
            nudItemHeight.Enabled = false;
            nudItemWidth.Enabled = false;
            nudRows.Enabled = false;
            nudScalePercent.Enabled = false;
            cbRotateAndFlip.Enabled = false;

            btnCancel.Enabled = true;
        }

        private void EnableControls()
        {
            btnChooseFiles.Enabled = true;
            btnSelectOutputDir.Enabled = true;
            btnCollage.Enabled = true;
            nudColumns.Enabled = true;
            nudItemHeight.Enabled = true;
            nudItemWidth.Enabled = true;
            nudRows.Enabled = true;
            nudScalePercent.Enabled = true;
            cbRotateAndFlip.Enabled = true;

            btnCancel.Enabled = false;
        }

        private void CreateCollage(CollageSettings settings)
        {
            collage.Settings = settings;

            collage.CreateCompleted += new AsyncCompletedEventHandler(collage_CreateCompleted);
            collage.CreateProgressChanged += new EventHandler<ProgressChangedEventArgs>(collage_CreateProgressChanged);

            collage.CreateAsync();
        }

        void collage_CreateProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        void collage_CreateCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
                ShowInformation("Cancelled");
            else
                ShowInformation("Done");

            if (e.UserState != null)
                ShowInformation(e.UserState.ToString());

            progressBar1.Value = 0;
            EnableControls();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //var settings = new CollageSettings
            //{
            //    InputImages = new List<string>() { @"D:\test\Tulips.jpg", @"D:\test\Penguins.jpg" },
            //    NumberOfColumns = 3,
            //    NumberOfRows = 3,
            //    TileHeight = 100,
            //    TileWidth = 100,
            //    RotateAndFlipRandomly = true,
            //    OutputDirectory = @"D:\test",
            //    ScalePercent = 50,
            //};

            //CreateCollage(settings);

            //collage.CancelAsync();
        }
    }
}