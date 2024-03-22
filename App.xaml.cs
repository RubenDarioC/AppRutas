using RutaSeguimientoApp.MVVM.Views;

namespace RutaSeguimientoApp
{
	public partial class App : Application
	{
		public static IServiceProvider ServiceProvider { get; private set; }
		public App(IServiceProvider serviceProvider)
		{
			InitializeComponent();
			ServiceProvider = serviceProvider;
			MainPage = new AppShell();

		}
	}
}
