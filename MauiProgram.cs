using Microsoft.Extensions.Logging;
using RutaSeguimientoApp.MVVM.ViewModels;
using RutaSeguimientoApp.MVVM.Views;

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
				});

#if DEBUG
			builder.Logging.AddDebug();
#endif
			builder.Services.AddTransient<MapRouteUserViewModel>();
			builder.Services.AddTransient<MapForRouteUserView>();

			return builder.Build();
		}
	}
}
