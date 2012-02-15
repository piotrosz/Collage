using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Collage.Engine
{
    public class CollageSettings
    {
        private int tileHeight = 2;
        private int tileWidth = 2;
        private int numberOfRows = 2;
        private int numberOfColumns = 2;
        private bool rotateAndFlipRandomly = false;
        private bool convertToGrayscale = false;
        List<string> inputImages = new List<string>();
        private string outputDirectory = "";

        public int TileHeight
        {
            get { return tileHeight; }
            set { tileHeight = value; }
        }

        public int TileWidth
        {
            get { return tileWidth; }
            set { tileWidth = value; }
        }

        public int NumberOfRows
        {
            get { return numberOfRows; }
            set { numberOfRows = value; }
        }

        public int NumberOfColumns
        {
            get { return numberOfColumns; }
            set { numberOfColumns = value; }
        }

        public bool RotateAndFlipRandomly
        {
            get { return rotateAndFlipRandomly; }
            set { rotateAndFlipRandomly = value; }
        }

        public bool ConvertToGrayscale
        {
            get { return convertToGrayscale; }
            set { convertToGrayscale = value; }
        }

        public List<string> InputImages
        {
            get { return inputImages; }
            set { inputImages = value; }
        }

        public string OutputDirectory
        {
            get { return outputDirectory; }
            set { outputDirectory = value; }
        }
    }
}
