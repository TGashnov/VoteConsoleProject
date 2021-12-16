using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace VoteDbFirst.DAL
{
    public partial class VoteDbContext : DbContext
    {
        public VoteDbContext()
        {
        }

        public VoteDbContext(DbContextOptions<VoteDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Vote> Votes { get; set; }
        public virtual DbSet<VoteStatus> VoteStatuses { get; set; }
        public virtual DbSet<VoteTag> VoteTags { get; set; }
        public virtual DbSet<VwSearchVoteByAnswer> VwSearchVoteByAnswers { get; set; }
        public virtual DbSet<VwSearchVoteByTag> VwSearchVoteByTags { get; set; }
        public virtual DbSet<VwVote> VwVotes { get; set; }
        public virtual DbSet<VwVoteAndTag> VwVoteAndTags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("Answer");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Vote)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.VoteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Answer__VoteId__2F10007B");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("Tag");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Vote>(entity =>
            {
                entity.ToTable("Vote");

                entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Note).HasMaxLength(1000);

                entity.Property(e => e.Question)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Votes)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Vote__Status__300424B4");
            });

            modelBuilder.Entity<VoteStatus>(entity =>
            {
                entity.ToTable("VoteStatus");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<VoteTag>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Vote_Tag");

                entity.HasOne(d => d.TagNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Tag)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Vote_Tag__Tag__31EC6D26");

                entity.HasOne(d => d.VoteNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Vote)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Vote_Tag__Vote__30F848ED");
            });

            modelBuilder.Entity<VwSearchVoteByAnswer>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwSearchVoteByAnswer");

                entity.Property(e => e.Answers).HasMaxLength(4000);

                entity.Property(e => e.Question)
                    .IsRequired()
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<VwSearchVoteByTag>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwSearchVoteByTag");

                entity.Property(e => e.Question)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Tag)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<VwVote>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwVote");

                entity.Property(e => e.Answers).HasMaxLength(4000);

                entity.Property(e => e.Note).HasMaxLength(1000);

                entity.Property(e => e.Question)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsFixedLength(true);

                entity.Property(e => e.Tags).HasMaxLength(4000);
            });

            modelBuilder.Entity<VwVoteAndTag>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwVoteAndTag");

                entity.Property(e => e.Question)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Tags).HasMaxLength(4000);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
