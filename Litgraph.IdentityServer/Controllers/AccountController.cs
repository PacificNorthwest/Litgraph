using System;
using Litgraph.IdentityServer.DAL.Entities;
using Litgraph.IdentityServer.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Litgraph.IdentityServer.Model.Authorization;
using Microsoft.AspNetCore.Authorization;
using Litgraph.IdentityServer.Model.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using IdentityServer4.Stores;
using IdentityServer4.Events;
using Litgraph.IdentityServer.DAL;

namespace Litgraph.IdentityServer.Controllers
{
    [Route("/"), AllowAnonymous]
    public class AccountController : Controller
    {
        private SignInManager<UserEntity> _signInManager;
        private UserManager<UserEntity> _userManager;
        private IConfiguration _configuration;
        private IIdentityServerInteractionService _identityInteraction;
        private IClientStore _clientStore;
        private IEventService _events;

        public AccountController(SignInManager<UserEntity> signInManager,
                                 UserManager<UserEntity> userManager,
                                 IConfiguration configuration,
                                 IIdentityServerInteractionService identityInteraction,
                                 IAuthenticationSchemeProvider schemeProvider,
                                 IClientStore clientStore,
                                 IEventService events)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._configuration = configuration;
            this._identityInteraction = identityInteraction;
            this._clientStore = clientStore;
            this._events = events;
        }

        [HttpGet, Route("signin")]
        public IActionResult Signin(string callbackUrl)
        {
            return View(new SignInModel { ReturnUrl = callbackUrl });
        }

        [HttpPost, Route("signin")]
        public async Task<IActionResult> Signin(SignInModel signInModel)
        {
            var context = await this._identityInteraction.GetAuthorizationContextAsync(signInModel.ReturnUrl);

            if (ModelState.IsValid)
            {
                var result = await this._signInManager.PasswordSignInAsync(signInModel.UserName, signInModel.Password, signInModel.RememberLogin, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    var user = await this._userManager.FindByNameAsync(signInModel.UserName);
                    await this._events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName));

                    if (context != null)
                        return Redirect(signInModel.ReturnUrl);
                    if (Url.IsLocalUrl(signInModel.ReturnUrl))
                        return Redirect(signInModel.ReturnUrl);
                    else if (string.IsNullOrEmpty(signInModel.ReturnUrl))
                        return Redirect("~/");
                    else
                        throw new InvalidReturnUrlException();
                }

                await _events.RaiseAsync(new UserLoginFailureEvent(signInModel.UserName, "Invalid credentials"));
                ModelState.AddModelError(string.Empty, "Invalid username or password");
            }

            return View(new SignInModel { ReturnUrl = signInModel.ReturnUrl });
        }

        [HttpGet, Route("signup")]
        public IActionResult Signup(string callbackUrl)
        {
            return View(new SignUpModel { ReturnUrl = callbackUrl });
        }

        [HttpPost, Route("signup"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup([Bind("UserName", "Email", "Password", "PasswordRepeat")]SignUpModel signUpModel)
        {
            if (!ModelState.IsValid)
                return View(signUpModel);
            
            var user = new UserEntity { UserName = signUpModel.UserName, Email = signUpModel.Email };
            var result = await this._userManager.CreateAsync(user, signUpModel.Password);

            if (!result.Succeeded)
                throw new SignUpException("Failed to create user with the given username and password");

            await this._userManager.AddToRoleAsync(user, Roles.USER);
            await this._signInManager.PasswordSignInAsync(user, signUpModel.Password, true, lockoutOnFailure: true);

            var context = await this._identityInteraction.GetAuthorizationContextAsync(signUpModel.ReturnUrl);

            await this._events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName));

            if (context != null)
                return Redirect(signUpModel.ReturnUrl);
            if (Url.IsLocalUrl(signUpModel.ReturnUrl))
                return Redirect(signUpModel.ReturnUrl);
            else if (string.IsNullOrEmpty(signUpModel.ReturnUrl))
                return Redirect("~/");
            else
                throw new InvalidReturnUrlException();
        }

        // [HttpPost, Route("signout"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        // public async Task SignOut()
        // {
        //     if (await this._userManager.GetUserAsync(this.User) is UserEntity user)
        //     {
        //         user.JwtToken = null;
        //         await this._context.SaveChangesAsync();
        //     }
        //     await this._signInManager.SignOutAsync();
        // }
    }
}