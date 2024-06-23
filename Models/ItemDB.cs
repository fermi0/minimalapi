using Microsoft.EntityFrameworkCore;

namespace api.Models;
public class ItemDB(DbContextOptions<ItemDB> options) : DbContext(options)
{
    public DbSet<Item> Item => Set<Item>();
}
