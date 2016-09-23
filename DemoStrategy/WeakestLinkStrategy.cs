using MarketKing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoStrategy
{
    public class WeakestLinkStrategy : IStrategy
    {
        public string Name
        {
            get
            {
                return "WeakestLink";
            }
        }

        public Transaction Turn(MyCell[] myBlocks)
        {
            //myBlocks.SelectMany(m=>m.Neighbours).Where(n=>n.OwnedById.HasValue.OrderBy(n=>n.Resources)
            throw new NotImplementedException();
        }
    }
}
