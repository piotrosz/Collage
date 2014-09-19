namespace Collage.Engine
{
    using System;

    public class FileNameCreator
    {
        public string CreateFileName()
        {
            return string.Format("collage_{0:yyyy-MM-dd_HHmm}.jpg", DateTime.Now);
        } 
    }
}
