using Microsoft.EntityFrameworkCore;

// Inherits from DbContext, the base class in EF Core for managing database operations.
public class AppDbContext : DbContext {

    // DbSet<T> is a generic class that takes an entity type (T) and represents a database table for that type.
    // DbSet<Employee> means EF Core manages an "Employees" table, allowing CRUD operations on Employee entities.
    public DbSet<Employee> Employees {get; set;}

    // Constructor for AppDbContext that takes configuration options.
    // DbContextOptions<T> is a generic class that takes a DbContext type (T) and provides configuration options for it.
    // Here, DbContextOptions<AppDbContext> ensures EF Core configures AppDbContext with the correct database settings.
    public AppDbContext(DbContextOptions<AppDbContext> options) 
    // Calls the base DbContext constructor and passes the options (from Program.cs) generally.
    : base(options)
    {}  // Constructor body is empty because all configuration is handled by options


}