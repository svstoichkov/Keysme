namespace Keysme.Data.Migrations
{
    using System;
    using System.Collections.Generic;
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
            var countriesOrdered = countries.OrderBy(x => x.Split(',')[2]);
            
            if (!context.Countries.Any())
            {
                foreach (var country in countriesOrdered)
                {
                    var split = country.Split(',');
                    var countryModel = new Country
                                       {
                                           Code = (CountryCode)Enum.Parse(typeof(CountryCode), split[1]),
                                           Name = split[2],
                                           IsActive = true
                                       };
            
                    context.Countries.Add(countryModel);
                }
            
                context.SaveChanges();
            }

            if (!context.Currencies.Any())
            {
                var currencies = new List<Currency>
                                 {
                                     new Currency { Name = "US Dollar", Code = "USD", Symbol = "$", IsActive = true },
                                     new Currency { Name = "Canadian Dollar", Code = "CAD", Symbol = "$", IsActive = true },
                                     new Currency { Name = "Euro", Code = "EUR", Symbol = "€", IsActive = true },
                                     new Currency { Name = "British Pound", Code = "GBP", Symbol = "£", IsActive = true },
                                     new Currency { Name = "Australian Dollar", Code = "AUD", Symbol = "$", IsActive = true },
                                     new Currency { Name = "Chinese Yuan", Code = "CNY", Symbol = "¥", IsActive = true },
                                     new Currency { Name = "Hong Kong Dollar", Code = "HKD", Symbol = "HK$", IsActive = true },
                                     new Currency { Name = "Brazilian Real", Code = "BRL", Symbol = "R$", IsActive = true },
                                     new Currency { Name = "Indian Rupee", Code = "INR", Symbol = "₹", IsActive = true },
                                     new Currency { Name = "Indonesian Rupiah", Code = "IDR", Symbol = "Rp", IsActive = true },
                                     new Currency { Name = "Japanese Yen", Code = "JPY", Symbol = "¥", IsActive = true },
                                     new Currency { Name = "Korean Won", Code = "KRW", Symbol = "₩", IsActive = true },
                                     new Currency { Name = "Malaysian Ringgit", Code = "MYR", Symbol = "RM", IsActive = true },
                                     new Currency { Name = "Mexician Peso", Code = "MXN", Symbol = "$", IsActive = true },
                                     new Currency { Name = "Thai Baht", Code = "THB", Symbol = "฿", IsActive = true },
                                     new Currency { Name = "Emirati Dirham", Code = "AED", Symbol = "Dirham", IsActive = true },
                                     new Currency { Name = "Singapore Dollar", Code = "SGD", Symbol = "$", IsActive = true }
                                 };

                foreach (var currency in currencies)
                {
                    context.Currencies.Add(currency);
                }

                context.SaveChanges();
            }
        }
    }
}
