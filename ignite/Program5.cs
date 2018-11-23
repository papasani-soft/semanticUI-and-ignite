using Apache.Ignite.Core;
using Apache.Ignite.Core.Binary;
using Apache.Ignite.Core.Cache;
using Apache.Ignite.Core.Cache.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgntieDotnet
{
    class Program5
    {
        static void Main()
        {
            var cfg = new IgniteConfiguration
            {
                BinaryConfiguration = new BinaryConfiguration(typeof(Person1),
                    typeof(PersonFilter))
            };
            IIgnite ignite = Ignition.Start(cfg);

            ICache<int, Person1> cache = ignite.GetOrCreateCache<int, Person1>("persons");
            cache[1] = new Person1 { Name = "John Doe", Age = 27 };
            cache[2] = new Person1 { Name = "Jane Moe", Age = 43 };

            var scanQuery = new ScanQuery<int, Person1>(new PersonFilter());
            IQueryCursor<ICacheEntry<int, Person1>> queryCursor = cache.Query(scanQuery);

            foreach (ICacheEntry<int, Person1> cacheEntry in queryCursor)
                Console.WriteLine(cacheEntry);

            Console.ReadLine();
        }
    }
    class Person1
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"Person1 [Name={Name}, Age={Age}]";
        }
    }

    class PersonFilter : ICacheEntryFilter<int, Person1>
    {
        public bool Invoke(ICacheEntry<int, Person1> entry)
        {
            return entry.Value.Age > 30;
        }
    }

}
