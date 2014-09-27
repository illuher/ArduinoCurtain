using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curtain
{
    class Program
    {
        static void Main(string[] args)
        {
            CurtainDriver.Curtain curtain = new CurtainDriver.Curtain("COM4", 9600);
            curtain.Connect();
            
            int position = curtain.GetPosition();

            string s;
            
            while (true)
            {
                Console.WriteLine("Current position is {0}", curtain.GetPosition());
                s = Console.ReadLine();
                position = int.Parse(s);

                curtain.MoveToPosition(position);
            }



            //Console.WriteLine("Current position is {0}", position);
            //curtain.MoveToPosition(0);
            ////Console.WriteLine("Current position is {0}", position);
            //Console.ReadLine();
        }
    }
}
