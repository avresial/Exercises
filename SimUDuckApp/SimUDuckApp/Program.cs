using SimUDuckApp.Ducks;
using SimUDuckApp.Ducks.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimUDuckApp
{
    class Program
    {
        static void Main(string[] args)
        {

            Duck mallardDuck = new MallardDuck();
            mallardDuck.Display();
            mallardDuck.PerformFly();
            mallardDuck.PerformQuack();

            Duck readHeadDuck = new RedHeadDuck();
            readHeadDuck.Display();
            readHeadDuck.PerformQuack();
            readHeadDuck.PerformFly();

            Duck modelDuck = new ModelDuck();
            modelDuck.Display();
            modelDuck.PerformFly();
            modelDuck.SetFlyBehavior(new FlyRocketPowered());
            modelDuck.PerformFly();
            Console.ReadLine();
        }
    }
}
