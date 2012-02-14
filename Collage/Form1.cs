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

        public Form1()
        {
            InitializeComponent();
        }

        private void bntChooseDir_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            imagesList.AddRange(openFileDialog1.FileNames);
            ShowInformation(string.Format("Number of images selected {0}.", imagesList.Count));
        }

        private void btnCollage_Click(object sender, EventArgs e)
        {
            if (imagesList != null && imagesList.Count > 0)
            {
                ShowInformation("You have to be patient.");
                var collageSettings = new CollageSettings
                {
                    InputImages = imagesList,
                    NumberOfColumns = Convert.ToInt32(nudColumns.Value),
                    NumberOfRows = Convert.ToInt32(nudRows.Value),
                    TileHeight = Convert.ToInt32(nudItemHeight.Value),
                    TileWidth = Convert.ToInt32(nudItemWidth.Value),
                    RotateAndFlipRandomly = cbCropAndFlip.Checked
                };

                var collage = new CollageEngine(collageSettings);
                collage.OutputDir = folderBrowserDialog1.SelectedPath;
                collage.CreateCollage();

                ShowInformation("Done.");
            }
            else
            {
                ShowInformation("No images selected.");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //if (CollageEngine.CreateTempDir())
            //    ShowInformation("Temp directory created.");
           
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //CollageEngine.DeleteTempFiles();
        }

        private void ShowInformation(string message)
        {
            listBox1.Items.Add(message);
        }

        private void btnSelectOutputDir_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.ShowDialog();
            this.outputDirectory = folderBrowserDialog1.SelectedPath;
            ShowInformation(string.Format("Selected output path: {0}.", this.outputDirectory));
        }
    }
}