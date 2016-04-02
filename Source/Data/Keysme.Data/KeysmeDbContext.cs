namespace Keysme.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Models;

    public class KeysmeDbContext : IdentityDbContext<User>, IKeysmeDbContext
    {
        public KeysmeDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public IDbSet<Host> Hosts { get; set; }

        public IDbSet<Currency> Currencies { get; set; }

        public static KeysmeDbContext Create()
        {
            return new KeysmeDbContext();
        }
    }
}