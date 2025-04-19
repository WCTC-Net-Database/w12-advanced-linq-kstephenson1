using Spectre.Console;
using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Models.Interfaces.Rooms;
using ConsoleRpgEntities.Models.Rooms;

namespace ConsoleRpgEntities.Models.UI.Character;

public class RoomUI
{
    // RoomUI helps display room information in a nice little table.

    private GameContext _db;
    public RoomUI(GameContext context)
    {
        _db = context;
    }

    public void DisplayRooms() // Displays the rooms and their info.
    {
        List<Room> rooms = _db.Rooms.ToList();

        // Creates a display table that contains all the other tables to create a nice little display.
        Table displayTable = new Table();
        displayTable
            .AddColumn(new TableColumn("Room Name"))
            .AddColumn(new TableColumn("Room Description"));

        foreach (IRoom room in rooms)
        {
            displayTable.AddRow(room.Name, room.Description);
        }

        // Displays the table to the user.
        AnsiConsole.Write(displayTable);
    }
}
