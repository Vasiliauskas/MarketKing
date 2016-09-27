using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MarketKing.Game
{
    public class CellCollection : Dictionary<Vector, Cell>, ICollection<Cell>
    {
        public bool IsReadOnly { get { return false; } }

        public void Add(Cell item)
        {
            Add(item.CenterLocation, item);
            AssignNeighbours(item);
        }

        public bool Contains(Cell item)
        {
            return ContainsValue(item);
        }

        public void CopyTo(Cell[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Cell item)
        {
            return Remove(item.CenterLocation);
        }

        IEnumerator<Cell> IEnumerable<Cell>.GetEnumerator()
        {
            return Values.GetEnumerator();
        }

        private void AssignNeighbours(Cell cell)
        {
            var cellLocation = cell.CenterLocation;
            var x = cellLocation.X;
            var y = cellLocation.Y;
            var isEvenColumn = x % 2 == 0;
            cell.TopLeft = TryGetCell(new Vector(x - 1, !isEvenColumn ? y : y - 1));
            cell.Top = TryGetCell(new Vector(x, y - 1));
            cell.TopRight = TryGetCell(new Vector(x + 1, !isEvenColumn ? y : y - 1));
            cell.BottomRight = TryGetCell(new Vector(x + 1, !isEvenColumn ? y + 1 : y));
            cell.Bottom = TryGetCell(new Vector(x, y + 1));
            cell.BottomLeft = TryGetCell(new Vector(x - 1, !isEvenColumn ? y + 1 : y));

            if (cell.TopLeft != null)
                cell.TopLeft.BottomRight = cell;

            if (cell.Top != null)
                cell.Top.Bottom = cell;

            if (cell.TopRight != null)
                cell.TopRight.BottomLeft = cell;

            if (cell.BottomRight != null)
                cell.BottomRight.TopLeft = cell;

            if (cell.Bottom != null)
                cell.Bottom.Top = cell;

            if (cell.BottomLeft != null)
                cell.BottomLeft.TopRight = cell;
        }

        private Cell TryGetCell(Vector vector)
        {
            if (Keys.Contains(vector))
                return this[vector];

            return null;
        }
    }
}
