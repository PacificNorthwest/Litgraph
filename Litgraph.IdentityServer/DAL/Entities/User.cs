using System;
using Microsoft.AspNetCore.Identity;

namespace Litgraph.IdentityServer.DAL.Entities
{
    public class UserEntity : IdentityUser
    {
        public string JwtToken { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}