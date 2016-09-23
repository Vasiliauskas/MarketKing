using System;
using System.Collections.Generic;
using System.Windows;

namespace MarketKing
{
    public interface IStrategy
    {
        string Name { get; }
        Transaction Turn(MyCell[] myBlocks);
    }

    public class HexagonModel
    {
        public HexagonModel(Vector location)
        {
            CenterLocation = location;
        }
        public Vector CenterLocation { get; private set; }

        private Cell _TopLeft;
        public Cell TopLeft
        {
            get
            {
                return _TopLeft;
            }
            set
            {
                if (_TopLeft != value)
                {
                    _TopLeft = value;
                    Neighbours.Add(_TopLeft);
                }
            }
        }

        private Cell _Top;
        public Cell Top
        {
            get
            {
                return _Top;
            }
            set
            {
                if (_Top != value)
                {
                    _Top = value;
                    Neighbours.Add(_Top);
                }
            }
        }

        private Cell _TopRight;
        public Cell TopRight
        {
            get
            {
                return _TopRight;
            }
            set
            {
                if (_TopRight != value)
                {
                    _TopRight = value;
                    Neighbours.Add(_TopRight);
                }
            }
        }

        private Cell _BottomRight;
        public Cell BottomRight
        {
            get
            {
                return _BottomRight;
            }
            set
            {
                if (_BottomRight != value)
                {
                    _BottomRight = value;
                    Neighbours.Add(_BottomRight);
                }
            }
        }

        private Cell _Bottom;
        public Cell Bottom
        {
            get
            {
                return _Bottom;
            }
            set
            {
                if (_Bottom != value)
                {
                    _Bottom = value;
                    Neighbours.Add(_Bottom);
                }
            }
        }

        private Cell _BottomLeft;
        public Cell BottomLeft
        {
            get
            {
                return _BottomLeft;
            }
            set
            {
                if (_BottomLeft != value)
                {
                    _BottomLeft = value;
                    Neighbours.Add(_BottomLeft);
                }
            }
        }

        public List<Cell> Neighbours { get; } = new List<Cell>();
    }

    public class Cell : HexagonModel
    {
        public int? OwnedById { get; set; }
        public int Resources { get; set; }

        public Cell(Vector location, int? ownedById, int resources)
            : base(location)
        {
            OwnedById = ownedById;
            Resources = resources;
        }
    }

    public class MyCell : Cell // used for ensuring method contract for securely supplying resources to move from a valid field
    {
        public MyCell(Vector location, int? ownedById, int resources) : base(location, ownedById, resources)
        {
        }

        public MyCell(Cell block)
            : base(block.CenterLocation, block.OwnedById, block.Resources)
        {
            TopLeft = block.TopLeft;
            Top = block.Top;
            TopRight = block.TopRight;
            BottomRight = block.BottomRight;
            Bottom = block.Bottom;
            BottomLeft = block.BottomLeft;
        }
    }

    public struct Transaction
    {
        public MyCell MyBlock { get; private set; }
        public Cell TargetBlock { get; private set; }
        public int AmmountToTransfer { get; private set; }
        public Transaction(MyCell myblock, Cell targetBlock, int ammountToTransfer)
        {
            MyBlock = myblock;
            TargetBlock = targetBlock;
            AmmountToTransfer = ammountToTransfer;
        }
    }
}
