namespace RutaSeguimientoApp.MVVM.ViewModels
{
	using Microsoft.Maui.Maps;
	using RutaSeguimientoApp.Common.Helpers;
	using RutaSeguimientoApp.Models;
	using Map = Microsoft.Maui.Controls.Maps.Map;
	public class MapRouteUserViewModel : ViewModelBase
	{
		private CancellationTokenSource _cancelTokenSource { get; set; } = new CancellationTokenSource(TimeSpan.FromSeconds(3));
		private bool _isCheckingLocation;

		public MapRouteUserModel MapRouteUserModel { get; set; }
		Map IMap { get; set; }

		public MapRouteUserViewModel(Map map)
		{
			MapRouteUserModel = new();
			IMap = map;
			CargarInicializacion().GetAwaiter().GetResult();
		}

		public async Task CargarInicializacion()
		{
			await CargarRutaLocal();
		}


		public async Task CargarRutaLocal()
		{
			try
			{
				Location? location = null;
				if (await AppRequestPermission.ValiatePermissionsApp())
				{
					location = await Geolocation.Default.GetLastKnownLocationAsync();
				}

				if (location == null && !_isCheckingLocation)
				{
					_isCheckingLocation = true;

					try
					{
						GeolocationRequest request = new(GeolocationAccuracy.Best, TimeSpan.FromSeconds(5));
						location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

						if (location != null)
							IMap.MoveToRegion(MapSpan.FromCenterAndRadius(location, Distance.FromKilometers(1)));
						else
						{
							location = new Location(4.6506737541889835, -74.10439188662122);
							IMap.MoveToRegion(MapSpan.FromCenterAndRadius(location, Distance.FromKilometers(10)));
						}
					}
					catch (FeatureNotEnabledException)
					{
						bool resultado = await DisplayAlertCentralizado("GPS no esta habilitado", " Porfavor encienda el GPS.)", true);
						if (resultado)
							AppInfo.Current.ShowSettingsUI();
						await Redireccionar($"//{nameof(MainPageView)}");
					}
					finally
					{
						_isCheckingLocation = false;
					}
				}
			}
			catch (Exception ex)
			{
				await Redireccionar($"//{nameof(MainPageView)}");
				await Shell.Current.DisplayAlert("Acceso a localizacion", "El acceso a la localizacion fue denegado, ingrese a las cnfiguraciones del telefno y asigne permisos para cargar las rutas", "ok");
			}
		}
	}
}
