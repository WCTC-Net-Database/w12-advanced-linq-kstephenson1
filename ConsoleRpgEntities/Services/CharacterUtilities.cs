namespace ConsoleRpgEntities.Services;

using ConsoleRpgEntities.Configuration;
using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Models.Combat;
using ConsoleRpgEntities.Models.Rooms;
using ConsoleRpgEntities.Models.UI.Character;
using ConsoleRpgEntities.Models.UI.Menus.InteractiveMenus;
using ConsoleRpgEntities.Models.Units.Abstracts;
using ConsoleRpgEntities.Services.DataHelpers;
using ConsoleRpgEntities.Models.Interfaces;
using ConsoleRpgEntities.Models.Interfaces.Rooms;
using Spectre.Console;

public class CharacterUtilities
{
    private CharacterUI _characterUI;
    private GameContext _db;
    private LevelUpMenu _levelUpMenu;
    private RoomMenu _roomMenu;
    private UnitClassMenu _unitClassMenu;
    private UnitSelectionMenu _unitSelectionMenu;
    // CharacterFunctions class contains fuctions that manipulate characters based on user input.

    public CharacterUtilities(CharacterUI characterUI, GameContext db, UnitClassMenu unitClassMenu, LevelUpMenu levelUpMenu, RoomMenu roomMenu, UnitSelectionMenu unitSelectionMenu)
    {
        _characterUI = characterUI;
        _db = db;
        _levelUpMenu = levelUpMenu;
        _roomMenu = roomMenu;
        _unitClassMenu = unitClassMenu;
        _unitSelectionMenu = unitSelectionMenu;
    }
    public void NewCharacter() // Creates a new character.  Asks for name, class, level, hitpoints, and items.
    {
        string name = Input.GetString("Enter your character's name: ");
        Type characterClass = _unitClassMenu.Display($"Please select a class for {name}", "[[Cancel Character Creation]]");
        if (characterClass == null) return;
        int level = Input.GetInt("Enter your character's level: ", 1, Config.CHARACTER_LEVEL_MAX, $"character level must be 1-{Config.CHARACTER_LEVEL_MAX}");
        int hitPoints = Input.GetInt("Enter your character's maximum hit points: ", 1, "must be greater than 0");

        Console.Clear();
        Console.WriteLine($"\nWelcome, {name} the {characterClass.Name}! You are level {level} and have {hitPoints} health.\n");

        //_unitManager.Characters.AddUnit(new(name, characterClass, level, hitPoints, inventory));
        dynamic character = Activator.CreateInstance(characterClass);
        character.Name = name;
        character.Class = characterClass.Name;
        character.Level = level;

        List<UnitItem> unitItems = new();


        character.UnitItems = unitItems;

        Stat stat = new Stat();
        stat.HitPoints = hitPoints;
        stat.MaxHitPoints = hitPoints;
        stat.Movement = 5;
        stat.Constitution = 5;
        stat.Strength = 8;
        stat.Magic = 8;
        stat.Dexterity = 8;
        stat.Speed = 8;
        stat.Luck = 8;
        stat.Defense = 8;
        stat.Resistance = 8;

        character.Stat = stat;

        IRoom room = _roomMenu.Display($"Select room for {character.Name}","[[No Room]]");
        if (room != null)
        {
            character.CurrentRoom = (Room)room;
        }
        _db.Stats.Add(character.Stat);
        _db.Units.Add(character);
        _db.SaveChanges();
    }

    public T CastObject<T>(object input)
    {
        return (T) input;
    }

    public void FindCharacterByName() // Asks the user for a name and displays a character based on input.
    {
        string characterName = Input.GetString("What is the name of the character you would like to search for? ");
        Unit character = FindCharacterByName(characterName)!;

        Console.Clear();

        if (character != null)
        {
            _characterUI.DisplayCharacterInfo(character);
        }
        else
        {
            AnsiConsole.MarkupLine($"[Red]No characters found with the name {characterName}\n[/]");
        }
    }

    public void FindCharacterByList() // Asks the user for a name and displays a character based on input.
    {
        IUnit unit = _unitSelectionMenu.Display("Select unit to view.", "[[Cancel Character Search]]");

        Console.Clear();

        if (unit != null)
        {
            _characterUI.DisplayCharacterInfo(unit);
        }
        else
        {
            AnsiConsole.MarkupLine($"[White]Character search cancelled.\n[/]");
        }
    }

    private Unit? FindCharacterByName(string name) // Finds and returns a character based on input.
    {
        Unit unit = _db.Units.Where(c => c.Name.ToUpper() == name.ToUpper()).FirstOrDefault();
        return unit;
    }

    public void LevelUp() //Asks the user for a character to level up, then displays that character.
    {
        string characterName = Input.GetString("What is the name of the character that you would like to level up? ");
        Unit unit = FindCharacterByName(characterName)!;
        Console.Clear();

        if (unit != null)
        {
            int levelModifier = _levelUpMenu.Display($"Choose how to change the level for {unit.Name}", "Go Back");
            _db.Units.Update(unit);
            switch (levelModifier)
            {
                case -1:
                    if (unit.Level > 1)
                    {
                        unit.Level += levelModifier;
                        AnsiConsole.MarkupLine($"[Yellow]Yikes! {unit.Name} has been demoted to level {unit.Level}[/]\n");
                    }
                    else
                    {
                        AnsiConsole.MarkupLine($"[Red]{unit.Name} is level one and cannot go down another level![/]\n");
                    }
                    break;
                case 1:
                    if (unit.Level < Config.CHARACTER_LEVEL_MAX)
                    {
                        unit.Level += levelModifier;
                        AnsiConsole.MarkupLine($"[Green]Congratulations! {unit.Name} has reached level {unit.Level}[/]\n");
                    }
                    else
                    {
                        AnsiConsole.MarkupLine($"[Red]{unit.Name} is already max level! ({Config.CHARACTER_LEVEL_MAX})[/]\n");
                    }
                    break;
                default:
                    AnsiConsole.MarkupLine($"[White]{unit.Name} remains the same level[/]\n");
                    break;
            }
            _characterUI.DisplayCharacterInfo(unit);
            _db.SaveChanges();
        }
        else
        {
            AnsiConsole.MarkupLine($"[Red]No characters found with the name {characterName}[/]\n");
        }
    }

    public void LevelUpByList() //Asks the user for a character to level up, then displays that character.
    {
        IUnit unit = _unitSelectionMenu.Display("Select unit to view.", "[[Cancel Level Up/Down]]");
        Console.Clear();

        if (unit != null)
        {
            int levelModifier = _levelUpMenu.Display($"Choose how to change the level for {unit.Name}", "Go Back");
            _db.Units.Update(unit as Unit);
            switch (levelModifier)
            {
                case -1:
                    if (unit.Level > 1)
                    {
                        unit.Level += levelModifier;
                        AnsiConsole.MarkupLine($"[Yellow]Yikes! {unit.Name} has been demoted to level {unit.Level}[/]\n");
                    }
                    else
                    {
                        AnsiConsole.MarkupLine($"[Red]{unit.Name} is level one and cannot go down another level![/]\n");
                    }
                    break;
                case 1:
                    if (unit.Level < Config.CHARACTER_LEVEL_MAX)
                    {
                        unit.Level += levelModifier;
                        AnsiConsole.MarkupLine($"[Green]Congratulations! {unit.Name} has reached level {unit.Level}[/]\n");
                    }
                    else
                    {
                        AnsiConsole.MarkupLine($"[Red]{unit.Name} is already max level! ({Config.CHARACTER_LEVEL_MAX})[/]\n");
                    }
                    break;
                default:
                    AnsiConsole.MarkupLine($"[White]{unit.Name} remains the same level[/]\n");
                    break;
            }
            _characterUI.DisplayCharacterInfo(unit);
            _db.SaveChanges();
        }
        else
        {
            AnsiConsole.MarkupLine($"[White]Level up cancelled.[/]\n");
        }
    }

    public void DisplayCharacters()                       //Displays each c's information.
    {
        Console.Clear();
        List<Unit> units = _db.Units.Where(u => !u.UnitType.Contains("Enemy")).ToList();

        _characterUI.DisplayCharacterInfo(units);
    }
}
