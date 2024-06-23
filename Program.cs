using Microsoft.EntityFrameworkCore;
using api.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ItemDB>(opt =>
    opt.UseInMemoryDatabase("All_Items"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

var items = app.MapGroup("/items");
items.MapGet("/", getItems);
items.MapGet("/completed", getComplete);
items.MapGet("/incomplete", getIncomplete);
items.MapGet("/{id}", getItem);
items.MapPost("/", addItem);
items.MapPut("/{id}", editItem);
items.MapDelete("/{id}", delItem);

app.Run();

static async Task<IResult> getItems(ItemDB db)
{
    return TypedResults.Ok(await db.Item.ToArrayAsync());
}


static async Task<IResult> getComplete(ItemDB db)
{
    return TypedResults.Ok(await db.Item.Where(t =>
        t.IsComplete).ToArrayAsync());
}

static async Task<IResult> getIncomplete(ItemDB db)
{
    return TypedResults.Ok(await db.Item.Where(t =>
        !t.IsComplete).ToArrayAsync());
}

static async Task<IResult> getItem(int id, ItemDB db)
{
    return await db.Item.FindAsync(id)
        is Item item
        ? TypedResults.Ok(item)
        : TypedResults.NotFound();
}

static async Task<IResult> addItem(Item item, ItemDB db)
{
    db.Item.Add(item);
    await db.SaveChangesAsync();
    return TypedResults.Created($"{item.Id}", item);
}

static async Task<IResult> editItem(int id, Item editItem, ItemDB db)
{
    var item = await db.Item.FindAsync(id);
    if (item == null) return TypedResults.NotFound();
    item.Title = editItem.Title;
    item.IsComplete = editItem.IsComplete;
    await db.SaveChangesAsync();
    return TypedResults.NoContent();
}

static async Task<IResult> delItem(int id, ItemDB db)
{
    if (await db.Item.FindAsync(id) is Item item)
    {
        db.Item.Remove(item);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }
    return TypedResults.NotFound();
}
