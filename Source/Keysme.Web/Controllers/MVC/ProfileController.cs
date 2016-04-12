namespace Keysme.Web.Controllers.MVC
{
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using Services.Data.Contracts;

    using ViewModels.Profile;

    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly IUsersService usersService;

        public ProfileController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var user = this.usersService.GetUser(this.User.Identity.GetUserId());
            var model = this.Mapper.Map<ProfileViewModel>(user);
            return this.View(model);
        }
    }
}