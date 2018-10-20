using System;
using Litgraph.DAL.Entities;
using Litgraph.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Litgraph.Server.Model.Authorization;
using Microsoft.AspNetCore.Authorization;
using Litgraph.Server.Model.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Litgraph.Server.Controllers
{
    [Route("api/auth"), ApiController]
    public class AuthorizationController : ControllerBase
    {
        private SignInManager<UserEntity> _signInManager;
        private UserManager<UserEntity> _userManager;
        private IConfiguration _configuration;
        private Litgraph.DAL.LitgraphContext _context;

        public AuthorizationController(SignInManager<UserEntity> signInManager,
                                       UserManager<UserEntity> userManager,
                                       IConfiguration configuration,
                                       Litgraph.DAL.LitgraphContext context)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._configuration = configuration;
            this._context = context;
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

            return new OkObjectResult(new { token = await this.GenerateToken(await this._userManager.FindByNameAsync(request.UserName)) });
        }

        [HttpPost, Route("signout"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task SignOut()
        {
            if (await this._userManager.GetUserAsync(this.User) is UserEntity user)
            {
                user.JwtToken = null;
                await this._context.SaveChangesAsync();
            }
            await this._signInManager.SignOutAsync();
        }

        private async Task<string> GenerateToken(UserEntity user)
        {
            var claims = (await this._userManager.GetRolesAsync(user))
                            .Select(r => new Claim(ClaimTypes.Role, r))
                            .Append(new Claim(ClaimTypes.Name, user.UserName));

            var token = new JwtSecurityTokenHandler().WriteToken(
                        new JwtSecurityToken(
                                issuer: this._configuration.GetValue<string>("JwtCreds:Issuer"),
                                audience: "LitgraphUser",
                                expires: DateTime.Now.AddDays(this._configuration.GetValue<int>("JwtCreds:ExpDays")),
                                claims: claims,
                                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(
                                                    new SymmetricSecurityKey(
                                                        Encoding.UTF8.GetBytes(this._configuration.GetValue<string>("JwtCreds:Key"))),
                                                        SecurityAlgorithms.HmacSha256Signature)
            ));

            user.JwtToken = token;
            await this._context.SaveChangesAsync();

            return token;
        }
    }
}