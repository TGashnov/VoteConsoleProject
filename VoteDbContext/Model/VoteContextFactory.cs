using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Design;

namespace VoteDbContext.Model
{
    public class VoteContextFactory : IDesignTimeDbContextFactory<VoteContext>
    {
        public VoteContext CreateDbContext(string[] args)
        {
            return new VoteContext();
        }
    }
}
