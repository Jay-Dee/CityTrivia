﻿using CityTrivia.WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityTrivia.WebApi.DbContext {
    public interface ICitiesTriviaDbContext {
        DbSet<City> Cities { get; set; }
    }

    public class CitiesTriviaDbContext : Microsoft.EntityFrameworkCore.DbContext, ICitiesTriviaDbContext {

        public CitiesTriviaDbContext(DbContextOptions<CitiesTriviaDbContext> options) : base(options) { }

        public DbSet<City> Cities { get; set; } = null!;
    }
}
