using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassInheritance
{
    internal class Dog : Creature, IFourLeggedWalker
    {
        public void FourLeggedWalk()
        {
            throw new NotImplementedException();
        }
    }
}
