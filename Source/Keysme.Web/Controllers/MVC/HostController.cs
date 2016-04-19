namespace Keysme.Web.Controllers.MVC
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Data;
    using Data.Models;

    using Global;

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
        public ActionResult Details(int id)
        {
            var host = this.hostsService.GetAll().FirstOrDefault(x => x.Id == id);

            if (host == null)
            {
                return this.RedirectToAction("CreateMainInformation");
            }

            var model = new DetailsViewModel
            {
                Host = host,
                ProfileImagePath = Path.Combine(GlobalConstants.UserProfileImageFolder, host.User.ProfileImage)
            };

            return this.View(model);
        }

        [HttpGet]
        public ActionResult CreateMainInformation()
        {
            var host = this.hostsService.GetWorkInProgressOrCreateNew(this.User.Identity.GetUserId());
            var model = this.Mapper.Map<MainInformationViewModel>(host);
            model.Currencies = new SelectList(this.currencyRepository.All().Where(x => x.IsActive), "Id", "Name");
            var checkInItems = new List<SelectListItem>
                                                {
                                                    new SelectListItem { Text = "10 AM", Value = "10" },
                                                    new SelectListItem { Text = "11 AM", Value = "11" },
                                                    new SelectListItem { Text = "12 PM", Value = "12"},
                                                    new SelectListItem { Text = "1 PM", Value = "13" },
                                                    new SelectListItem { Text = "2 PM", Value = "14" },
                                                    new SelectListItem { Text = "3 PM", Value = "15" },
                                                    new SelectListItem { Text = "4 PM", Value = "16" },
                                                };


            this.ViewBag.CheckInItems = checkInItems;

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
            var model = this.Mapper.Map<AmenitiesViewModel>(host);

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

            var host = this.Mapper.Map<Host>(model);
            this.hostsService.CreateAmenities(this.User.Identity.GetUserId(), host);

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
            if (!files.Any())
            {
                return this.RedirectToAction("CreateImages");
            }

            try
            {
                var images = files.Select(file => System.Drawing.Image.FromStream(file.InputStream));

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
            var host = this.hostsService.GetWorkInProgressOrCreateNew(this.User.Identity.GetUserId());
            if (!this.CheckMainInformation(host))
            {
                return this.RedirectToAction("CreateMainInformation");
            }

            if (!this.CheckLocation(host))
            {
                return this.RedirectToAction("CreateLocation");
            }

            if (!this.CheckImages(host))
            {
                return this.RedirectToAction("CreateImages");
            }

            return this.View("Publish");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePublish(string dummy)
        {
            var host = this.hostsService.GetWorkInProgressOrCreateNew(this.User.Identity.GetUserId());
            if (!this.CheckMainInformation(host))
            {
                return this.RedirectToAction("CreateMainInformation");
            }

            if (!this.CheckLocation(host))
            {
                return this.RedirectToAction("CreateLocation");
            }

            if (!this.CheckImages(host))
            {
                return this.RedirectToAction("CreateImages");
            }

            var id = this.hostsService.CreatePublish(this.User.Identity.GetUserId());

            return this.RedirectToAction("Details", new { id = id });
        }

        private bool CheckMainInformation(Host host)
        {
            return host.Title != null
                   && host.Description != null
                   && host.Type != null
                   && host.RoomType != null
                   && host.MaxGuests != null
                   && host.BedsCount != null
                   && host.BathsCount != null
                   && host.Price != null
                   && host.Currency != null
                   && host.CancellationPolicy != null
                   && host.MainPhone != null
                   && host.ReservationPhone != null;
        }

        private bool CheckLocation(Host host)
        {
            return host.Country != null
                   && host.City != null
                   && host.State != null
                   && host.Address != null
                   && host.PostalCode != null
                   && host.Latitude != null
                   && host.Longitude != null;
        }

        private bool CheckImages(Host host)
        {
            return host.Images.Count > 0;
        }
    }
}