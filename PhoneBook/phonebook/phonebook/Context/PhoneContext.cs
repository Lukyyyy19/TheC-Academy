using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using phonebook.Models;

namespace phonebook.Context;

public class PhoneContext:DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public PhoneContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=PhoneBook;Trusted_Connection=True;TrustServerCertificate=True;");
    }
}