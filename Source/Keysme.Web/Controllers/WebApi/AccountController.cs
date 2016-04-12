﻿namespace Keysme.Web.Controllers.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;

    using Data.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;

    using Services.Data.Contracts;

    using ViewModels.Account;
    using ViewModels.Profile;

    using Image = System.Drawing.Image;

    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : BaseController
    {
        private readonly IUsersService usersService;
        private ApplicationUserManager _userManager;

        public AccountController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public AccountController(ApplicationUserManager userManager,
                                 ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            this.UserManager = userManager;
            //this.AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return this._userManager ?? this.Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                this._userManager = value;
            }
        }

        [HttpPut]
        public IHttpActionResult Put(ProfileViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            try
            {
                var user = this.Mapper.Map<User>(model);
                this.usersService.Update(this.User.Identity.GetUserId(), user);
                return this.Ok();
            }
            catch
            {
                return this.BadRequest();
            }
        }

        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var result = await this.UserManager.ChangePasswordAsync(this.User.Identity.GetUserId(), model.OldPassword,
                                                                    model.NewPassword);

            if (!result.Succeeded)
            {
                return this.GetErrorResult(result);
            }

            return this.Ok();
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.BirthDate,
                CreatedOn = DateTime.Now
            };

            var result = await this.UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return this.GetErrorResult(result);
            }

            return this.Ok();
        }

        [HttpPost]
        [Route("UploadProfileImage")]
        public IHttpActionResult UploadProfileImage()
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count == 1)
            {
                var postedFile = httpRequest.Files[0];
                try
                {
                    var image = Image.FromStream(postedFile.InputStream);
                    this.usersService.AddProfileImage(this.User.Identity.GetUserId(), image);
                }
                catch
                {
                    return this.BadRequest();
                }

                return this.Ok();
            }

            return this.BadRequest();
        }

        [HttpPost]
        [Route("verify")]
        public IHttpActionResult Verify(string type, string countryCode)
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count == 2)
            {
                var front = httpRequest.Files["front"];
                var back = httpRequest.Files["back"];
                try
                {
                    var frontImage = Image.FromStream(front.InputStream);
                    var backImage = Image.FromStream(back.InputStream);
                    this.usersService.Verify(this.User.Identity.GetUserId(), type, countryCode, frontImage, backImage);
                }
                catch
                {
                    return this.BadRequest();
                }

                return this.Ok();
            }

            return this.BadRequest();
        }



        //public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; }

        // GET api/Account/UserInfo
        //[HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        //[Route("UserInfo")]
        //public UserInfoViewModel GetUserInfo()
        //{
        //    var externalLogin = ExternalLoginData.FromIdentity(this.User.Identity as ClaimsIdentity);
        //
        //    return new UserInfoViewModel
        //    {
        //        Email = this.User.Identity.GetUserName(),
        //        HasRegistered = externalLogin == null,
        //        LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
        //    };
        //}

        // POST api/Account/Logout
        //[Route("Logout")]
        //public IHttpActionResult Logout()
        //{
        //    this.Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
        //    return this.Ok();
        //}

        // POST api/Account/SetPassword
        //[Route("SetPassword")]
        //public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        //{
        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.BadRequest(this.ModelState);
        //    }

        //    var result = await this.UserManager.AddPasswordAsync(this.User.Identity.GetUserId(), model.NewPassword);

        //    if (!result.Succeeded)
        //    {
        //        return this.GetErrorResult(result);
        //    }

        //    return this.Ok();
        //}

        // GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
        //[Route("ManageInfo")]
        //public async Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
        //{
        //    IdentityUser user = await this.UserManager.FindByIdAsync(this.User.Identity.GetUserId());

        //    if (user == null)
        //    {
        //        return null;
        //    }

        //    var logins = new List<UserLoginInfoViewModel>();

        //    foreach (var linkedAccount in user.Logins)
        //    {
        //        logins.Add(new UserLoginInfoViewModel
        //        {
        //            LoginProvider = linkedAccount.LoginProvider,
        //            ProviderKey = linkedAccount.ProviderKey
        //        });
        //    }

        //    if (user.PasswordHash != null)
        //    {
        //        logins.Add(new UserLoginInfoViewModel
        //        {
        //            LoginProvider = LocalLoginProvider,
        //            ProviderKey = user.UserName
        //        });
        //    }

        //    return new ManageInfoViewModel
        //    {
        //        LocalLoginProvider = LocalLoginProvider,
        //        Email = user.UserName,
        //        Logins = logins,
        //        ExternalLoginProviders = this.GetExternalLogins(returnUrl, generateState)
        //    };
        //}

        // POST api/Account/AddExternalLogin
        //[Route("AddExternalLogin")]
        //public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
        //{
        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.BadRequest(this.ModelState);
        //    }

        //    this.Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

        //    var ticket = this.AccessTokenFormat.Unprotect(model.ExternalAccessToken);

        //    if (ticket == null || ticket.Identity == null || (ticket.Properties != null
        //                                                      && ticket.Properties.ExpiresUtc.HasValue
        //                                                      && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
        //    {
        //        return this.BadRequest("External login failure.");
        //    }

        //    var externalData = ExternalLoginData.FromIdentity(ticket.Identity);

        //    if (externalData == null)
        //    {
        //        return this.BadRequest("The external login is already associated with an account.");
        //    }

        //    var result = await this.UserManager.AddLoginAsync(this.User.Identity.GetUserId(),
        //                                                      new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

        //    if (!result.Succeeded)
        //    {
        //        return this.GetErrorResult(result);
        //    }

        //    return this.Ok();
        //}

        // POST api/Account/RemoveLogin
        //[Route("RemoveLogin")]
        //public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
        //{
        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.BadRequest(this.ModelState);
        //    }

        //    IdentityResult result;

        //    if (model.LoginProvider == LocalLoginProvider)
        //    {
        //        result = await this.UserManager.RemovePasswordAsync(this.User.Identity.GetUserId());
        //    }
        //    else
        //    {
        //        result = await this.UserManager.RemoveLoginAsync(this.User.Identity.GetUserId(),
        //                                                         new UserLoginInfo(model.LoginProvider, model.ProviderKey));
        //    }

        //    if (!result.Succeeded)
        //    {
        //        return this.GetErrorResult(result);
        //    }

        //    return this.Ok();
        //}

        // GET api/Account/ExternalLogin
        //[OverrideAuthentication]
        //[HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        //[AllowAnonymous]
        //[Route("ExternalLogin", Name = "ExternalLogin")]
        //public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        //{
        //    if (error != null)
        //    {
        //        return this.Redirect(this.Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
        //    }

        //    if (!this.User.Identity.IsAuthenticated)
        //    {
        //        return new ChallengeResult(provider, this);
        //    }

        //    var externalLogin = ExternalLoginData.FromIdentity(this.User.Identity as ClaimsIdentity);

        //    if (externalLogin == null)
        //    {
        //        return this.InternalServerError();
        //    }

        //    if (externalLogin.LoginProvider != provider)
        //    {
        //        this.Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
        //        return new ChallengeResult(provider, this);
        //    }

        //    var user = await this.UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
        //                                                                  externalLogin.ProviderKey));

        //    var hasRegistered = user != null;

        //    if (hasRegistered)
        //    {
        //        this.Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

        //        var oAuthIdentity = await user.GenerateUserIdentityAsync(this.UserManager,
        //                                                                 OAuthDefaults.AuthenticationType);
        //        var cookieIdentity = await user.GenerateUserIdentityAsync(this.UserManager,
        //                                                                  CookieAuthenticationDefaults.AuthenticationType);

        //        var properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
        //        this.Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
        //    }
        //    else
        //    {
        //        IEnumerable<Claim> claims = externalLogin.GetClaims();
        //        var identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
        //        this.Authentication.SignIn(identity);
        //    }

        //    return this.Ok();
        //}

        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        //[AllowAnonymous]
        //[Route("ExternalLogins")]
        //public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        //{
        //    var descriptions = this.Authentication.GetExternalAuthenticationTypes();
        //    var logins = new List<ExternalLoginViewModel>();

        //    string state;

        //    if (generateState)
        //    {
        //        const int strengthInBits = 256;
        //        state = RandomOAuthStateGenerator.Generate(strengthInBits);
        //    }
        //    else
        //    {
        //        state = null;
        //    }

        //    foreach (var description in descriptions)
        //    {
        //        var login = new ExternalLoginViewModel
        //        {
        //            Name = description.Caption,
        //            Url = this.Url.Route("ExternalLogin", new
        //            {
        //                provider = description.AuthenticationType,
        //                response_type = "token",
        //                client_id = Startup.PublicClientId,
        //                redirect_uri = new Uri(this.Request.RequestUri, returnUrl).AbsoluteUri,
        //                state
        //            }),
        //            State = state
        //        };
        //        logins.Add(login);
        //    }

        //    return logins;
        //}

        // POST api/Account/RegisterExternal
        //[OverrideAuthentication]
        //[HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        //[Route("RegisterExternal")]
        //public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        //{
        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.BadRequest(this.ModelState);
        //    }

        //    var info = await this.Authentication.GetExternalLoginInfoAsync();
        //    if (info == null)
        //    {
        //        return this.InternalServerError();
        //    }

        //    var user = new User { UserName = model.Email, Email = model.Email };

        //    var result = await this.UserManager.CreateAsync(user);
        //    if (!result.Succeeded)
        //    {
        //        return this.GetErrorResult(result);
        //    }

        //    result = await this.UserManager.AddLoginAsync(user.Id, info.Login);
        //    if (!result.Succeeded)
        //    {
        //        return this.GetErrorResult(result);
        //    }
        //    return this.Ok();
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing && this._userManager != null)
            {
                this._userManager.Dispose();
                this._userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager Authentication
        {
            get
            {
                return this.Request.GetOwinContext().Authentication;
            }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return this.InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                    {
                        this.ModelState.AddModelError("", error);
                    }
                }

                if (this.ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return this.BadRequest();
                }

                return this.BadRequest(this.ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }

            public string ProviderKey { get; set; }

            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, this.ProviderKey, null, this.LoginProvider));

                if (this.UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, this.UserName, null, this.LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                var providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || string.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || string.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static readonly RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                var strengthInBytes = strengthInBits / bitsPerByte;

                var data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
    }
}