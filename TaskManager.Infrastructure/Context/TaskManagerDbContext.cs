﻿using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Infrastructure.Context
{
    public class TaskManagerDbContext : DbContext
    {
        public TaskManagerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
    }
}