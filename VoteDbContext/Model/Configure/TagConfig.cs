using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VoteDbContext.Model.DTO;

namespace VoteDbContext.Model.Configure
{
    public class TagConfig : IEntityTypeConfiguration<TagDbDTO>
    {
        public void Configure(EntityTypeBuilder<TagDbDTO> builder)
        {
            builder.ToTable("Tag");

            builder.HasKey(x => x.TagId);

            builder.Property(x => x.TagId)
                .HasColumnType("bigint")
                .HasColumnName("Id");

            builder.Property(x => x.Text)
                .HasColumnType("nvarchar(100)")
                .HasColumnName("Text")
                .IsRequired();
        }
    }
}
