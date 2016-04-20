namespace Keysme.Web.Controllers.WebApi
{
    using System.Linq;
    using System.Web;
    using System.Web.Http;

    using Data;
    using Data.Models;

    using Microsoft.AspNet.Identity;

    using Services.Data.Contracts;

    using ViewModels.Host;

    [Authorize]
    [RoutePrefix("api/host")]
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
        [Route("Details")]
        public IHttpActionResult Details(int id)
        {
            var host = this.hostsService.GetAll().FirstOrDefault(x => x.Id == id);

            if (host == null)
            {
                return this.NotFound();
            }

            var model = new
            {
                MainInformation = this.Mapper.Map<MainInformationViewModel>(host),
                Amenities = this.Mapper.Map<AmenitiesViewModel>(host),
                Location = this.Mapper.Map<LocationViewModel>(host),
                Images = host.Images.Select(y => y.Filename),
                ProfileImagePath = host.User.ProfileImage
            };

            return this.Ok(model);
        }

        [HttpGet]
        [Route("CreateMainInformation")]
        public IHttpActionResult CreateMainInformation()
        {
            var host = this.hostsService.GetWorkInProgressOrCreateNew(this.User.Identity.GetUserId());
            var model = this.Mapper.Map<MainInformationViewModel>(host);

            return this.Ok(new
            {
                Model = model,
                Currency = this.currencyRepository.All().FirstOrDefault(x => x.Id == host.CurrencyId)
            });
        }

        [HttpPost]
        [Route("CreateMainInformation")]
        public IHttpActionResult CreateMainInformation(MainInformationViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var host = this.Mapper.Map<Host>(model);
            this.hostsService.CreateMainInformation(this.User.Identity.GetUserId(), host);

            return this.Ok();

        }

        [HttpGet]
        [Route("CreateLocation")]
        public IHttpActionResult CreateLocation()
        {
            var host = this.hostsService.GetWorkInProgressOrCreateNew(this.User.Identity.GetUserId());
            var model = this.Mapper.Map<LocationViewModel>(host);

            return this.Ok(model);
        }

        [HttpPost]
        [Route("CreateLocation")]
        public IHttpActionResult CreateLocation(LocationViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var host = this.Mapper.Map<Host>(model);
            this.hostsService.CreateLocation(this.User.Identity.GetUserId(), host);

            return this.Ok();
        }

        [HttpGet]
        [Route("CreateAmenities")]
        public IHttpActionResult CreateAmenities()
        {
            var host = this.hostsService.GetWorkInProgressOrCreateNew(this.User.Identity.GetUserId());
            var model = this.Mapper.Map<AmenitiesViewModel>(host);

            return this.Ok(model);
        }

        [HttpPost]
        [Route("CreateAmenities")]
        public IHttpActionResult CreateAmenities(AmenitiesViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var host = this.Mapper.Map<Host>(model);
            this.hostsService.CreateAmenities(this.User.Identity.GetUserId(), host);

            return this.Ok();
        }

        [HttpGet]
        [Route("CreateImages")]
        public IHttpActionResult CreateImages()
        {
            var host = this.hostsService.GetWorkInProgressOrCreateNew(this.User.Identity.GetUserId());
            var model = host.Images.Select(x => x.Filename);

            return this.Ok(model);
        }

        [HttpPost]
        [Route("CreateImages")]
        public IHttpActionResult PostCreateImages()
        {
            var files = HttpContext.Current.Request.Files;

            if (files.Count == 0)
            {
                return this.BadRequest();
            }

            try
            {
                var images = files.AllKeys.Select(key => System.Drawing.Image.FromStream(files[key].InputStream));
                this.hostsService.CreateImages(this.User.Identity.GetUserId(), images);
            }
            catch
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        [HttpGet]
        [Route("CreatePublish")]
        public IHttpActionResult CreatePublish()
        {
            var host = this.hostsService.GetWorkInProgressOrCreateNew(this.User.Identity.GetUserId());
            if (!this.CheckMainInformation(host))
            {
                return this.BadRequest("CreateMainInformation");
            }

            if (!this.CheckLocation(host))
            {
                return this.BadRequest("CreateLocation");
            }

            if (!this.CheckImages(host))
            {
                return this.BadRequest("CreateMainInformation");
            }

            return this.Ok();
        }

        [HttpPost]
        [Route("CreatePublish")]
        public IHttpActionResult PostCreatePublish()
        {
            var host = this.hostsService.GetWorkInProgressOrCreateNew(this.User.Identity.GetUserId());
            if (!this.CheckMainInformation(host))
            {
                return this.BadRequest("CreateMainInformation");
            }

            if (!this.CheckLocation(host))
            {
                return this.BadRequest("CreateLocation");
            }

            if (!this.CheckImages(host))
            {
                return this.BadRequest("CreateMainInformation");
            }

            var id = this.hostsService.CreatePublish(this.User.Identity.GetUserId());

            return this.Ok(id);
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
                   && host.MainPhone != null;
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

















        //private readonly IHostsService hostsService;

        //public HostController(IHostsService hostsService)
        //{
        //    this.hostsService = hostsService;
        //}

        //[HttpGet]
        //public IHttpActionResult GetOwn()
        //{
        //    return this.Ok(this.hostsService.GetOwn(this.User.Identity.GetUserId()));
        //}

        //[HttpGet]
        //[AllowAnonymous]
        //public IHttpActionResult GetAll()
        //{
        //    return this.Ok(this.hostsService.GetAll());
        //}

        //[HttpPost]
        //public IHttpActionResult Create(HostCreateViewModel model)
        //{
        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.BadRequest(this.ModelState);
        //    }

        //    var host = this.Mapper.Map<Host>(model.MainInformation);
        //    var amenities = this.Mapper.Map<Host>(model.Amenities);

        //    this.hostsService.Create(this.User.Identity.GetUserId(), host, amenities);
        //    return this.Ok();
        //}

        //[HttpPut]
        //public IHttpActionResult Edit(HostUpdateViewModel model)
        //{
        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.BadRequest(this.ModelState);
        //    }

        //    var host = this.Mapper.Map<Host>(model.MainInformation);
        //    var amenities = this.Mapper.Map<Host>(model.Amenities);

        //    try
        //    {
        //        this.hostsService.Update(this.User.Identity.GetUserId(), model.HostId, host, amenities);
        //        return this.Ok();
        //    }
        //    catch
        //    {
        //        return this.BadRequest();
        //    }
        //}

        //[HttpDelete]
        //public IHttpActionResult Delete(int id)
        //{
        //    try
        //    {
        //        this.hostsService.Delete(this.User.Identity.GetUserId(), id);
        //        return this.Ok();
        //    }
        //    catch
        //    {
        //        return this.BadRequest();
        //    }
        //}
    }
}