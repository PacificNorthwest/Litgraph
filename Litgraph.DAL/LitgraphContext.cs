using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Litgraph.DAL.Entities;

namespace Litgraph.DAL
{
    public class LitgraphContext : IdentityDbContext<UserEntity>
    {
        public LitgraphContext(DbContextOptions<LitgraphContext> options) : base(options) {}
    }
}