namespace Keysme.Web.Controllers.MVC
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
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

            return this.RedirectToAction("CreateImages");
        }

        [HttpGet]
        public ActionResult CreateImages()
        {
            var host = this.hostsService.GetWorkInProgressOrCreateNew(this.User.Identity.GetUserId());
            var model = host.Images;

            return this.View("Images", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateImages(IEnumerable<HttpPostedFileBase> files)
        {
            var images = files.Select(file => System.Drawing.Image.FromStream(file.InputStream));

            try
            {
                this.hostsService.CreateImages(this.User.Identity.GetUserId(), images);
            }
            catch
            {
                //TODO: show error
                return this.RedirectToAction("CreateImages");
            }

            return this.RedirectToAction("CreatePublish");
        }

        [HttpGet]
        public ActionResult CreatePublish()
        {
            //TODO: add checking

            return this.View("Publish");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePublish(string dummy)
        {
            //TODO: add checking

            this.hostsService.CreatePublish(this.User.Identity.GetUserId());

            return this.RedirectToAction("CreateMainInformation");
        }
    }
}