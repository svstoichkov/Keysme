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
            var model = this.Mapper.Map<MainInformationViewModel>(host);
            model.Currencies = new SelectList(this.currencyRepository.All(), "Id", "Name");

            return this.View("MainInformation", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMainInformation(MainInformationViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Currencies = new SelectList(this.currencyRepository.All(), "Id", "Name");
                return this.View("MainInformation", model);
            }

            var host = this.Mapper.Map<Host>(model);
            this.hostsService.CreateMainInformation(this.User.Identity.GetUserId(), host);

            return this.RedirectToAction("CreateLocation");

        }

        [HttpGet]
        public ActionResult CreateLocation()
        {
            var host = this.hostsService.GetWorkInProgressOrCreateNew(this.User.Identity.GetUserId());
            var model = this.Mapper.Map<LocationViewModel>(host);

            return this.View("Location", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateLocation(LocationViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("Location", model);
            }

            var host = this.Mapper.Map<Host>(model);
            this.hostsService.CreateLocation(this.User.Identity.GetUserId(), host);

            return this.RedirectToAction("CreateAmenities");
        }

        [HttpGet]
        public ActionResult CreateAmenities()
        {
            var host = this.hostsService.GetWorkInProgressOrCreateNew(this.User.Identity.GetUserId());
            var model = this.Mapper.Map<AmenitiesViewModel>(host.Amenities);

            return this.View("Amenities", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAmenities(AmenitiesViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("Amenities", model);
            }

            var amenities = this.Mapper.Map<Amenities>(model);
            this.hostsService.CreateAmenities(this.User.Identity.GetUserId(), amenities);

            return this.RedirectToAction("CreateAmenities");
        }
    }
}