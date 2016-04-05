namespace Keysme.Web.Controllers.WebApi
{
    using System;
    using System.Linq;
    using System.Web.Http;

    using Automapper;

    using AutoMapper;

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

        [HttpGet]
        [Authorize]
        public IHttpActionResult Get()
        {
            return this.Ok(this.hostsService.Read(this.User.Identity.GetUserId()));
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Post(HostCreateViewModel model)
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

        [HttpPut]
        [Authorize]
        public IHttpActionResult Put(HostUpdateViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var host = this.Mapper.Map<Host>(model.Host);
            var amenities = this.Mapper.Map<Amenities>(model.Amenities);

            try
            {
              this.hostsService.Update(this.User.Identity.GetUserId(), model.HostId, host, amenities);
              return this.Ok();
            }
            catch
            {
              return this.BadRequest();
            }
        }

        [HttpDelete]
        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                this.hostsService.Delete(this.User.Identity.GetUserId(), id);
                return this.Ok();
            }
            catch
            {
                return this.BadRequest();
            }
        }
    }
}