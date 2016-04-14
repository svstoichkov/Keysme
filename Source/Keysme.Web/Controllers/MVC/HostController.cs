namespace Keysme.Web.Controllers.MVC
{
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
    
    using Services.Data.Contracts;

    using ViewModels.Host;

    public class HostController : BaseController
    {
        private readonly IHostsService hostsService;

        public HostController(IHostsService hostsService)
        {
            this.hostsService = hostsService;
        }

        [HttpGet]
        public ActionResult Create()
        {
            var host = this.hostsService.GetWorkInProgressOrCreateNew(this.User.Identity.GetUserId());
            var model = this.Mapper.Map<HostViewModel>(host);

            return this.View(model);
        }

        [HttpGet]
        public ActionResult AddAmenities()
        {
            var host = this.hostsService.GetWorkInProgressOrCreateNew(this.User.Identity.GetUserId());
            var model = this.Mapper.Map<AmenitiesViewModel>(host.Amenities);

            return this.View(model);
        }
    }
}