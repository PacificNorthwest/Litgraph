using System.Threading.Tasks;
using IdentityServer4.Services;
using Litgraph.IdentityServer.Model.Error;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Litgraph.IdentityServer.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService _interactions;
        private readonly IHostingEnvironment _env;

        public HomeController(IIdentityServerInteractionService interactionService, IHostingEnvironment env)
        {
            this._interactions = interactionService;
            this._env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Error(string errorId)
        {
            var errorModel = new Error();

            var errorMessage = await this._interactions.GetErrorContextAsync(errorId);
            if (errorMessage != null)
            {
                errorModel.Message = errorMessage;
                if (!this._env.IsDevelopment())
                    errorModel.Message.ErrorDescription = null;
            }

            return View("Error", errorModel);
        }
    }
}