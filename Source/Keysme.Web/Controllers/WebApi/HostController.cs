namespace Keysme.Web.Controllers.WebApi
{
    using System.Web.Http;

    using Data.Models;

    using Microsoft.AspNet.Identity;

    using Models.Host;

    using Services.Data;

    using ViewModels.Host;

    public class HostController : BaseController
    {
        private readonly IHostsService hostsService;

        public HostController(IHostsService hostsService)
        {
            this.hostsService = hostsService;
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Create(HostCreateViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var host = this.Mapper.Map<Host>(model.Host);
            var amenities = this.Mapper.Map<Amenities>(model.Amenities);

            this.hostsService.Create(this.User.Identity.GetUserId(), host, amenities);
            return this.Ok();
        }
    }
}