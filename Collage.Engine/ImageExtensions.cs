using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Collage.Engine
{
    public static class ImageExtensions
    {
        public static void RotateFlipRandom(this Image image, Random random)
        {
            image.RotateFlip((RotateFlipType)random.Next(0, 7));
        }

        // Remember to dispose Bitmap
        public static Bitmap RotateByAngle(this Image image, float angle)
        {
            Bitmap bitmap = new Bitmap(image.Width, image.Height);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                // set the rotation point to the center of our image
                graphics.TranslateTransform((float)bitmap.Width / 2, (float)bitmap.Height / 2);
                graphics.RotateTransform(angle);
                graphics.TranslateTransform(-(float)bitmap.Width / 2, -(float)bitmap.Height / 2);
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(image, new Point(0, 0));
            }

            return bitmap;
        }

        // Remember to dispose Bitmap
        public static Bitmap Scale(this Image image, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                //set the resize quality modes to high quality
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                //draw the image into the target bitmap
                graphics.DrawImage(image, 0, 0, bitmap.Width, bitmap.Height);
            }

            return bitmap;
        }

        // Remember to dispose Bitmap
        public static Bitmap Scale(this Image image, int percent)
        {
            float nPercent = ((float)percent / 100);

            int sourceWidth = image.Width;
            int sourceHeight = image.Height;
            int sourceX = 0, sourceY = 0;

            int destX = 0, destY = 0;
            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bitmap = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
            bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (Graphics graphics = Graphics.FromImage(image))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                graphics.DrawImage(image,
                    new Rectangle(destX, destY, destWidth, destHeight),
                    new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                    GraphicsUnit.Pixel);
            }
            return bitmap;
        }

        // Remember to dispose Bitmap
        public static Bitmap ToGrayscale(this Bitmap image)
        {
            Bitmap bitmap = new Bitmap(image.Width, image.Height);
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color color = image.GetPixel(x, y);
                    int luma = (int)(color.R * 0.3 + color.G * 0.59 + color.B * 0.11);
                    bitmap.SetPixel(x, y, Color.FromArgb(luma, luma, luma));
                }
            }
            return bitmap;
        }
    }
}
