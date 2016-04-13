﻿namespace Keysme.Web.Controllers.MVC
{
    using System;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using Data.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    using Services.Data.Contracts;

    using ViewModels.Profile;

    using Image = System.Drawing.Image;

    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly IUsersService usersService;

        public ProfileController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public ApplicationSignInManager SignInManager => this.HttpContext.GetOwinContext().Get<ApplicationSignInManager>();

        public ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        [HttpGet]
        public ActionResult Index()
        {
            var user = this.usersService.GetUser(this.User.Identity.GetUserId());
            var model = new ProfileViewModel();
            model.ChangeInfoViewModel = this.Mapper.Map<ChangeInfoViewModel>(user);
            model.ChangePasswordViewModel = new ChangePasswordViewModel();
            model.RequestVerificationViewModel = new RequestVerificationViewModel();
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeInfo(ChangeInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.RedirectToAction("Index");
            }

            var monthDaysCount = DateTime.DaysInMonth(model.BirthYear, model.BirthMonth);
            var birthDate = new DateTime(model.BirthYear, model.BirthMonth, model.BirthDay > monthDaysCount ? monthDaysCount : model.BirthDay);
            model.BirthDate = birthDate;

            var user = this.Mapper.Map<User>(model);
            this.usersService.Update(this.User.Identity.GetUserId(), user);

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Index");
            }
            var result = await this.UserManager.ChangePasswordAsync(this.User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index");
            }
            //AddErrors(result);
            return this.RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadProfileImage(HttpPostedFileBase file)
        {
            try
            {
                var image = Image.FromStream(file.InputStream);
                this.usersService.AddProfileImage(this.User.Identity.GetUserId(), image);

                return this.RedirectToAction("Index");
            }
            catch
            {
                return this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RequestVerification(RequestVerificationViewModel model)
        {
            try
            {
                var frontImage = Image.FromStream(model.Front.InputStream);
                var backImage = Image.FromStream(model.Back.InputStream);
                this.usersService.Verify(this.User.Identity.GetUserId(), model.VerificationType, model.CountryCode, frontImage, backImage);

                return this.RedirectToAction("Index");
            }
            catch
            {
                return this.RedirectToAction("Index");
            }
        }
    }
}