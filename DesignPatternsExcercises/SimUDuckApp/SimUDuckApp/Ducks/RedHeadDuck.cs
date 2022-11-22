using SimUDuckApp.Ducks.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimUDuckApp.Ducks
{
    public class RedHeadDuck : Duck
    {
        public RedHeadDuck(FlyingBehavior flyingBehavior, QuackBehavior quackBehavior) : base(flyingBehavior, quackBehavior)
        {
        }
        public RedHeadDuck()
        {
            this.flyingBehavior = new FlyWithWings();
            this.quackBehavior = new Squeak();
        }

        public override void Display()
        {
            Console.WriteLine("Looks like a RedHead Duck!");
        }
    }
}
