namespace Keysme.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    using Models;

    public class Configuration : DbMigrationsConfiguration<KeysmeDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(KeysmeDbContext context)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var countriesCsv = Path.Combine(basePath, "country.csv");
            var countries = File.ReadAllLines(countriesCsv);

            if (!context.Countries.Any())
            {
                foreach (var country in countries)
                {
                    var split = country.Split(',');
                    var countryModel = new Country
                                       {
                                           Code = split[1],
                                           Name = split[2],
                                           IsActive = true
                                       };

                    context.Countries.Add(countryModel);
                }

                context.SaveChanges();
            }

            context.Currencies.AddOrUpdate(x => x.Name, 
                new Currency { Name = "US Dollar", Code = "USD", Symbol = "$", IsActive = true },
                new Currency { Name = "Euro", Code = "EUR", Symbol = "€", IsActive = true },
                new Currency { Name = "British Pound", Code = "GBP", Symbol = "£", IsActive = true },
                new Currency { Name = "Australian Dollar", Code = "AUD", Symbol = "$", IsActive = true },
                new Currency { Name = "Chinese Yuan", Code = "CNY", Symbol = "¥", IsActive = true },
                new Currency { Name = "Hong Kong Dollar", Code = "HKD", Symbol = "HK$", IsActive = true },
                new Currency { Name = "Singapore Dollar", Code = "SGD", Symbol = "$", IsActive = true });
        }
    }
}
