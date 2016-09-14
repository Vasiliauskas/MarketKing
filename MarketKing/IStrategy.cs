using System;
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
        public Cell TopLeft { get; set; }
        public Cell Top { get; set; }
        public Cell TopRight { get; set; }
        public Cell BottomLeft { get; set; }
        public Cell Bottom { get; set; }
        public Cell BottomRight { get; set; }
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

        //[Obsolete("Member will be always null")]
        //public new Cell Top { get { return null; } }
        //[Obsolete("Member will be always null")]
        //public new Cell Bottom { get { return null; } }
        //[Obsolete("Member will be always null")]
        //public new Cell TopLeft { get { return null; } }
        //[Obsolete("Member will be always null")]
        //public new Cell TopRight { get { return null; } }
        //[Obsolete("Member will be always null")]
        //public new Cell BottomLeft { get { return null; } }
        //[Obsolete("Member will be always null")]
        //public new Cell BottomRight { get { return null; } }
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
