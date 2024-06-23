using Microsoft.EntityFrameworkCore;

namespace api.Models;

// This is where the temporary database is connected using default options.
public class ItemDB(DbContextOptions<ItemDB> options) : DbContext(options) // Inherits from Dbcontext
{
    public DbSet<Item> Item => Set<Item>(); // Sets a database using DbSet where Item is a blueprint.
    // Item is where the every data in database is stored, they are taken from Item.cs
}
