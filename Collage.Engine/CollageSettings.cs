using System.Collections.Generic;

namespace Collage.Engine
{
    using System.IO;

    public class CollageSettings
    {
        public CollageSettings( 
            CollageDimensionSettings collageDimensionSettings, 
            AdditionalCollageSettings additionalCollageSettings, 
            List<FileInfo> inputFiles, 
            DirectoryInfo outputDirectory)
        {
            this.Dimensions = collageDimensionSettings;
            this.Additional = additionalCollageSettings;
            this.InputFiles = inputFiles;
            this.OutputDirectory = outputDirectory;
        }

        public AdditionalCollageSettings Additional { get; private set; }

        public CollageDimensionSettings Dimensions { get; private set; }

        public List<FileInfo> InputFiles { get; private set; }

        public DirectoryInfo OutputDirectory { get; private set; }
    }
}
