using Microsoft.Extensions.Logging;
using RutaSeguimientoApp.Configuration;

namespace RutaSeguimientoApp
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.UseMauiMaps()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
					fonts.AddFont("MaterialIconsOutlined-Regular.otf", "MaterialOutlined");
				})
				.AddCustomConfiguration();

			builder.Services
				.AddDependencyServices()
				.AddDependencyViews()
				.AddDependencyViewsModels();

			AppConfigurations.AddRoutesPages();
#if DEBUG
			builder.Logging.AddDebug();
#endif
			return builder.Build();
		}
	}
}
