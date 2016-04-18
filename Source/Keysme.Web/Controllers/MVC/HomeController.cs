namespace Keysme.Web.Controllers.MVC
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Global;

    using Services.Data.Contracts;

    using ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly IHostsService hostsService;

        public HomeController(IHostsService hostsService)
        {
            this.hostsService = hostsService;
        }

        [HttpGet]
        public ActionResult Index(int id = 1)
        {
            var count = this.hostsService.GetAll().Count(x => x.IsApproved);
            var pages = (int)Math.Floor(count / (double)GlobalConstants.HomePageSize) + 1;
            var hosts = this.hostsService.GetAll().Where(x => x.IsApproved).OrderByDescending(x => x.CreatedOn).Skip((id - 1) * GlobalConstants.HomePageSize).Take(GlobalConstants.HomePageSize);

            return this.View(new IndexViewModel { Hosts = hosts, CurrentPage = id, Pages = pages});
        }
    }
}