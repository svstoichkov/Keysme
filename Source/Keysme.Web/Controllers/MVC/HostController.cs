namespace Keysme.Web.Controllers.MVC
{
    using System.Web.Mvc;

    using Data;
    using Data.Models;

    using Microsoft.AspNet.Identity;
    
    using Services.Data.Contracts;

    using ViewModels.Host;

    [Authorize]
    public class HostController : BaseController
    {
        private readonly IHostsService hostsService;
        private readonly IRepository<Currency> currencyRepository;

        //TODO: add currency caching
        public HostController(IHostsService hostsService, IRepository<Currency> currencyRepository)
        {
            this.hostsService = hostsService;
            this.currencyRepository = currencyRepository;
        }

        [HttpGet]
        public ActionResult CreateMainInformation()
        {
            var host = this.hostsService.GetWorkInProgressOrCreateNew(this.User.Identity.GetUserId());
            var model = this.Mapper.Map<HostMainInformationViewModel>(host);
            model.Currencies = new SelectList(this.currencyRepository.All(), "Id", "Name");

            return this.View("MainInformation", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMainInformation(HostMainInformationViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("MainInformation", model);
            }

            var host = this.Mapper.Map<Host>(model);
            this.hostsService.AddMainInformation(this.User.Identity.GetUserId(), host);

            return this.View("MainInformation", model);

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