using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VoteDbContext.Connection;
using VoteDbContext.Model.Configure;
using VoteDbContext.Model.DTO;

namespace VoteDbContext.Model
{
    public class VoteContext : IdentityDbContext<UserDbDTO>
    {
        public VoteContext(DbContextOptions options) : base(options) { }

        public VoteContext() { }

        public DbSet<AnswerDbDTO> Answers { get; set; }
        public DbSet<TagDbDTO> Tags { get; set; }
        public DbSet<VoteDbDTO> Votes { get; set; }
        public DbSet<VoteStatusDbDTO> VoteStatuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(new ConnectionStringManager().ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AnswerConfig());
            modelBuilder.ApplyConfiguration(new TagConfig());
            modelBuilder.ApplyConfiguration(new VoteConfig());
            modelBuilder.ApplyConfiguration(new VoteStatusConfig());
        }
    }
}
