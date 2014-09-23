namespace Collage.Engine
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal class RandomFilesEnumerator : IFilesEnumerator
    {
        private readonly List<FileInfo> filesList;
        private readonly IRandomGenerator randomGenerator;

        public RandomFilesEnumerator(List<FileInfo> filesList)
        {
            if (filesList == null)
            {
                throw new ArgumentNullException("filesList");
            }
            
            this.filesList = filesList;
            this.randomGenerator = new RandomGenerator();
        }

        public string GetNextFileName()
        {
            return this.filesList[this.randomGenerator.Next(0, this.filesList.Count)].FullName;
        }
    }
}
