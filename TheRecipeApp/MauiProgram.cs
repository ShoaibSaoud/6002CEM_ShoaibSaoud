using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using TheRecipeApp.Services;
using System.IO;
namespace TheRecipeApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
                fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                fonts.AddFont("Poppins-Bold.ttf", "PoppinsBold");
                fonts.AddFont("Poppins-Light.ttf", "PoppinsLight");
            });

        // SQLite Database path
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "recipeapp.db");

        // Register DatabaseService for Dependency Injection
        builder.Services.AddSingleton(new DatabaseService(dbPath));

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
