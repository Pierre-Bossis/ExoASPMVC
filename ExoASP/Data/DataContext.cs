﻿using ExoASP.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExoASP.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> users { get; set; }
    }
}