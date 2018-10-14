using System;
using Litgraph.DAL.Entities;
using Litgraph.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Litgraph.Server.Model.Authorization;
using Microsoft.AspNetCore.Authorization;
using Litgraph.Server.Model.Exceptions;

namespace Litgraph.Server.Controllers
{
    [Route("api")]
    public class AuthorizationController : Controller
    {
        private SignInManager<UserEntity> _signInManager;
        private UserManager<UserEntity> _userManager;

        public AuthorizationController(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
        }

        [HttpPost, Route("signup")]
        public async Task<IActionResult> SignUp([FromBody]SignUpRequest signUpRequest)
        {
            var result = await this._userManager.CreateAsync(new UserEntity 
            { 
                UserName = signUpRequest.UserName, 
                Email = signUpRequest.Email
            }, signUpRequest.Password);

            if (!result.Succeeded)
                throw new SignUpException(string.Join(';', result.Errors));

            return new OkResult();
        }

        [HttpPost, Route("signin")]
        public async Task<IActionResult> SignIn([FromBody]SignInRequest request)
        {
            var result = await this._signInManager.PasswordSignInAsync(request.UserName, request.Password, true, true);
            if (!result.Succeeded)
                throw new AuthorizationExceptions();
            
            return new OkResult();
        }

        [HttpPost, Authorize, Route("signout")]
        public async Task SignOut()
        {
            await this._signInManager.SignOutAsync();
        }
    }
}