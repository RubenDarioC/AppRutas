namespace RutaSeguimientoApp.MVVM.ViewModels
{
	using Microsoft.Maui.Maps;
	using RutaSeguimientoApp.Models;
	using System.Diagnostics;
	using Map = Microsoft.Maui.Controls.Maps.Map;
	public class MapRouteUserViewModel : ViewModelBase
	{
		private readonly CancellationTokenSource _cancelTokenSource;
		private bool _isCheckingLocation;

		public MapRouteUserModel MapRouteUserModel { get; set; }
		Map IMap { get; set; }

		public MapRouteUserViewModel(Map map)
		{
			_cancelTokenSource = new CancellationTokenSource();
			MapRouteUserModel = new();
			IMap = map;
			CargarRutaLocal().GetAwaiter().GetResult();
		}


		public async Task CargarRutaLocal()
		{
			try
			{
				Location? location = await Geolocation.Default.GetLastKnownLocationAsync();
				if (location == null)
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
						bool resultado = await DisplayAlertCentralizado("GPS no esta habilitado"," Porfavor encienda el GPS.)", true);
						if(resultado)
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
				await Shell.Current.DisplayAlert("Acceso a localizacion", "El acceso a la localizacion fue denegado, ingrese a las cnfiguraciones del telefno y asigne permisos para cargar las rutas", "ok");
			}
		}
	}
}
