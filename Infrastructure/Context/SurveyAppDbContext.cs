using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class SurveyAppDbContext : DbContext
    {
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new NpgsqlDataSourceBuilder("Server=localhost;Port=5432;Database=survey;Userid=postgres;Password=123456;Include Error Detail=True;");
            builder.EnableDynamicJson();
            var dataSource = builder.Build();
            optionsBuilder.UseNpgsql(dataSource);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SurveyConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
