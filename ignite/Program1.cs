using Apache.Ignite.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgntieDotnet
{
    class Program1
    {
        static void Main(string[] args)
        {
            Ignition.Start();
            Console.ReadKey(); // keep the node running
        }
    }
}
