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
		/// <summary>
		/// Agrega la dependencia de servicios 
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddDependencyServices(this IServiceCollection services)
		{
			services.AddTransient<ILoginRestService, LoginRestService>();
			return services;
		}

		/// <summary>
		/// Agrega la dependencia de viewmodels
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
		/// Agrega la dependencia de views 
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
