using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Runtime.Remoting.Messaging;

namespace Collage.Engine
{
    internal class CreateCollageAsyncContext
    {
        private readonly object sync = new object();
        private bool isCancelling = false;

        public bool IsCancelling
        {
            get { lock (sync) { return isCancelling; } }
        }

        public void Cancel()
        {
            lock (sync) { isCancelling = true; }
        }
    }

    public class CollageEngine
    {
        private bool isBusy = false;
        public bool IsBusy { get { return isBusy; } }

        private readonly object sync = new object();

        private delegate string CreateTaskWorkerDelegate(AsyncOperation async, CreateCollageAsyncContext asyncContext, out bool cancelled);

        public event EventHandler<ProgressChangedEventArgs> CreateProgressChanged;
        public event AsyncCompletedEventHandler CreateCompleted;

        protected virtual void OnCreateCompleted(AsyncCompletedEventArgs e)
        {
            if (CreateCompleted != null)
                CreateCompleted(this, e);
        }

        protected virtual void OnCreateProgressChanged(ProgressChangedEventArgs e)
        {
            if (CreateProgressChanged != null)
                CreateProgressChanged(this, e);
        }

        private CreateCollageAsyncContext createTaskContext = null;

        public CollageSettings Settings { get; set; }

        public CollageEngine(CollageSettings settings)
        {
            this.Settings = settings;
        }

        public CollageEngine() { }

        public void CreateAsync()
        {
            var worker = new CreateTaskWorkerDelegate(CreateCollage);
            var completedCallback = new AsyncCallback(CreateTaskCompletedCallback);

            lock (sync)
            {
                if (isBusy)
                    throw new InvalidOperationException("The engine is currently busy.");

                AsyncOperation async = AsyncOperationManager.CreateOperation(null);
                CreateCollageAsyncContext context = new CreateCollageAsyncContext();
                bool cancelled;

                worker.BeginInvoke(async, context, out cancelled, completedCallback, async);

                isBusy = true;
                createTaskContext = context;
            }
        }

        public void CancelAsync()
        {
            lock (sync)
            {
                if (createTaskContext != null)
                    createTaskContext.Cancel();
            }
        }

        private void CreateTaskCompletedCallback(IAsyncResult ar)
        {
            CreateTaskWorkerDelegate worker = (CreateTaskWorkerDelegate)((AsyncResult)ar).AsyncDelegate;
            AsyncOperation async = (AsyncOperation)ar.AsyncState;

            bool cancelled = false;
            string result = worker.EndInvoke(out cancelled, ar);

            lock (sync)
            {
                isBusy = false;
            }

            AsyncCompletedEventArgs completedArgs = new AsyncCompletedEventArgs(null, cancelled, result);

            async.PostOperationCompleted(
              delegate(object e) { OnCreateCompleted((AsyncCompletedEventArgs)e); },
              completedArgs);
        }

        public string Create()
        {
            bool cancelled = false;
            return CreateCollage(null, null, out cancelled);
        }

        private string CreateCollage(AsyncOperation async, CreateCollageAsyncContext context, out bool cancelled)
        {
            cancelled = false;

            string collageFileName = Path.Combine(Settings.OutputDirectory, GetFileName());
            Random random = new Random();

            using (Bitmap bitmapCollage = new Bitmap(this.Settings.NumberOfColumns * this.Settings.TileWidth,
                this.Settings.NumberOfRows * this.Settings.TileHeight))
            {
                using (Graphics graphics = Graphics.FromImage(bitmapCollage))
                {
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    for (int rowsCounter = 0; rowsCounter < this.Settings.NumberOfRows; rowsCounter++)
                    {
                        for (int colsCounter = 0; colsCounter < this.Settings.NumberOfColumns; colsCounter++)
                        {
                            // Report progress
                            if (async != null)
                            {
                                int progressPercentage = CountProgressPercentage(colsCounter, rowsCounter);
                                ProgressChangedEventArgs args = new ProgressChangedEventArgs(progressPercentage, null);
                                async.Post(delegate(object e)
                                { OnCreateProgressChanged((ProgressChangedEventArgs)e); }, args);
                            }

                            // Support for cancelling
                            if (context != null)
                            {
                                if (context.IsCancelling)
                                {
                                    cancelled = true;
                                    return "";
                                }
                            }

                            using (Bitmap tile = (Bitmap)Bitmap.FromFile(Settings.InputImages[random.Next(0, Settings.InputImages.Count)]))
                            {
                                using (Bitmap tileScaled = tile.Scale(Settings.ScalePercent))
                                {
                                    if (this.Settings.RotateAndFlipRandomly)
                                        tileScaled.RotateFlipRandom(random);

                                    if (tileScaled.HorizontalResolution != graphics.DpiX || tileScaled.VerticalResolution != graphics.DpiY)
                                        tileScaled.SetResolution(graphics.DpiX, graphics.DpiY);

                                    int randomX = (tileScaled.Width > Settings.TileWidth) ?
                                        random.Next(0, tileScaled.Width - Settings.TileWidth) : 0;

                                    int randomY = (tileScaled.Height > Settings.TileHeight) ?
                                        random.Next(0, tileScaled.Height - Settings.TileHeight) : 0;

                                    graphics.DrawImage(
                                        tileScaled,
                                        colsCounter * Settings.TileWidth,
                                        rowsCounter * Settings.TileHeight,
                                        new Rectangle(randomX, randomY, Settings.TileWidth, Settings.TileHeight),
                                        GraphicsUnit.Pixel);
                                }
                            }
                        }
                    }
                }

                Bitmap bitmapCollageTransformed = bitmapCollage;

                if (Settings.ConvertToGrayscale)
                    bitmapCollageTransformed = bitmapCollage.ToGrayscale();

                bitmapCollageTransformed.Save(collageFileName, ImageFormat.Jpeg);
                bitmapCollageTransformed.Dispose();
            }

            return collageFileName;
        }

        /// <summary>
        /// Gets the output file name.
        /// </summary>
        /// <returns></returns>
        private string GetFileName()
        {
            return string.Format("collage_{0:yyyy-MM-dd_HHmm}.jpg", DateTime.Now);
        }

        /// <summary>
        /// Gets the progress percentage while creating collage.
        /// </summary>
        /// <param name="colsCounter">Current column index.</param>
        /// <param name="rowsCounter">Current row index</param>
        /// <returns></returns>
        private int CountProgressPercentage(int colsCounter, int rowsCounter)
        {
            return (int)
             (
              (
               ((float)(rowsCounter * this.Settings.NumberOfRows + (colsCounter + 1)))
               /
               ((float)(this.Settings.NumberOfRows * this.Settings.NumberOfColumns))
              )
              * ((float)100)
             );
        }
    }
}
