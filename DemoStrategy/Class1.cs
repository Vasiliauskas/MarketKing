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
        public string Name
        {
            get
            {
                return "NAME";
            }
        }

        public Transaction Turn(MyCell[] myBlocks)
        {
            throw new NotImplementedException();
        }
    }
}
