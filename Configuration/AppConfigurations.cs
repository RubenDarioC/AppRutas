using Microsoft.Extensions.Configuration;
using RutaSeguimientoApp.MVVM.Views;
using System.Reflection;

namespace RutaSeguimientoApp.Configuration
{
	public static class AppConfigurations
	{
		/// <summary>
		/// Agrega los archivos de configuracion del ambiente
		/// </summary>
		/// <param name="builder"></param>
		/// <returns></returns>
		public static MauiAppBuilder AddCustomConfiguration(this MauiAppBuilder builder)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			using Stream stream = executingAssembly.GetManifestResourceStream($"{AppInfo.Current.Name}.appsettings.json")!;
			IConfigurationRoot configuration = new ConfigurationBuilder()
				.AddJsonStream(stream!)
				.Build();

			builder.Configuration.AddConfiguration(configuration);
			return builder;
		}


		/// <summary>
		/// El registro de estas rutas se debe dar con vistas o paginas que no esten dentro de la jerquia de la clase <see cref="Shell"/>
		/// como lo son los ShellContent o FlyoutItem
		/// </summary>
		public static void AddRoutesPages()
		{
			Routing.RegisterRoute("LoginView", typeof(LoginView));
		}
	}
}
