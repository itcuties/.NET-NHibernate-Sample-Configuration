using System;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using System.Reflection;

namespace ItCuties.NHibernate
{
    class Program
    {
        static void Main(string[] args)
        {
            var cfg = new Configuration();
            cfg.DataBaseIntegration(x =>
            {
                x.ConnectionString = "Server=.\\SqlExpress; Database=ItCutiesDemo; Integrated Security=SSPI;";
                x.Driver<SqlClientDriver>();
                x.Dialect<MsSql2012Dialect>();
            });
            cfg.AddAssembly(Assembly.GetExecutingAssembly());
            var sessionFactory = cfg.BuildSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    var customers = session.CreateCriteria<Customer>()
                        .List<Customer>();
                    foreach (var customer in customers)
                    {
                        Console.WriteLine("{0} {1}", customer.FirstName, customer.LastName);
                    }
                    tx.Commit();
                }
                Console.WriteLine("Press <ENTER> to exit...");
                Console.ReadLine();
            }
        }
    }
}

