using Apache.Ignite.Core;
using Apache.Ignite.Core.Cache;
using System;

namespace IgntieDotnet
{
    class Program3
    {
        public static void Main(string[] args)
        {
            IIgnite ignite = Ignition.Start();
            ICache<int, string> cache = ignite.GetOrCreateCache<int, string>("test");
            if (cache.PutIfAbsent(1, "Hello, World!"))
                Console.WriteLine("Added a value to the cache!");
            else
                Console.WriteLine(cache.Get(1));

            Console.ReadLine();
        }
    }
}
