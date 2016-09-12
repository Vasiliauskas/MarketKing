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
        internal Cell TopLeft { get; private set; }
        internal Cell Top { get; private set; }
        internal Cell TopRight { get; private set; }
        internal Cell BottomLeft { get; private set; }
        internal Cell Bottom { get; private set; }
        internal Cell BottomRight { get; private set; }
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

        //internal MyCell(Cell block)
        //    : base(block.Id, block.OwnedById, block.Resources, block.Top, block.Bottom, block.Left, block.Right)
        //{
        //}

        public new Cell Top { get { return Top; } }
        public new Cell Bottom { get { return Bottom; } }
        public new Cell TopLeft { get { return TopLeft; } }
        public new Cell TopRight { get { return TopRight; } }
        public new Cell BottomLeft { get { return BottomLeft; } }
        public new Cell BottomRight { get { return BottomRight; } }
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
