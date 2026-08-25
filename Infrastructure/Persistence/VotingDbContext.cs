using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence
{
    public class VotingDbContext : DbContext
    {
        public VotingDbContext(DbContextOptions<VotingDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Election> Elections => Set<Election>();
        public DbSet<Candidate> Candidates => Set<Candidate>();
        public DbSet<Vote> Votes => Set<Vote>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Configure relationships and constraints if needed

            modelBuilder.Entity<Vote>()
                .HasIndex(v => new
                {
                    v.UserId,
                    v.ElectionId
                })
                .IsUnique();

            modelBuilder.Entity<Candidate>()
                .HasOne(c =>c.Election)
                .WithMany(e => e.Candidates)
                .HasForeignKey(c => c.ElectionId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
