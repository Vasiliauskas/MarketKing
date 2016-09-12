using System;
using System.Linq;
using System.Windows;

namespace MarketKing.Game
{
    public class Board
    {
        private readonly CellCollection _cells;
        private readonly Vector[] _startLocations;
        private const int Gap = 5;
        public CellCollection Cells
        {
            get
            {
                return _cells;
            }
        }

        public Vector[] StartLocations
        {
            get { return _startLocations; }
        }

        public Board(int areaCount)
        {
            _cells = new CellCollection();
            _startLocations = new Vector[areaCount];
            int axisCount = (int)Math.Sqrt(areaCount); // get matrix NxN
            int k = 0;
            for (int i = 0; i < axisCount; i++) // fill starting points for matrix
                for (int j = 0; j < axisCount; j++, k++)
                    _startLocations[k] = new Vector(Gap * i, Gap * j);

            for (int i = 0; i < areaCount - (axisCount * axisCount); i++, k++) // fill starting points for remainder
                _startLocations[k] = new Vector(Gap * axisCount, Gap * i);

            int axisWithRemainderCount = axisCount + (areaCount == axisCount * axisCount ? 0 : 1);
            for (int i = -1 * Gap; i < axisWithRemainderCount * Gap; i++) // create cells
                for (int j = -1 * Gap; j < (axisCount) * Gap; j++)
                    _cells.Add(new Cell(new Vector(i, j), null, 0));
        }
    }
}
