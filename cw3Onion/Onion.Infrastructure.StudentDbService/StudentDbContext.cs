using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Onion.Domain.Entities;

namespace Onion.Infrastructure.StudentDbService
{
    public class StudentDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=db-mssql;Initial Catalog=s16776;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Student> Student { get; set; }
    }
}
