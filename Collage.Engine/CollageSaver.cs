namespace Collage.Engine
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    public class CollageSaver
    {
        private readonly FileNameCreator fileNameCreator;

        public CollageSaver(DirectoryInfo outputDirectory)
        {
            this.fileNameCreator = new FileNameCreator(outputDirectory);
        }

        public FileInfo Save(Bitmap bitmap)
        {
            var fileName = this.fileNameCreator.CreateFileName();

            bitmap.Save(fileName, ImageFormat.Jpeg);
            bitmap.Dispose();

            return new FileInfo(fileName);
        }
    }
}
