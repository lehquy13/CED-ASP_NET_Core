﻿using CED.Domain.Entities.Subject;
using CED.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CED.Infrastructure.Persistence;

public class CEDDBContext : DbContext
{
    public DbSet<Subject> _subjects { get; set; } = null!;
    public DbSet<User> _users { get; set; } = null!;
    public CEDDBContext(DbContextOptions<CEDDBContext> options) : base(options)
    {

    }
}

//using to support addmigration
public class CEDDBContextFactory : IDesignTimeDbContextFactory<CEDDBContext>
{
    public CEDDBContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CEDDBContext>();
        optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=CED_DDD;Trusted_Connection=True;TrustServerCertificate=True");

        return new CEDDBContext(optionsBuilder.Options);
    }
}