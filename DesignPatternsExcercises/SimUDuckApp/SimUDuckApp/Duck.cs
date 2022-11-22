using SimUDuckApp.Ducks.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimUDuckApp
{
    public class Duck
    {
        protected FlyingBehavior flyingBehavior;
        protected QuackBehavior quackBehavior;

        public Duck(FlyingBehavior flyingBehavior, QuackBehavior quackBehavior)
        {
            this.flyingBehavior = flyingBehavior;
            this.quackBehavior = quackBehavior;
        }

        public Duck()
        {
        }

        public void PerformFly()
        {
            flyingBehavior.fly();
        }

        public void PerformQuack()
        {
            quackBehavior.Quack();
        }

        public virtual void Display()
        {
            Console.WriteLine("Looks like a Duck!");

        }

        public void Swim()
        {
            Console.WriteLine("I'm swimming");
        }

        public void SetFlyBehavior(FlyingBehavior flyingBehavior)
        {
            this.flyingBehavior = flyingBehavior;
        }
        public void SetQuackBehavior(QuackBehavior quackBehavior)
        {
            this.quackBehavior = quackBehavior;
        }
    }
}
