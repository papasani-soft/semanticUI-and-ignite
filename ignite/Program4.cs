using Apache.Ignite.Core;
using Apache.Ignite.Core.Binary;
using Apache.Ignite.Core.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgntieDotnet
{
    class Program4
    {
        static void Main()
        {
            var cfg = new IgniteConfiguration
            {
                // Register custom class for Ignite serialization
                BinaryConfiguration = new BinaryConfiguration(typeof(Person))
            };
            IIgnite ignite = Ignition.Start(cfg);

            ICache<int, Person> cache = ignite.GetOrCreateCache<int, Person>("persons");
            cache[1] = new Person { Name = "John Doe", Age = 27 };

            foreach (ICacheEntry<int, Person> cacheEntry in cache)
                Console.WriteLine(cacheEntry);

            var binCache = cache.WithKeepBinary<int, IBinaryObject>();
            IBinaryObject binPerson = binCache[1];
            Console.WriteLine(binPerson.GetField<string>("Name"));

            Console.ReadLine();
        }
    }
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"Person [Name={Name}, Age={Age}]";
        }
    }
}
