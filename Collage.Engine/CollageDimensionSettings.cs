namespace Collage.Engine
{
    public class CollageDimensionSettings
    {
        public int TileHeight { get; set; }

        public int TileWidth { get; set; }

        public int NumberOfRows { get; set; }

        public int NumberOfColumns { get; set; }

        public Percentage TileScalePercent { get; set; }

        public int TotalHeight
        {
            get
            {
                return this.NumberOfRows * this.TileHeight;
            }
        }

        public int TotalWidth
        {
            get
            {
                return this.NumberOfColumns * this.TileWidth;
            }
        }
    }
}
