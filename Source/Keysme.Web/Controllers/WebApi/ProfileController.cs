namespace Keysme.Web.Controllers.WebApi
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;

    using Data.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    using Services.Data.Contracts;

    using ViewModels.Profile;

    using Image = System.Drawing.Image;

    [Authorize]
    [RoutePrefix("api/profile")]
    public class ProfileController : BaseController
    {
        private readonly IUsersService usersService;

        public ProfileController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public ApplicationUserManager UserManager => this.Request.GetOwinContext().GetUserManager<ApplicationUserManager>();

        [HttpGet]
        [Route("GetProfile")]
        public IHttpActionResult GetProfile()
        {
            var user = this.usersService.GetUser(this.User.Identity.GetUserId());

            var model = new ProfileViewModel();
            model.ChangeInfoViewModel = this.Mapper.Map<ChangeInfoViewModel>(user);
            model.ChangePasswordViewModel = new ChangePasswordViewModel();
            model.RequestVerificationViewModel = new RequestVerificationViewModel();
            if (user.Verification != null)
            {
                model.IsVerified = user.Verification.IsApproved;
                model.RequestVerificationViewModel.HasRequestedVerification = true;
                model.RequestVerificationViewModel.CountryCode = user.Verification.CountryCode;
                model.RequestVerificationViewModel.VerificationType = user.Verification.Type;
                model.RequestVerificationViewModel.FrontImagePath = user.Verification.FrontPicture;
                model.RequestVerificationViewModel.BackImagePath = user.Verification.BackPicture;
            }
            return this.Ok(model);
        }

        [HttpPost]
        [Route("ChangeInfo")]
        public IHttpActionResult ChangeInfo(ChangeInfoViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var monthDaysCount = DateTime.DaysInMonth(model.BirthYear, model.BirthMonth);
            var birthDate = new DateTime(model.BirthYear, model.BirthMonth, model.BirthDay > monthDaysCount ? monthDaysCount : model.BirthDay);
            model.BirthDate = birthDate;

            var user = this.Mapper.Map<User>(model);
            this.usersService.Update(this.User.Identity.GetUserId(), user);

            return this.Ok();
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var result = await this.UserManager.ChangePasswordAsync(this.User.Identity.GetUserId(), model.OldPassword,
                                                                    model.NewPassword);

            if (!result.Succeeded)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        //[HttpPut]
        //[Route("Put")]
        //public IHttpActionResult Put(ProfileViewModel model)
        //{
        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.BadRequest(this.ModelState);
        //    }

        //    try
        //    {
        //        var user = this.Mapper.Map<User>(model);
        //        this.usersService.Update(this.User.Identity.GetUserId(), user);
        //        return this.Ok();
        //    }
        //    catch
        //    {
        //        return this.BadRequest();
        //    }
        //}

        [HttpPost]
        [Route("UploadProfileImage")]
        public IHttpActionResult UploadProfileImage()
        {
            try
            {
                var image = Image.FromStream(HttpContext.Current.Request.Files[0].InputStream);
                this.usersService.AddProfileImage(this.User.Identity.GetUserId(), image);

                return this.Ok();
            }
            catch
            {
                return this.BadRequest();
            }
        }

        [HttpPost]
        [Route("RequestVerification")]
        public IHttpActionResult RequestVerification(VerificationType type, CountryCode countryCode)
        {
            try
            {
                var frontImage = Image.FromStream(HttpContext.Current.Request.Files["front"].InputStream);
                var backImage = Image.FromStream(HttpContext.Current.Request.Files["back"].InputStream);
                this.usersService.RequestVerification(this.User.Identity.GetUserId(), type, countryCode, frontImage, backImage);

                return this.Ok();
            }
            catch
            {
                return this.BadRequest();
            }
        }
    }
}