using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VoteDbContext.Model.DTO;

namespace VoteDbContext.Model.Configure
{
    public class AnswerConfig : IEntityTypeConfiguration<AnswerDbDTO>
    {
        public void Configure(EntityTypeBuilder<AnswerDbDTO> builder)
        {
            builder.ToTable("Answer");

            builder.HasKey(x => x.AnsId);

            builder.Property(x => x.AnsId)
                .HasColumnType("bigint")
                .HasColumnName("Id");

            builder.Property(x => x.Text)
                .HasColumnType("nvarchar(100)")
                .HasColumnName("Text")
                .IsRequired();

            builder.Property(x => x.NumberOfVoters)
                .HasColumnType("int")
                .HasColumnName("NumberOfVoters");

            builder.HasOne(ans => ans.Vote)
                .WithMany(v => v.Answers)
                .HasForeignKey(ans => ans.AnsId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
