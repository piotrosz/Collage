using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace Collage.Engine
{
    public class CollageEngine
    {
        public CollageSettings Settings { get; set; }

        //private static string tempImgDir = @"temp\";
        private static string outputDir = "";

        private List<string> croppedImages = new List<string>();
        
        #region Properties
        //public string TempImgDir
        //{
        //    set { tempImgDir = value; }
        //}

        public string OutputDir
        {
            set { outputDir = value; }
        }
        #endregion

        public CollageEngine(CollageSettings settings)
        {
            this.Settings = settings;
        }

        public string CreateCollage()
        {
            //CropImages();

            string collageFileName = Path.Combine(outputDir, GetRandomName());
            Random random = new Random();

            using (Bitmap bitmapCollage = new Bitmap(this.Settings.NumberOfColumns * this.Settings.TileWidth, 
                this.Settings.NumberOfRows * this.Settings.TileHeight))
            {
                using (Graphics graphics = Graphics.FromImage(bitmapCollage))
                {
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
                                
                                graphics.DrawImage(
                                    tile,
                                    colsCounter * Settings.TileWidth, 
                                    rowsCounter * Settings.TileHeight,
                                    new Rectangle(0, 0, Settings.TileWidth, Settings.TileHeight), GraphicsUnit.Pixel);

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

        //private void CropImages()
        //{
        //    Random random = new Random();
        //    int counter = 0;

        //    for (int i = 0; i < (this.Settings.NumberOfColumns * this.Settings.NumberOfRows); i++)
        //    {
        //        if (counter == Settings.InputImages.Count)
        //            counter = 0;

        //        using (Bitmap bitmapOriginal = (Bitmap)Bitmap.FromFile(Settings.InputImages[counter]))
        //        {
        //            Rectangle rectangle = new Rectangle();

        //            if (bitmapOriginal.Width > Settings.TileWidth)
        //                rectangle.X = random.Next(0, bitmapOriginal.Width - Settings.TileWidth);

        //            if (bitmapOriginal.Height > Settings.TileHeight)
        //                rectangle.Y = random.Next(0, bitmapOriginal.Height - Settings.TileHeight);

        //            if (bitmapOriginal.Width > Settings.TileWidth && bitmapOriginal.Height > Settings.TileHeight)
        //            {
        //                rectangle.Width = Settings.TileWidth;
        //                rectangle.Height = Settings.TileHeight;
        //            }
        //            else
        //            {
        //                rectangle.X = rectangle.Y = 0;
        //                rectangle.Width = bitmapOriginal.Width;
        //                rectangle.Height = bitmapOriginal.Height;
        //            }

        //            using (Bitmap bitmapCropped = bitmapOriginal.Clone(rectangle, PixelFormat.Format24bppRgb))
        //            {
        //                string croppedImageName = Path.Combine(tempImgDir, GetRandomName());
        //                bitmapCropped.Save(croppedImageName);
        //                croppedImages.Add(croppedImageName);
        //                counter++;
        //            }
        //        }
        //    }
        //}

        private string GetRandomName()
        {
            return Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".jpg";
        }

        //public static void DeleteTempFiles()
        //{
        //    Directory.Delete(tempImgDir, false);
        //}

        //public static bool CreateTempDir()
        //{
        //    bool directoryCreated = false;

        //    if (!Directory.Exists(tempImgDir))
        //    {
        //        Directory.CreateDirectory(tempImgDir);
        //        directoryCreated = true;
        //    }

        //    return directoryCreated;
        //}
    }
}
