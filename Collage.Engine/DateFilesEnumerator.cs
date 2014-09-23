namespace Collage.Engine
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    internal class DateFilesEnumerator : IFilesEnumerator
    {
        private readonly List<FileInfo> filesListOrderedByDate;

        public DateFilesEnumerator(IEnumerable<FileInfo> filesList)
        {
            if (filesList == null)
            {
                throw new ArgumentNullException("filesList");
            }

            this.filesListOrderedByDate = filesList.OrderBy(f => f.LastWriteTime).ToList();
        }

        private int currentFileIndex; 

        public string GetNextFileName()
        {
            string fileName = this.filesListOrderedByDate[this.currentFileIndex].FullName;

            if (this.currentFileIndex < this.filesListOrderedByDate.Count)
            {
                this.currentFileIndex++;
            }
            else
            {
                this.currentFileIndex = 0;
            }

            return fileName;
        }
    }
}