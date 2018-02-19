using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Movie_Site.Models
{
    public partial class Movie_SiteContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Movie_Site;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cineplex>(entity =>
            {
                entity.Property(e => e.CineplexId).HasColumnName("CineplexID");

                entity.Property(e => e.Cdescription)
                    .IsRequired()
                    .HasColumnName("CDescription");

                entity.Property(e => e.Location).IsRequired();
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.Mdescription)
                    .IsRequired()
                    .HasColumnName("MDescription");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Title).IsRequired();
            });

            modelBuilder.Entity<SessionTimes>(entity =>
            {
                entity.HasKey(e => e.SessionId)
                    .HasName("PK__SessionT__C9F4927008750DD4");

                entity.Property(e => e.SessionId).HasColumnName("SessionID");

                entity.Property(e => e.CineplexId).HasColumnName("CineplexID");

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.MovieTime).HasColumnType("datetime");

                entity.HasOne(d => d.Cineplex)
                    .WithMany(p => p.SessionTimes)
                    .HasForeignKey(d => d.CineplexId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__SessionTi__Cinep__5812160E");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.SessionTimes)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__SessionTi__Movie__59063A47");
            });
        }

        public virtual DbSet<Cineplex> Cineplex { get; set; }
        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<SessionTimes> SessionTimes { get; set; }
    }
}