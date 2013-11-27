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
    // TODO: This class is too big
    public class CollageEngine
    {
        private bool _isBusy;
        public bool IsBusy { get { return _isBusy; } }

        private readonly Random _random = new Random();

        private readonly object _sync = new object();

        private delegate string CreateTaskWorkerDelegate(AsyncOperation async, CreateCollageAsyncContext asyncContext, out bool cancelled);

        public event EventHandler<ProgressChangedEventArgs> CreateProgressChanged;
        public event AsyncCompletedEventHandler CreateCompleted;

        protected virtual void OnCreateCompleted(AsyncCompletedEventArgs e)
        {
            if (CreateCompleted != null)
            {
                CreateCompleted(this, e);
            }
        }

        protected virtual void OnCreateProgressChanged(ProgressChangedEventArgs e)
        {
            if (CreateProgressChanged != null)
            {
                CreateProgressChanged(this, e);
            }
        }

        private CreateCollageAsyncContext _createTaskContext;

        public CollageSettings Settings { get; set; }

        public CollageEngine(CollageSettings settings)
        {
            Settings = settings;
        }

        public CollageEngine() { }

        public void CreateAsync()
        {
            var worker = new CreateTaskWorkerDelegate(CreateCollage);
            var completedCallback = new AsyncCallback(CreateTaskCompletedCallback);

            lock (_sync)
            {
                if (_isBusy)
                    throw new InvalidOperationException("The engine is currently busy.");

                AsyncOperation async = AsyncOperationManager.CreateOperation(null);
                var context = new CreateCollageAsyncContext();
                bool cancelled;

                worker.BeginInvoke(async, context, out cancelled, completedCallback, async);

                _isBusy = true;
                _createTaskContext = context;
            }
        }

        public void CancelAsync()
        {
            lock (_sync)
            {
                if (_createTaskContext != null)
                {
                    _createTaskContext.Cancel();
                }
            }
        }

        private void CreateTaskCompletedCallback(IAsyncResult asyncResult)
        {
            var worker = (CreateTaskWorkerDelegate)((AsyncResult)asyncResult).AsyncDelegate;
            var async = (AsyncOperation)asyncResult.AsyncState;

            bool isCancelled;
            string result = worker.EndInvoke(out isCancelled, asyncResult);

            lock (_sync)
            {
                _isBusy = false;
            }

            var completedArgs = new AsyncCompletedEventArgs(null, isCancelled, result);

            async.PostOperationCompleted(
                e => OnCreateCompleted((AsyncCompletedEventArgs) e),
              completedArgs);
        }

        public string Create()
        {
            bool cancelled;
            return CreateCollage(null, null, out cancelled);
        }

        private string CreateCollage(AsyncOperation async, CreateCollageAsyncContext context, out bool isCancelled)
        {
            isCancelled = false;

            string collageFileName = Path.Combine(Settings.OutputDirectory, GetFileName());
            
            using (var bitmapCollage = new Bitmap(
                Settings.NumberOfColumns * Settings.TileWidth,
                Settings.NumberOfRows * Settings.TileHeight))
            {
                using (Graphics graphics = CreateGraphics(bitmapCollage))
                {
                    for (int rowsCounter = 0; rowsCounter < Settings.NumberOfRows; rowsCounter++)
                    {
                        for (int colsCounter = 0; colsCounter < Settings.NumberOfColumns; colsCounter++)
                        {
                            // Progress reporting
                            if (async != null)
                            {
                                int progressPercentage = CountProgressPercentage(colsCounter, rowsCounter);
                                var args = new ProgressChangedEventArgs(progressPercentage, null);
                                async.Post(e => OnCreateProgressChanged((ProgressChangedEventArgs) e), args);
                            }

                            // Cancellation
                            if (context != null)
                            {
                                if (context.IsCancelling)
                                {
                                    isCancelled = true;
                                    return "";
                                }
                            }

                            DrawTile(graphics, colsCounter, rowsCounter);
                        }
                    }
                }

                Bitmap bitmapCollageTransformed = bitmapCollage;

                if (Settings.ConvertToGrayscale)
                {
                    bitmapCollageTransformed = bitmapCollage.ToGrayscale();
                }

                bitmapCollageTransformed.Save(collageFileName, ImageFormat.Jpeg);
                bitmapCollageTransformed.Dispose();
            }

            return collageFileName;
        }

        private Graphics CreateGraphics(Bitmap bitmap)
        {
            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            return graphics;
        }

        private void DrawTile(Graphics graphics, int colsCounter, int rowsCounter)
        {
            using (var tile = (Bitmap)Image.FromFile(Settings.InputImages[_random.Next(0, Settings.InputImages.Count)]))
            {
                using (Bitmap tileScaled = tile.Scale(Settings.ScalePercent))
                {
                    if (this.Settings.RotateAndFlipRandomly)
                    {
                        tileScaled.RotateFlipRandom(_random);
                    }

                    if (Math.Abs(tileScaled.HorizontalResolution - graphics.DpiX) > 0.01 ||
                        Math.Abs(tileScaled.VerticalResolution - graphics.DpiY) > 0.01)
                    {
                        tileScaled.SetResolution(graphics.DpiX, graphics.DpiY);
                    }

                    int randomX = (tileScaled.Width > Settings.TileWidth) ?
                        _random.Next(0, tileScaled.Width - Settings.TileWidth) : 0;

                    int randomY = (tileScaled.Height > Settings.TileHeight) ?
                        _random.Next(0, tileScaled.Height - Settings.TileHeight) : 0;

                    graphics.DrawImage(
                        tileScaled,
                        colsCounter * Settings.TileWidth,
                        rowsCounter * Settings.TileHeight,
                        new Rectangle(randomX, randomY, Settings.TileWidth, Settings.TileHeight),
                        GraphicsUnit.Pixel);
                }
            }
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
