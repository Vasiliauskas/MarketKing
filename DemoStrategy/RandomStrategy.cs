using MarketKing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoStrategy
{
    public class RandomStrategy : IStrategy
    {
        private Random _rnd = new Random((int)(DateTime.Now - DateTime.UtcNow).TotalMilliseconds);
        private string _nameRnd;
        public RandomStrategy()
        {
            _nameRnd = _rnd.Next(0, 1000).ToString();
        }
        public string Name => "Player" + _nameRnd;

        public Transaction Turn(MyCell[] myBlocks)
        {
            var rndIndexTarget = _rnd.Next(0, myBlocks.Count());
            var rndIndexSource = _rnd.Next(0, myBlocks.Count());
            var source = myBlocks[rndIndexSource];
            var rndResource = _rnd.Next(0, source.Resources);
            var targetBlock = myBlocks[rndIndexTarget];
            var neighbourRnd = _rnd.Next(1, 6);
            Cell target = null;
            
            switch (neighbourRnd)
            {
                case 1:
                    target = targetBlock.TopLeft;
                    break;
                case 2:
                    target = targetBlock.Top;
                    break;
                case 3:
                    target = targetBlock.TopRight;
                    break;
                case 4:
                    target = targetBlock.BottomRight;
                    break;
                case 5:
                    target = targetBlock.Bottom;
                    break;
                case 6:
                    target = targetBlock.BottomLeft;
                    break;
                default:
                    break;
            }

            return new Transaction(source, target, rndResource);
        }
    }
}
