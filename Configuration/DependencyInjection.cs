using RutaSeguimientoApp.MVVM.ViewModels;
using RutaSeguimientoApp.MVVM.Views;
using RutaSeguimientoApp.Services.Interfaces;
using RutaSeguimientoApp.Services.RestServices;

namespace RutaSeguimientoApp.Configuration
{
	/// <summary>
	/// Clase donde se implementaran las injecciones de dependencias
	/// </summary>
	public static class DependencyInjection
	{
		public static IServiceCollection AddDependencyServices(this IServiceCollection services)
		{
			services.AddTransient<ILoginRestService, LoginRestService>();
			return services;
		}


		/// <summary>
		/// metodo el cual agrega las dependecias del proyecto
		/// </summary>
		/// <param name="services"></param>
		public static IServiceCollection AddDependencyViewsModels(this IServiceCollection services)
		{
			services.AddTransient<MapRouteUserViewModel>();
			services.AddTransient<LoginViewModel>();
			services.AddTransient<AppShelViewModel>();

			return services;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddDependencyViews(this IServiceCollection services)
		{
			services.AddTransient<UserAdminView>();
			services.AddTransient<MapForRouteUserView>();
			services.AddTransient<MainPageView>();
			services.AddTransient<LoginView>();

			return services;
		}


	}
}
