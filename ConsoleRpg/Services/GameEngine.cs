using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Models.Abilities;
using ConsoleRpgEntities.Models.UI;
using ConsoleRpgEntities.Models.Units.Abstracts;
using ConsoleRpgEntities.Services;

namespace ConsoleRpg.Services;

public class GameEngine
{
    private GameContext _db;
    private SeedHandler _seedHandler;
    private UserInterface _userInterface;
    private CombatHandler _combatHandler;

    public GameEngine(GameContext db, SeedHandler seedHandler, UserInterface userInterface, CombatHandler combatHandler)
    {
        _db = db;
        _seedHandler = seedHandler;
        _userInterface = userInterface;
        _combatHandler = combatHandler;
    }

    public void StartGameEngine()
    {
        Initialization();
        Run();
        //Test();
        End();
    }

    void Test()
    {
        // This method is only used for testing purposes and should be removed when the "game" is finished.
        Unit rogue = _db.Units.Where(u => u.Class == "Rogue").FirstOrDefault();
        Unit target = _db.Units.FirstOrDefault();
        Ability steal = _db.Abilities.Where(a => a.Units.Contains(rogue)).FirstOrDefault();
        rogue.UseAbility(target, steal);
        
        rogue.Attack(target);
    }

    public void Initialization()
    {
        // Seeds the database with initial data. This is only run once when the program is started for the first time.
        _seedHandler.SeedDatabase();
    }

    public void Run()
    {
        // Runs the main game loop. This is where the game starts and runs until the user chooses to exit.

        // Shows the main menu and waits for the user to choose an option.
        _userInterface.MainMenu.Display("[[Start Game]]");

        // Starts the combat handler, which is the main game loop.
        _combatHandler.StartCombat();
    }

    public void End()
    {
        // Ends the game and exits the program.
        _userInterface.ExitMenu.Show();
    }
}
