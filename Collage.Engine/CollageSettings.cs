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
            this.DimensionSettings = collageDimensionSettings;
            this.AdditionalSettings = additionalCollageSettings;
            this.InputFiles = inputFiles;
            this.OutputDirectory = outputDirectory;
        }

        public AdditionalCollageSettings AdditionalSettings { get; set; }

        public CollageDimensionSettings DimensionSettings { get; set; }

        public List<FileInfo> InputFiles { get; set; }

        public DirectoryInfo OutputDirectory { get; set; }
    }
}
