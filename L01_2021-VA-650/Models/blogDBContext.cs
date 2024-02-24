﻿using Microsoft.EntityFrameworkCore;

namespace L01_2021_VA_650.Models
{
    public class blogDBContext : DbContext 
    {
        public blogDBContext(DbContextOptions<blogDBContext> options) : base(options) 
        {

        }
        public DbSet<usuarios> usuarios { get; set; }  
        public DbSet<calificaciones> calificaciones { get; set; }
        public DbSet<comentarios> comentarios { get; set; } 
    }

    
}
