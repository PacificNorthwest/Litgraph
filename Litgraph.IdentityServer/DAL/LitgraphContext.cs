using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Litgraph.IdentityServer.DAL.Entities;

namespace Litgraph.IdentityServer.DAL
{
    public class LitgraphContext : IdentityDbContext<UserEntity>
    {
        public LitgraphContext(DbContextOptions<LitgraphContext> options) : base(options) {}
    }
}