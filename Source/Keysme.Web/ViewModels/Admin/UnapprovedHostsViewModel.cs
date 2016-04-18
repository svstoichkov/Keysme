namespace Keysme.Web.ViewModels.Admin
{
    using System.Collections.Generic;

    using Data.Models;

    public class UnapprovedHostsViewModel
    {
        public IEnumerable<Host> Hosts { get; set; }
    }
}