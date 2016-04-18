namespace Keysme.Web.ViewModels.Admin
{
    using System.Collections.Generic;

    using Data.Models;

    public class VerifyUserViewModel
    {
        public IEnumerable<User> Users { get; set; }
    }
}