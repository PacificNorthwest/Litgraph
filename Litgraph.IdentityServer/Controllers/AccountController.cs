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
using IdentityServer4.Extensions;

namespace Litgraph.IdentityServer.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IIdentityServerInteractionService _identityInteraction;
        private readonly IClientStore _clientStore;
        private readonly IEventService _events;

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

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new SignInModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(SignInModel signInModel)
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

        [HttpGet]
        public IActionResult Signup(string returnUrl)
        {
            return View(new SignUpModel { ReturnUrl = returnUrl });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup([Bind("UserName", "Email", "Password", "PasswordRepeat", "ReturnUrl")]SignUpModel signUpModel)
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

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            var context = await _identityInteraction.GetLogoutContextAsync(logoutId);
            return Redirect(context.PostLogoutRedirectUri);
        }
    }
}