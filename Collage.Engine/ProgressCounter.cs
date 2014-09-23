namespace Collage.Engine
{
    using System;

    internal class ProgressCounter
    {
        private readonly int totalNumberOfRows;
        private readonly int totalNumberOfColumns;

        public ProgressCounter(int totalNumberOfRows, int totalNumberOfColumns)
        {
            if (totalNumberOfRows <= 0)
            {
                throw new ArgumentOutOfRangeException("totalNumberOfRows", "Total number of rows must be a positive value");
            }

            if (totalNumberOfColumns <= 0)
            {
                throw new ArgumentOutOfRangeException("totalNumberOfColumns", "Total number of columns must be a positive value");
            }

            this.totalNumberOfRows = totalNumberOfRows;
            this.totalNumberOfColumns = totalNumberOfColumns;
        }

        public int GetProgressPercentage(int colsCounter, int rowsCounter)
        {
            int tilesCompleted = rowsCounter * this.totalNumberOfRows + (colsCounter + 1);
            int totalTiles = this.totalNumberOfRows * this.totalNumberOfColumns;

            return (int) ( (float)tilesCompleted / (float)totalTiles * 100f );
        }
    }
}
