using System;
using Auth_Cookie.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth_Cookie.Data
{
    public class AuthDbContext:DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options):base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Test> Tests { get; set; }
    }
}
