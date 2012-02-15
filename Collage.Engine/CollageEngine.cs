using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Collage.Engine
{
    public class CollageEngine
    {
        public CollageSettings Settings { get; set; }

        public CollageEngine(CollageSettings settings)
        {
            this.Settings = settings;
        }

        public string CreateCollage()
        {
            string collageFileName = Path.Combine(Settings.OutputDirectory, GetRandomName());
            Random random = new Random();

            using (Bitmap bitmapCollage = new Bitmap(this.Settings.NumberOfColumns * this.Settings.TileWidth,
                this.Settings.NumberOfRows * this.Settings.TileHeight))
            {
                using (Graphics graphics = Graphics.FromImage(bitmapCollage))
                {
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    int imageCounter = 0;
                    for (int rowsCounter = 0; rowsCounter < this.Settings.NumberOfRows; rowsCounter++)
                    {
                        for (int colsCounter = 0; colsCounter < this.Settings.NumberOfColumns; colsCounter++)
                        {
                            if (imageCounter >= Settings.InputImages.Count)
                                imageCounter = 0;

                            using (Bitmap tile = (Bitmap)Bitmap.FromFile(Settings.InputImages[imageCounter]))
                            {
                                if (this.Settings.RotateAndFlipRandomly)
                                    tile.RotateFlipRandom(random);

                                if (tile.HorizontalResolution != graphics.DpiX || tile.VerticalResolution != graphics.DpiY)
                                    tile.SetResolution(graphics.DpiX, graphics.DpiY);

                                int randomX = 0;
                                int randomY = 0;

                                if (tile.Width > Settings.TileWidth)
                                    randomX = random.Next(0, tile.Width - Settings.TileWidth);

                                if (tile.Height > Settings.TileHeight)
                                    randomX = random.Next(0, tile.Height - Settings.TileHeight);

                                graphics.DrawImage(
                                    tile,
                                    colsCounter * Settings.TileWidth,
                                    rowsCounter * Settings.TileHeight,
                                    new Rectangle(randomX, randomY, Settings.TileWidth, Settings.TileHeight),
                                    GraphicsUnit.Pixel);

                                imageCounter++;
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

        private string GetRandomName()
        {
            return Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".jpg";
        }
    }
}
