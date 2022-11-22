using SimUDuckApp.Ducks.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimUDuckApp.Ducks
{
    public class MallardDuck : Duck
    {
        public MallardDuck(FlyingBehavior flyingBehavior, QuackBehavior quackBehavior) : base(flyingBehavior, quackBehavior)
        {
        }

        public MallardDuck()
        {
            this.flyingBehavior = new FlyWithWings();
            this.quackBehavior = new Quack();
        }

        public override void Display()
        {
            Console.WriteLine("Looks like a Mallard Duck!");

        }
    }
}
