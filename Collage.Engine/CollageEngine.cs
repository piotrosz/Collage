namespace Collage.Engine
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Drawing.Drawing2D;
    using System.ComponentModel;
    using System.Runtime.Remoting.Messaging;

    // TODO: This class is too big
    public class CollageEngine
    {
        private readonly ProgressCounter progressCounter;
        private readonly FileNameCreator fileNameCreator;

        public CollageEngine(CollageSettings settings)
        {
            this.Settings = settings;
            this.progressCounter = new ProgressCounter(settings.DimensionSettings.NumberOfRows, settings.DimensionSettings.NumberOfColumns);
            this.fileNameCreator = new FileNameCreator(settings.OutputDirectory);
        }

        public string FileName { get; private set; }

        private bool _isBusy;
        public bool IsBusy { get { return _isBusy; } }

        private readonly Random _random = new Random();

        private readonly object _sync = new object();

        private delegate void CreateTaskWorkerDelegate(AsyncOperation async, CreateCollageAsyncContext asyncContext, out bool cancelled);

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

        public void CreateAsync()
        {
            var worker = new CreateTaskWorkerDelegate(CreateCollage);
            var completedCallback = new AsyncCallback(CreateTaskCompletedCallback);

            lock (_sync)
            {
                if (_isBusy)
                {
                    throw new InvalidOperationException("The engine is currently busy.");
                }
                    
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
            worker.EndInvoke(out isCancelled, asyncResult);

            lock (_sync)
            {
                _isBusy = false;
            }

            var completedArgs = new AsyncCompletedEventArgs(null, isCancelled, null);

            async.PostOperationCompleted(
                e => OnCreateCompleted((AsyncCompletedEventArgs) e),
              completedArgs);
        }

        public void Create()
        {
            bool cancelled;
            CreateCollage(null, null, out cancelled);
        }

        private void CreateCollage(AsyncOperation async, CreateCollageAsyncContext context, out bool isCancelled)
        {
            isCancelled = false;

            this.FileName = this.fileNameCreator.CreateFileName();
            
            using (var bitmapCollage = new Bitmap(
                this.Settings.DimensionSettings.TotalWidth,
                this.Settings.DimensionSettings.TotalHeight))
            {
                using (Graphics graphics = CreateGraphics(bitmapCollage))
                {
                    for (int rowsCounter = 0; rowsCounter < Settings.DimensionSettings.NumberOfRows; rowsCounter++)
                    {
                        for (int colsCounter = 0; colsCounter < Settings.DimensionSettings.NumberOfColumns; colsCounter++)
                        {
                            // Progress reporting
                            if (async != null)
                            {
                                int progressPercentage = this.progressCounter.GetProgressPercentage(colsCounter, rowsCounter);
                                var args = new ProgressChangedEventArgs(progressPercentage, null);
                                async.Post(e => OnCreateProgressChanged((ProgressChangedEventArgs) e), args);
                            }

                            // Cancellation
                            if (context != null)
                            {
                                if (context.IsCancelling)
                                {
                                    isCancelled = true;
                                }
                            }

                            DrawTile(graphics, colsCounter, rowsCounter);
                        }
                    }
                }

                Bitmap bitmapCollageTransformed = bitmapCollage;

                if (Settings.AdditionalSettings.ConvertToGrayscale)
                {
                    bitmapCollageTransformed = bitmapCollage.ToGrayscale();
                }

                bitmapCollageTransformed.Save(this.FileName, ImageFormat.Jpeg);
                bitmapCollageTransformed.Dispose();
            }
        }

        private Graphics CreateGraphics(Bitmap bitmap)
        {
            var graphics = Graphics.FromImage(bitmap);

            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            return graphics;
        }

        private void DrawTile(Graphics graphics, int colsCounter, int rowsCounter)
        {
            using (var tile = (Bitmap)Image.FromFile(Settings.InputFiles[_random.Next(0, Settings.InputFiles.Count)].FullName))
            {
                using (Bitmap tileScaled = tile.Scale(Settings.DimensionSettings.TileScalePercent))
                {
                    if (this.Settings.AdditionalSettings.RotateAndFlipRandomly)
                    {
                        tileScaled.RotateFlipRandom(_random);
                    }

                    if (Math.Abs(tileScaled.HorizontalResolution - graphics.DpiX) > 0.01 ||
                        Math.Abs(tileScaled.VerticalResolution - graphics.DpiY) > 0.01)
                    {
                        tileScaled.SetResolution(graphics.DpiX, graphics.DpiY);
                    }

                    int randomX = (tileScaled.Width > Settings.DimensionSettings.TileWidth) ?
                        _random.Next(0, tileScaled.Width - Settings.DimensionSettings.TileWidth) : 0;

                    int randomY = (tileScaled.Height > Settings.DimensionSettings.TileHeight) ?
                        _random.Next(0, tileScaled.Height - Settings.DimensionSettings.TileHeight) : 0;

                    graphics.DrawImage(
                        tileScaled,
                        colsCounter * Settings.DimensionSettings.TileWidth,
                        rowsCounter * Settings.DimensionSettings.TileHeight,
                        new Rectangle(randomX, randomY, Settings.DimensionSettings.TileWidth, Settings.DimensionSettings.TileHeight),
                        GraphicsUnit.Pixel);
                }
            }
        }  
    }
}
