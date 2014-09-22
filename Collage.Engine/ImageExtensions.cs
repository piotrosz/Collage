namespace Collage.Engine
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;

    public static class ImageExtensions
    {
        public static void RotateFlipRandom(this Image image, Random random)
        {
            image.RotateFlip((RotateFlipType)random.Next(0, 7));
        }

        // Remember to dispose Bitmap
        public static Bitmap RotateByAngle(this Image image, float angle)
        {
            var bitmap = new Bitmap(image.Width, image.Height);

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
            if (width < image.Width && height < image.Height)
            {
                var bitmap = new Bitmap(width, height);

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
            return (Bitmap)image;
        }

        // Remember to dispose Bitmap
        public static Bitmap Scale(this Image image, Percentage percentage)
        {
            if (percentage.Value == 100)
            {
                return (Bitmap)image;
            }

            var nPercent = percentage.ValueAsFloat;

            var sourceWidth = image.Width;
            var sourceHeight = image.Height;
            const int sourceX = 0;
            const int sourceY = 0;

            const int destX = 0;
            const int destY = 0;
            var destWidth = (int)(sourceWidth * nPercent);
            var destHeight = (int)(sourceHeight * nPercent);

            var bitmap = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
            bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
         
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
            var bitmap = new Bitmap(image.Width, image.Height);
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

        public static Graphics CreateGraphics(this Bitmap bitmap)
        {
            var graphics = Graphics.FromImage(bitmap);

            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            return graphics;
        }
    }
}
