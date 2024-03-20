using Microsoft.EntityFrameworkCore;
using System;
using UploadCSV.Models;

namespace UploadCSV.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<Persona> Personas { get; set; }
        public DbSet<Direccion> Direccion { get; set; }
    }

    
}
