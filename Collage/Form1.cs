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
    //http://www.csharp-examples.net/create-asynchronous-method/

    public partial class Form1 : Form
    {
        private List<string> imagesList = new List<string>();
        private string outputDirectory = "";

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
            //btnCancel.Enabled = true;

            var collageSettings = new CollageSettings
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

            backgroundWorker1.RunWorkerAsync(collageSettings);
        }

        // Shows the information for the user
        private void ShowInformation(string message)
        {
            listBox1.Items.Add(message);
        }

        // Cancels collage creation
        private void btnCancel_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();

            //btnCancel.Enabled = false;
            EnableControls();
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
        }

        // Creates collage using background worker
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;

            e.Result = CreateCollage((CollageSettings)e.Argument, worker, e);
        }

        // Report colllage creation progress
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        //    this.progressBar1.Value = e.ProgressPercentage;
        }

        // Executed when background worker finished its work
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                ShowInformation("Canceled");
            }
            else
            {
                // success
                ShowInformation("Image saved: " + e.Result.ToString());
            }

            EnableControls();
            //btnCancel.Enabled = false;
        }

        private string CreateCollage(CollageSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            string result = "";

            //if (worker.CancellationPending)
            //{
            //    e.Cancel = true;
            //}
            //else
            //{
                var collage = new CollageEngine(settings);

                //collage.ProgressChanged += new CollageEngine.CollageProgressChangedEventHandler(backgroundWorker1_ProgressChanged);

                result = collage.Create();
            //}

            return result;
        }
    }
}