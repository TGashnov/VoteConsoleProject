using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VoteDbContext.Model.DTO;

namespace VoteDbContext.Model.Configure
{
    public class VoteConfig : IEntityTypeConfiguration<VoteDbDTO>
    {
        public void Configure(EntityTypeBuilder<VoteDbDTO> builder)
        {
            builder.ToTable("Vote");

            builder.HasKey(x => x.VoteId);

            builder.Property(x => x.VoteId)
                .HasColumnType("bigint")
                .HasColumnName("Id");

            builder.Property(x => x.Question)
                .HasColumnType("nvarchar(1000)")
                .HasColumnName("Question")
                .IsRequired();

            builder.Property(x => x.Note)
                .HasColumnType("nvarchar(1000)")
                .HasColumnName("Note");

            builder.Property(x => x.NumberOfVoters)
                .HasColumnType("int")
                .HasColumnName("NumberOfVoters");

            builder.Property(x => x.Created)
                .HasColumnType("datetime")
                .HasColumnName("Created")
                .HasDefaultValue(DateTime.Now);

            builder.Property(x => x.Published)
                .HasColumnType("datetime")
                .HasColumnName("Published");

            builder.Property(x => x.VoteStatusId)
                .HasColumnType("int")
                .HasColumnName("VoteStatus");

            builder.HasOne(v => v.VoteStatus)
                .WithMany(vS => vS.Votes)
                .HasForeignKey(v => v.VoteStatusId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(v => v.Tags)
                .WithMany(t => t.Votes);

            builder.HasOne(v => v.User)
                .WithMany(u => u.Votes)
                .HasForeignKey(v => v.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
