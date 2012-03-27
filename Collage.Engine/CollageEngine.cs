using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace Collage.Engine
{
    public class CollageEngine
    {
        public CollageSettings Settings { get; set; }

        //public delegate void CollageProgressChangedEventHandler(object sender, ProgressChangedEventArgs e);

        //public event CollageProgressChangedEventHandler ProgressChanged;

        public CollageEngine(CollageSettings settings)
        {
            this.Settings = settings;
        }

        public string Create()
        {
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
                            //if (ProgressChanged != null)
                            //{
                            //    int percentage = CountProgressPercentage(colsCounter, rowsCounter);
                            //    ProgressChanged(null, new ProgressChangedEventArgs(percentage, null));
                            //}

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

        private string GetFileName()
        {
            return string.Format("collage_{0:yyyy-MM-dd_HHmm}.jpg", DateTime.Now);
        }

        private int CountProgressPercentage(int colsCounter, int rowsCounter)
        {
            return (int)
             (
              (
               ((float)((rowsCounter + 1) * (colsCounter + 1)))
               /
               ((float)(this.Settings.NumberOfRows * this.Settings.NumberOfColumns))
              )
              * ((float)100)
             );
        }
    }
}
