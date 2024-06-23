// This is a name of the module we create to later import in other files
namespace api.Models;

//This class is accecible from anywhere, and this is the blueprint for how data is created and stored
public class Item
{
    public int Id { get; set; } // Id is a unique number assigned to a data
    public string? Title { get; set; } // This is to edit or set to create or update data
    public bool IsComplete { get; set; } // This is a boolean which only returns true of false
}