namespace RutaSeguimientoApp.Common.Helpers
{
	public static class ReadWriteStoragePermission
	{
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
						bool answer = await Shell.Current.DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");
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
						bool answer = await Shell.Current.DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");
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
