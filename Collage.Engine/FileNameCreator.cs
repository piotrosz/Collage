namespace Collage.Engine
{
    using System;
    using System.IO;

    public class FileNameCreator
    {
        public DirectoryInfo OutputDirectory { get; private set; }

        public FileNameCreator(DirectoryInfo outputDirectory)
        {
            this.OutputDirectory = outputDirectory;
        }

        public string CreateFileName()
        {
            string fileName = string.Format("collage_{0:yyyy-MM-dd_HHmm}.jpg", DateTime.Now);
            return Path.Combine(this.OutputDirectory.FullName, fileName);
        } 
    }
}
