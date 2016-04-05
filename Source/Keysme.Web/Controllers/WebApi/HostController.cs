namespace Keysme.Web.Controllers.WebApi
{
    using System.Web.Http;

    using Data.Models;

    using Microsoft.AspNet.Identity;

    using Services.Data;

    using ViewModels.Host;

    [Authorize]
    public class HostController : BaseController
    {
        private readonly IHostsService hostsService;

        public HostController(IHostsService hostsService)
        {
            this.hostsService = hostsService;
        }

        [HttpGet]
        public IHttpActionResult GetOwn()
        {
            return this.Ok(this.hostsService.GetOwn(this.User.Identity.GetUserId()));
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetAll()
        {
            return this.Ok(this.hostsService.GetAll());
        }

        [HttpPost]
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