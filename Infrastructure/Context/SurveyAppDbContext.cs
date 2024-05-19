using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class SurveyAppDbContext : DbContext, ISurveyDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public SurveyAppDbContext(IConfiguration configuration)
        {
            
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new NpgsqlDataSourceBuilder(_connectionString);
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
