﻿using AdvancedAJAX.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AdvancedAJAX.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
    }
}
