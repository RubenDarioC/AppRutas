namespace RutaSeguimientoApp.MVVM.Views
{
	using RutaSeguimientoApp.MVVM.ViewModels;

	public partial class MapForRouteUserView : ContentPage
	{

		public MapForRouteUserView()
		{
			InitializeComponent();
			BindingContext = new MapRouteUserViewModel(map);
		}

	}
}