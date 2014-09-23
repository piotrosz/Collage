namespace Collage.Engine
{
    using System;
    using System.IO;

    internal class FileNameCreator
    {
        public DirectoryInfo OutputDirectory { get; private set; }

        public FileNameCreator(DirectoryInfo outputDirectory)
        {
            if (outputDirectory == null)
            {
                throw new ArgumentNullException("outputDirectory");
            }

            if (!outputDirectory.Exists)
            {
                throw new ArgumentException("Output directory does not exist", "outputDirectory");
            }

            this.OutputDirectory = outputDirectory;
        }

        public string CreateFileName()
        {
            string fileName = string.Format("collage-{0:yyyy-MM-dd_HHmm}.jpg", DateTime.Now);
            return Path.Combine(this.OutputDirectory.FullName, fileName);
        } 
    }
}
