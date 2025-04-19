using ConsoleRpgEntities.Models.Rooms;
using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Models.Interfaces.Rooms;

namespace ConsoleRpgEntities.Models.UI.Menus.InteractiveMenus;

public class RoomMenu : InteractiveSelectionMenu<IRoom>
{
    private GameContext _db;
    // The RoomMenu contains items that have 4 parts, the index, the name, the description, and a room.

    public RoomMenu(GameContext context)
    {
        _db = context;
    }

    public override void Display(string errorMessage)
    {
        throw new ArgumentException("CommandMenu(unit, prompt) requires a unit.");
    }

    public IRoom Display(string prompt, string exitMessage)
    {
        IRoom selection = default!;
        bool exit = false;
        while (exit != true)
        {
            Console.Clear();
            Console.WriteLine(prompt);
            Update(exitMessage);
            BuildTable(exitMessage);
            Show();
            ConsoleKey key = ReturnValidKey();
            selection = DoKeyActionReturnUnit(key, out exit);
        }
        return selection;
    }

    public override void Update(string exitMessage)
    {
        _menuItems = new();
        List<Room> rooms = _db.Rooms.ToList();

        foreach (Room room in rooms)
        {
            AddMenuItem($"{room.Name}", $"{room.Description}", room);
        }

        AddMenuItem(exitMessage, "", null!);
    }
}

