using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VoteDbContext.Model.DTO;

namespace VoteDbContext.Model.Configure
{
    public class VoteStatusConfig : IEntityTypeConfiguration<VoteStatusDbDTO>
    {
        public void Configure(EntityTypeBuilder<VoteStatusDbDTO> builder)
        {
            builder.ToTable("VoteStatus");

            builder.HasKey(x => x.VSId);

            builder.Property(x => x.VSId)
                .HasColumnType("int")
                .HasColumnName("Id");

            builder.Property(x => x.Name)
                .HasColumnType("nchar(20)")
                .HasColumnName("Name");
        }
    }
}
