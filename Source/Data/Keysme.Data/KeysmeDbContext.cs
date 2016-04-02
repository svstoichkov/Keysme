namespace Keysme.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;

    using Models;

    public class KeysmeDbContext : IdentityDbContext<User>
    {
        public KeysmeDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static KeysmeDbContext Create()
        {
            return new KeysmeDbContext();
        }
    }
}