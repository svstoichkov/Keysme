namespace Keysme.Web.Controllers.WebApi
{
    using System;
    using System.Linq;
    using System.Web.Http;

    using Global;

    using Services.Data.Contracts;
    
    using ViewModels.Host;

    [RoutePrefix("api/home")]
    public class HomeController : BaseController
    {
        private readonly IHostsService hostsService;

        public HomeController(IHostsService hostsService)
        {
            this.hostsService = hostsService;
        }

        [HttpGet]
        [Route("Index")]
        public IHttpActionResult Index(int id = 1)
        {
            var count = this.hostsService.GetAll().Count(x => x.IsApproved);
            var pages = (int)Math.Floor(count / (double)GlobalConstants.HomePageSize) + 1;
            var hosts = this.hostsService.GetAll().Where(x => x.IsApproved).OrderByDescending(x => x.CreatedOn).Skip((id - 1) * GlobalConstants.HomePageSize).Take(GlobalConstants.HomePageSize).ToList();

            var model = new
            {
                CurrentPage = id,
                Pages = pages,
                Hosts = hosts.Select(x => new
                {
                    MainInformation = this.Mapper.Map<MainInformationViewModel>(x),
                    Amenities = this.Mapper.Map<AmenitiesViewModel>(x),
                    Location = this.Mapper.Map<LocationViewModel>(x),
                    Images = x.Images.Select(y => y.Filename),
                    ProfileImagePath = x.User.ProfileImage
                })
            };

            return this.Ok(model);
        }
    }
}