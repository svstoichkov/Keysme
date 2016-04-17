namespace Keysme.Web.Controllers.MVC
{
    using System.Linq;
    using System.Web.Mvc;

    using Services.Data.Contracts;

    using ViewModels.Admin;

    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        private readonly IUsersService usersService;

        public AdminController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet]
        public ActionResult VerifyUser()
        {
            var users = this.usersService.GetAll().Where(x => x.Verification != null && !x.Verification.IsApproved);
            return this.View(new VerifyUsersViewModel { Users = users });
        }

        [HttpPost]
        public ActionResult VerifyUser(string userId)
        {
            this.usersService.Verify(userId);
            return this.RedirectToAction("VerifyUser");
        }
    }
}