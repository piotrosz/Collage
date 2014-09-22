using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Collage.Engine;

namespace Collage
{
    using System.IO;
    using System.Linq;

    public partial class Form1 : Form
    {
        private readonly List<FileInfo> imagesList = new List<FileInfo>();
        private string outputDirectory = "";

        private CollageEngine collage;

        public Form1()
        {
            InitializeComponent();
        }

        // Shows dialog with .jpg images selection
        private void bntChooseDir_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            imagesList.AddRange(openFileDialog1.FileNames.Select(name => new FileInfo(name)));
            ShowInformation(string.Format("Number of images selected {0}.", imagesList.Count));
            
            btnCollage.Enabled = imagesList.Count > 0;
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

            var settings =
                new CollageSettings(
                    new CollageDimensionSettings
                        {
                            NumberOfColumns = Convert.ToInt32(nudColumns.Value),
                            NumberOfRows = Convert.ToInt32(nudRows.Value),
                            TileHeight = Convert.ToInt32(nudItemHeight.Value),
                            TileWidth = Convert.ToInt32(nudItemWidth.Value),
                            TileScalePercent =new Percentage(Convert.ToInt32(nudScalePercent.Value))
                        },
                        new AdditionalCollageSettings
                            {
                                ConvertToGrayscale = false,
                                RotateAndFlipRandomly = cbRotateAndFlip.Checked
                                
                            }, 
                        imagesList,
                        new DirectoryInfo(folderBrowserDialog1.SelectedPath)
                    );

            CreateCollage(settings);
        }

        private void ShowInformation(string message)
        {
            listBox1.Items.Add(message);
        }

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
            this.collage = new CollageEngine(settings);
            
            collage.CreateCompleted += this.collage_CreateCompleted;
            collage.CreateProgressChanged += this.collage_CreateProgressChanged;

            collage.CreateAsync();
        }

        void collage_CreateProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        void collage_CreateCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.ShowInformation(e.Cancelled ? "Cancelled" : "Done");

            if (e.UserState != null)
            {
                ShowInformation(e.UserState.ToString());
            }
                
            progressBar1.Value = 0;
            EnableControls();
        }
    }
}