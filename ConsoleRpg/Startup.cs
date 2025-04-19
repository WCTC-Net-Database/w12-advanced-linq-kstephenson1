using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Helpers;
using ConsoleRpgEntities.Models.UI;
using ConsoleRpgEntities.Models.UI.Character;
using ConsoleRpgEntities.Models.UI.Menus;
using ConsoleRpgEntities.Models.UI.Menus.InteractiveMenus;
using ConsoleRpgEntities.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NReco.Logging.File;

namespace ConsoleRpgEntities.Configuration;

public static class Startup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        // Get the configuration from appsettings.json
        IConfiguration configuration = ConfigurationHelper.GetConfiguration();

        // Configure FileLoggerOptions from configuration
        FileLoggerOptions fileLoggerOptions = new FileLoggerOptions();
        configuration.GetSection("Logging:File").Bind(fileLoggerOptions);

        // Configure logging
        services.AddLogging(loggingBuilder =>
        {
            // Clear existing providers to avoid duplicate logs
            loggingBuilder.ClearProviders();
            loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));

            // Add Console logger
            loggingBuilder.AddConsole();

            // Add File logger with options from configuration
            string? logFileName = "Logs/log.txt"; // Specify the log file path
            loggingBuilder.AddProvider(new FileLoggerProvider(logFileName, fileLoggerOptions));
        });

        // Register services for dependency injection
        services.AddTransient<CharacterUtilities>();
        services.AddTransient<CharacterUI>();
        services.AddTransient<CombatHandler>();
        services.AddTransient<CommandHandler>();
        services.AddTransient<CommandMenu>();
        services.AddTransient<DungeonFactory>();
        services.AddTransient<ExitMenu>();
        services.AddDbContext<GameContext>(options => options
        .UseSqlServer(configuration.GetConnectionString("DbConnection"))
        .UseLazyLoadingProxies());
        services.AddTransient<InventoryMenu>();
        services.AddTransient<ItemCommandMenu>();
        services.AddTransient<LevelUpMenu>();
        services.AddTransient<MainMenu>();
        services.AddTransient<RoomFactory>();
        services.AddTransient<RoomMenu>();
        services.AddTransient<RoomNavigationMenu>();
        services.AddTransient<RoomUI>();
        services.AddTransient<SeedHandler>();
        services.AddSingleton<UnitClassMenu>();
        services.AddTransient<UnitSelectionMenu>();
        services.AddTransient<UserInterface>();
    }
}