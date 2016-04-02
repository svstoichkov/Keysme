namespace Keysme.Data.Migrations
{
    using System.Data.Entity.Migrations;

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
