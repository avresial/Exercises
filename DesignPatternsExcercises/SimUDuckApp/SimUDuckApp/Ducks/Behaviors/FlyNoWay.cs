using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimUDuckApp.Ducks.Behaviors
{
    public class FlyNoWay : FlyingBehavior
    {
        public void fly()
        {
            Console.WriteLine("I'm not flying :c");
        }
    }
}
