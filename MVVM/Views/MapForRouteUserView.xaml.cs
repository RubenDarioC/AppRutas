namespace RutaSeguimientoApp.MVVM.Views
{
	using RutaSeguimientoApp.MVVM.ViewModels;
	using System.Threading.Tasks;

	public partial class MapForRouteUserView : ContentPage
	{

		public MapForRouteUserView()
		{
			InitializeComponent();
			BindingContext = new MapRouteUserViewModel(map);
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			if (!await CheckPermissions())
			{
				await Application.Current!.MainPage!.DisplayAlert("tittle", "message contraseña errronea", "Aceptar");
				await Task.Delay(2000);
				Application.Current.Quit();
			}
		}


		private async Task<bool> CheckPermissions()
		{

			//PermissionStatus cameraStatus = await CheckPermissions<Permissions.Camera>();
			//PermissionStatus mediaStatus = await CheckPermissions<Permissions.Media>();
			//PermissionStatus storageWriteStatus = await CheckPermissions<Permissions.StorageWrite>();
			PermissionStatus locationAlwaysStatus = await CheckPermissions<Permissions.LocationWhenInUse>();


			return IsGranted(locationAlwaysStatus);
		}


		private async Task<PermissionStatus> CheckPermissions<TPermission>() where TPermission : Permissions.BasePermission, new()
		{
			PermissionStatus status = await Permissions.CheckStatusAsync<TPermission>();

			if (status != PermissionStatus.Granted)
			{
				status = await Permissions.RequestAsync<TPermission>();
			}

			return status;
		}

		private static bool IsGranted(PermissionStatus status)
		{
			return status == PermissionStatus.Granted || status == PermissionStatus.Limited;
		}

		//protected override async void OnAppearing()
		//{
		//	base.OnAppearing();
		//	try
		//	{

		//		GeolocationRequest solicitudGeolocalizacion = new(GeolocationAccuracy.High, TimeSpan.FromSeconds(30));
		//		Location? localizacion = await Geolocation.GetLocationAsync(solicitudGeolocalizacion);
		//		if (localizacion != null)
		//			map.MoveToRegion(MapSpan.FromCenterAndRadius(localizacion, Distance.FromKilometers(1)));

		//		Permissions.

		//	}
		//	catch (Exception ex)
		//	{
		//		Location localizacion = new Location(4.6506737541889835, -74.10439188662122);
		//		map.MoveToRegion(MapSpan.FromCenterAndRadius(localizacion, Distance.FromKilometers(10)));
		//		await Shell.Current.DisplayAlert("Acceso a localizacion", "El acceso a la localizacion fue denegado, ingrese a las cnfiguraciones del telefno y asigne permisos para cargar las rutas", "ok");
		//	}

		//}
	}
}