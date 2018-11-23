using Apache.Ignite.Core;
using Apache.Ignite.Core.Binary;
using Apache.Ignite.Core.Cache;
using Apache.Ignite.Core.Cache.Configuration;
using Apache.Ignite.Core.Cache.Query;
using System;

namespace IgntieDotnet
{
    class Program7
    {
        public static void Main(string[] args)
        {
            var cfg = new IgniteConfiguration
            {
                BinaryConfiguration = new BinaryConfiguration(typeof(Person2),
                                                         typeof(Organization))
            };
            IIgnite ignite = Ignition.Start(cfg);

            ICache<int, Person2> personCache = ignite.GetOrCreateCache<int, Person2>(
                new CacheConfiguration("persons", typeof(Person2)));

            var orgCache = ignite.GetOrCreateCache<int, Organization>(
                new CacheConfiguration("orgs", typeof(Organization)));

            personCache[1] = new Person2 { Name = "John Doe", Age = 27, OrgId = 1 };
            personCache[2] = new Person2 { Name = "Jane Moe", Age = 43, OrgId = 2 };
            personCache[3] = new Person2 { Name = "Ivan Petrov", Age = 59, OrgId = 2 };

            orgCache[1] = new Organization { Id = 1, Name = "Contoso" };
            orgCache[2] = new Organization { Id = 2, Name = "Apache" };

            var fieldsQuery = new SqlFieldsQuery(
                "select Person2.Name from Person2 " +
                "join \"orgs\".Organization as org on (Person2.OrgId = org.Id) " +
                "where org.Name = ?", "Apache");

            foreach (var fieldList in personCache.QueryFields(fieldsQuery))
                Console.WriteLine(fieldList[0]);  // Jane Moe, Ivan Petrov
        }
    }
    class Person2
    {
        [QuerySqlField]
        public string Name { get; set; }

        [QuerySqlField]
        public int Age { get; set; }

        [QuerySqlField]
        public int OrgId { get; set; }
    }

    class Organization
    {
        [QuerySqlField]
        public string Name { get; set; }

        [QuerySqlField]
        public int Id { get; set; }
    }
}
