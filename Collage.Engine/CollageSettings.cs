using System.Collections.Generic;

namespace Collage.Engine
{
    public class CollageSettings
    {
        public CollageSettings()
        {
            OutputDirectory = "";
            InputImages = new List<string>();
            NumberOfColumns = 2;
            NumberOfRows = 2;
            TileWidth = 2;
            TileHeight = 2;
            ScalePercent = 50;
        }

        public int ScalePercent { get; set; }

        public int TileHeight { get; set; }

        public int TileWidth { get; set; }

        public int NumberOfRows { get; set; }

        public int NumberOfColumns { get; set; }

        public bool RotateAndFlipRandomly { get; set; }

        public bool ConvertToGrayscale { get; set; }

        public List<string> InputImages { get; set; }

        public string OutputDirectory { get; set; }
    }
}
