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
        private readonly IFilesEnumerator filesEnumerator;
        private readonly CollageSaver collageSaver;

        public CollageGenerator(CollageSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            this.settings = settings;
            this.progressCounter = new ProgressCounter(settings.Dimensions.NumberOfRows, settings.Dimensions.NumberOfColumns);
            this.randomGenerator = new RandomGenerator();
            this.tileTransformer = new TileTransformer();
            this.filesEnumerator = new DateFilesEnumerator(settings.InputFiles);
            this.collageSaver = new CollageSaver(settings.OutputDirectory);
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

            using (var bitmapCollage = new Bitmap(this.settings.Dimensions.TotalWidth, this.settings.Dimensions.TotalHeight))
            {
                using (Graphics graphics = bitmapCollage.CreateGraphics())
                {
                    for (int rowsCounter = 0; rowsCounter < this.settings.Dimensions.NumberOfRows; rowsCounter++)
                    {
                        for (int colsCounter = 0; colsCounter < this.settings.Dimensions.NumberOfColumns; colsCounter++)
                        {
                            this.ReportProgress(async, colsCounter, rowsCounter);
                            HandleCancellation(context, ref isCancelled);

                            DrawTile(graphics, colsCounter, rowsCounter);
                        }
                    }
                }

                this.collageSaver.Save(bitmapCollage);
            }
        }

        private void DrawTile(Graphics graphics, int colsCounter, int rowsCounter)
        {
            using (var tile = Image.FromFile(this.filesEnumerator.GetNextFileName()))
            {
                var tileTransformerSettings = new TileTransformerSettings
                                                  {
                                                      GraphicsDpiX = graphics.DpiX,
                                                      GraphicsDpiY = graphics.DpiY,
                                                      RotateAndFlipRandomly = settings.Additional.RotateAndFlipRandomly,
                                                      ScalePercent = settings.Dimensions.TileScalePercent
                                                  };

                using (var tileTransformed = this.tileTransformer.Transform(tile, tileTransformerSettings))
                {
                    graphics.DrawImage(
                       tileTransformed,
                       colsCounter * this.settings.Dimensions.TileWidth,
                       rowsCounter * this.settings.Dimensions.TileHeight,
                       new Rectangle(this.GetTilePosition(tile.Size), new Size(this.settings.Dimensions.TileWidth, this.settings.Dimensions.TileHeight)),
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

        private Point GetTilePosition(Size tileSize)
        {
            int x = 0, y = 0;

            if (this.settings.Additional.CutTileRandomly)
            {
                x = (tileSize.Width > this.settings.Dimensions.TileWidth) ?
                    this.randomGenerator.Next(0, tileSize.Width - this.settings.Dimensions.TileWidth) : 0;

                y = (tileSize.Height > this.settings.Dimensions.TileHeight) ?
                    this.randomGenerator.Next(0, tileSize.Height - this.settings.Dimensions.TileHeight) : 0;
            }
            
            return new Point(x, y);
        }
    }
}
