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
    }
}
