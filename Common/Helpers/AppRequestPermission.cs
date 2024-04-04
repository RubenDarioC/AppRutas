namespace RutaSeguimientoApp.Common.Helpers
{
	/// <summary>
	/// 
	/// </summary>
	public static class AppRequestPermission
	{
		/// <summary>
		/// Valida si los permisos necesarios para que funcione la aplicacion sean concedidos en caso de que no, solicitarlos, al solicitarlos y ser negados se envia a la configuracion de la aplicacion para que los conceda,
		/// en caso de que el usuario no los de por ningun medio retorna un <see langword="false"/>
		/// </summary>
		/// <returns></returns>
		public static async Task<bool> ValiatePermissionsApp()
		{
			try
			{
				if(DeviceInfo.Platform == DevicePlatform.WinUI)
					return true;

				PermissionStatus respondeLocationWhenInUse = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
				PermissionStatus respondeLocationAlways = await Permissions.CheckStatusAsync<Permissions.LocationAlways>();

				PermissionStatus respondePermissions = respondeLocationWhenInUse == PermissionStatus.Granted || respondeLocationAlways == PermissionStatus.Granted ? PermissionStatus.Granted : PermissionStatus.Denied;
				if (respondePermissions == PermissionStatus.Granted)
					return true;

				if (Permissions.ShouldShowRationale<Permissions.LocationWhenInUse>())
				{
					if (Permissions.ShouldShowRationale<Permissions.LocationAlways>())
						respondePermissions = await Permissions.RequestAsync<Permissions.LocationAlways>();

					if (respondePermissions != PermissionStatus.Granted)
					{
						bool answer = await Shell.Current.DisplayAlert("Desea ir a la configuracion del dispositivo?", "estos permisos son necesarios para que funcione la aplicacion", "Yes", "No");
						if (answer)
							AppInfo.Current.ShowSettingsUI();
						return false;
					}
					return true;
				}
				else
				{
					respondePermissions = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
					if (respondePermissions != PermissionStatus.Granted)
					{
						bool answer = await Shell.Current.DisplayAlert("Desea ir a la configuracion del dispositivo?", "estos permisos son necesarios para que funcione la aplicacion", "Yes", "No");
						if (answer)
							AppInfo.Current.ShowSettingsUI();
						return false;
					}
					return true;
				}
			}
			catch (Exception ex)
			{
				
				return false;
			}
		}
	}
}
