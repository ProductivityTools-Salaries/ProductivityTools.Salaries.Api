using DbUp;
using Microsoft.Extensions.Configuration;
using ProductivityTools.MasterConfiguration;
using System;
using System.Linq;
using System.Reflection;


namespace ProductivityTools.Salaries.Api.DbUp
{
    class Program
    {
        static int Main(string[] args)
        {
            IConfigurationRoot configuration =
                new ConfigurationBuilder()
                .AddMasterConfiguration(configName: "ProductivityTools.Salaries.Api.json", force: true)
                .Build();

            var masterConnectionString = configuration["ConnectionString"];
            var connectionString = args.FirstOrDefault() ?? masterConnectionString;

            EnsureDatabase.For.SqlDatabase(connectionString);
            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
#if DEBUG
                Console.ReadLine();
#endif
                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            return 0;
        }
    }
}
