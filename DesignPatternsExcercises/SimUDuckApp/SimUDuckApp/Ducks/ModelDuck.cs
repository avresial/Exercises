using SimUDuckApp.Ducks.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimUDuckApp.Ducks
{
    public class ModelDuck : Duck
    {

        public ModelDuck()
        {
            flyingBehavior = new FlyNoWay();
            quackBehavior = new Quack();
        }
        public override void Display()
        {
            Console.WriteLine("Looks like model Duck");
        }
    }
}
