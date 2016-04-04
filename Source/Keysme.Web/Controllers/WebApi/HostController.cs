namespace Keysme.Web.Controllers.WebApi
{
    using System;
    using System.Web.Http;

    using Data.Models;

    using Microsoft.AspNet.Identity;

    using Models.Host;

    using Services.Data;

    public class HostController : BaseController
    {
        private readonly IHostsService hostsService;

        public HostController(IHostsService hostsService)
        {
            this.hostsService = hostsService;
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Create(HostViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var host = this.Mapper.Map<Host>(model);

            this.hostsService.Create(this.User.Identity.GetUserId(), host);
            return this.Ok();
        }
    }
}