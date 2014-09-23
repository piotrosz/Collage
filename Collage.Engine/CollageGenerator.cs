namespace Collage.Engine
{
    using System;
    using System.Drawing;
    using System.ComponentModel;
    using System.Runtime.Remoting.Messaging;

    public class CollageGenerator
    {
        private readonly ProgressCounter progressCounter;
        private readonly IRandomGenerator randomGenerator;
        private readonly CollageSettings settings;
        private readonly TileTransformer tileTransformer;

        public CollageGenerator(CollageSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            this.settings = settings;
            this.progressCounter = new ProgressCounter(settings.DimensionSettings.NumberOfRows, settings.DimensionSettings.NumberOfColumns);
            this.randomGenerator = new RandomGenerator();
            this.tileTransformer = new TileTransformer();
        }

        public bool IsBusy { get; private set; }

        private readonly object sync = new object();

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

        private CreateCollageAsyncContext createTaskContext;

        public void CreateAsync()
        {
            var worker = new CreateTaskWorkerDelegate(CreateCollage);
            var completedCallback = new AsyncCallback(CreateTaskCompletedCallback);

            lock (this.sync)
            {
                if (this.IsBusy)
                {
                    throw new InvalidOperationException("The engine is currently busy.");
                }
                    
                AsyncOperation async = AsyncOperationManager.CreateOperation(null);
                var context = new CreateCollageAsyncContext();
                bool cancelled;

                worker.BeginInvoke(async, context, out cancelled, completedCallback, async);

                this.IsBusy = true;
                this.createTaskContext = context;
            }
        }

        public void CancelAsync()
        {
            lock (this.sync)
            {
                if (this.createTaskContext != null)
                {
                    this.createTaskContext.Cancel();
                }
            }
        }

        private void CreateTaskCompletedCallback(IAsyncResult asyncResult)
        {
            var worker = (CreateTaskWorkerDelegate)((AsyncResult)asyncResult).AsyncDelegate;
            var async = (AsyncOperation)asyncResult.AsyncState;

            bool isCancelled;
            worker.EndInvoke(out isCancelled, asyncResult);

            lock (this.sync)
            {
                this.IsBusy = false;
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

            using (var bitmapCollage = new Bitmap(
                this.settings.DimensionSettings.TotalWidth,
                this.settings.DimensionSettings.TotalHeight))
            {
                using (Graphics graphics = bitmapCollage.CreateGraphics())
                {
                    for (int rowsCounter = 0; rowsCounter < this.settings.DimensionSettings.NumberOfRows; rowsCounter++)
                    {
                        for (int colsCounter = 0; colsCounter < this.settings.DimensionSettings.NumberOfColumns; colsCounter++)
                        {
                            this.ReportProgress(async, colsCounter, rowsCounter);
                            HandleCancellation(context, ref isCancelled);

                            DrawTile(graphics, colsCounter, rowsCounter);
                        }
                    }
                }

                new CollageSaver(this.settings.OutputDirectory).Save(bitmapCollage);
            }
        }

        private void DrawTile(Graphics graphics, int colsCounter, int rowsCounter)
        {
            using (var tile = (Bitmap)Image.FromFile(this.settings.InputFiles[this.randomGenerator.Next(0, this.settings.InputFiles.Count)].FullName))
            {
                var tileTransformerSettings = new TileTransformerSettings
                                                  {
                                                      GraphicsDpiX = graphics.DpiX,
                                                      GraphicsDpiY = graphics.DpiY,
                                                      RotateAndFlipRandomly = settings.AdditionalSettings.RotateAndFlipRandomly,
                                                      ScalePercent = settings.DimensionSettings.TileScalePercent
                                                  };

                using (var tileTransformed = this.tileTransformer.Transform(tile, tileTransformerSettings))
                {
                    graphics.DrawImage(
                       tileTransformed,
                       colsCounter * this.settings.DimensionSettings.TileWidth,
                       rowsCounter * this.settings.DimensionSettings.TileHeight,
                       new Rectangle(this.GetTileXY(tileTransformed), new Size(this.settings.DimensionSettings.TileWidth, this.settings.DimensionSettings.TileHeight)),
                       GraphicsUnit.Pixel);   
                }
            }
        }

        private static void HandleCancellation(CreateCollageAsyncContext context, ref bool isCancelled)
        {
            if (context != null)
            {
                if (context.IsCancelling)
                {
                    isCancelled = true;
                }
            }
        }

        private void ReportProgress(AsyncOperation async, int colsCounter, int rowsCounter)
        {
            if (async != null)
            {
                int progressPercentage = this.progressCounter.GetProgressPercentage(colsCounter, rowsCounter);
                var args = new ProgressChangedEventArgs(progressPercentage, null);
                async.Post(e => this.OnCreateProgressChanged((ProgressChangedEventArgs)e), args);
            }
        }

        private Point GetTileXY(Image tile)
        {
            int x = (tile.Width > this.settings.DimensionSettings.TileWidth && this.settings.AdditionalSettings.CutTileRandomly) ?
                        this.randomGenerator.Next(0, tile.Width - this.settings.DimensionSettings.TileWidth) : 0;

            int y = (tile.Height > this.settings.DimensionSettings.TileHeight && this.settings.AdditionalSettings.CutTileRandomly) ?
                this.randomGenerator.Next(0, tile.Height - this.settings.DimensionSettings.TileHeight) : 0;

            return new Point(x, y);
        }
    }
}
