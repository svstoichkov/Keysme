namespace Keysme.Web.Controllers.MVC
{
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using Services.Data.Contracts;

    using ViewModels.Admin;

    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        private readonly IUsersService usersService;
        private readonly IHostsService hostsService;

        public AdminController(IUsersService usersService, IHostsService hostsService)
        {
            this.usersService = usersService;
            this.hostsService = hostsService;
        }

        [HttpGet]
        public ActionResult VerifyUser()
        {
            var users = this.usersService.GetAll().Where(x => x.Verification != null && !x.Verification.IsApproved);
            return this.View(new VerifyUserViewModel { Users = users });
        }

        [HttpPost]
        public ActionResult VerifyUser(string userId)
        {
            this.usersService.Verify(userId);
            return this.RedirectToAction("VerifyUser");
        }

        [HttpGet]
        public ActionResult UnapprovedHosts()
        {
            var hosts = this.hostsService.GetAll().Where(x => !x.IsApproved);
            return this.View(new UnapprovedHostsViewModel { Hosts = hosts });
        }

        [HttpPost]
        public ActionResult ApproveHost(int hostId)
        {
            this.hostsService.Approve(this.User.Identity.GetUserId(), hostId);
            return this.RedirectToAction("VerifyUser");
        }

        [HttpPost]
        public ActionResult DeleteHost(int hostId)
        {
            this.hostsService.DeleteAdmin(this.User.Identity.GetUserId(), hostId);
            return this.RedirectToAction("Details", "Host", new { id = hostId });
        }
    }
}