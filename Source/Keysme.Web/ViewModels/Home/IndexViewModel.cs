namespace Keysme.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using Data.Models;

    public class IndexViewModel
    {
        public IEnumerable<Host> Hosts { get; set; }

        public int Pages { get; set; }

        public int CurrentPage { get; set; }
    }
}