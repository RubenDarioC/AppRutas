using RutaSeguimientoApp.MVVM.Views;

namespace RutaSeguimientoApp
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			Routing.RegisterRoute(nameof(MainPageView), typeof(MainPageView));
			Routing.RegisterRoute(nameof(MapForRouteUserView), typeof(MapForRouteUserView));

			MainPage = new LoginView();
		}
	}
}
