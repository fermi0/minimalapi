// Importing EF library from Microsoft and Models from api directory from the root of the project.
using Microsoft.EntityFrameworkCore;
using api.Models;

/* Createbuilder creates an instance of webapplication from .NET core to create a new a application
   with default configurations */
var builder = WebApplication.CreateBuilder(args);

// This adds DBContext service from webapplication class provided by .NET This project uses temporary database.
builder.Services.AddDbContext<ItemDB>(opt => // opt is shot for options that are default to in-memory database.
    opt.UseInMemoryDatabase("temp")); // Keep the datas in "temp"

// This method shows/handles errors. This shouldnt be in production ready API.
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build(); // Build the app

/* Grouping the url for code tidiness. every path starts from /item and added as /item/<other paths>
   such as /item/completed. Manipulating the datas need somewhere to be stored,
   anything after a comma is a variable to be stored and manipulated later.
   {id} is a placeholder where it is replaced with intergers during the HTTP methods*/
var items = app.MapGroup("/items");
items.MapGet("/", getItems);
items.MapGet("/completed", getComplete);
items.MapGet("/incomplete", getIncomplete);
items.MapGet("/{id}", getItem);
items.MapPost("/", addItem);
items.MapPut("/{id}", editItem);
items.MapDelete("/{id}", delItem);

// print a greeting page. you dont need this in production-ready API, because api doesnt handle client-side interface.
app.MapGet("/", () => "Use postman to send a HTTP Post, " +
"Put and Delete request, All HTTP Get requests can be accessed by browser");

app.Run(); // Runs the app

/* This sends a GET request to get data using ItemDB.cs where it is mentioned 
   that a blueprint for creating data is located at Item.cs.
   Task is a thread that takes type as IResult method provided by .NET
   getItems is how the program know what to do when the path in the link is given.
   It takes parameters; how the data is connected and where is is stored */
static async Task<IResult> getItems(ItemDB db)
{
    return TypedResults.Ok(await db.Item.ToArrayAsync()); // outputs in a list 
}


// Checks the IsComplete boolean in Item.cs is true, if it's true then show it in the list
static async Task<IResult> getComplete(ItemDB db)
{
    return TypedResults.Ok(await db.Item.Where(t =>
        t.IsComplete).ToArrayAsync());
}

// Same as above but checks if it's false
static async Task<IResult> getIncomplete(ItemDB db)
{
    return TypedResults.Ok(await db.Item.Where(t =>
        !t.IsComplete).ToArrayAsync());
}

// Find an id; an number from an item in a list of data and outputs it
static async Task<IResult> getItem(int id, ItemDB db)
{
    return await db.Item.FindAsync(id)
        is Item item
        ? TypedResults.Ok(item)
        : TypedResults.NotFound();
}

// Sends a POST request to add data using Item as a blueprint and store it in item
static async Task<IResult> addItem(Item item, ItemDB db)
{
    db.Item.Add(item); // Add the stored data in item
    await db.SaveChangesAsync();
    return TypedResults.Created($"{item.Id}", item); // Throws a 200 Ok and creates an id for item and store it in item
}

// Sends a PUT request to update data and store it in editItem
static async Task<IResult> editItem(int id, Item editItem, ItemDB db)
{
    var item = await db.Item.FindAsync(id); // Finds id of the data
    if (item == null) return TypedResults.NotFound();
    item.Title = editItem.Title; // The updated title is stored in editItem.Title and passed in Item.cs
    item.IsComplete = editItem.IsComplete;
    await db.SaveChangesAsync();
    return TypedResults.Ok(await db.Item.ToArrayAsync()); // After update, changes are shown
}

// Deletes a selected id(number)
static async Task<IResult> delItem(int id, ItemDB db)
{
    if (await db.Item.FindAsync(id) is Item item) // Checks the id exists in the data
    {
        db.Item.Remove(item);
        await db.SaveChangesAsync();
        return TypedResults.NoContent(); // After removing data, so no content
    }
    return TypedResults.NotFound(); // if the id dont exist show 404 Not Found error
}
