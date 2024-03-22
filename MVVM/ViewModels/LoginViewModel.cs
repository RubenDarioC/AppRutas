using RutaSeguimientoApp.Common.Exceptions;
using RutaSeguimientoApp.Common.Extensions;
using RutaSeguimientoApp.Models.ModelsPreference;
using RutaSeguimientoApp.Models.ModelsRest;
using RutaSeguimientoApp.MVVM.Models;
using RutaSeguimientoApp.Services.Interfaces;

namespace RutaSeguimientoApp.MVVM.ViewModels
{
	public class LoginViewModel
	{
		readonly ILoginRestService _loginRestService = ActivatorUtilities.GetServiceOrCreateInstance<ILoginRestService>(App.ServiceProvider);
		public LoginModel Login { get; set; }
		public Command LoginCommand { get; }

		public LoginViewModel()
		{
			Login = new LoginModel();
			LoginCommand = new Command(LoginUser);
		}

		public async void LoginUser()
		{

			UserResponse result = await _loginRestService.LoginUse(Login.Email, Login.Password);

			if (result is { Error: null, Token: not null, UserId: not null, Name: not null })
			{
				try
				{
					PreferencesExtensionApp.InsertPreferencesByModel((UserPreference)result);
					await Shell.Current.GoToAsync($"//{nameof(MainPageView)}");
				}
				catch (BussinnesException ex)
				{
					await Application.Current!.MainPage!.DisplayAlert(ex.Title, ex.ErrorDetails, "Aceptar");
				}
				catch (Exception ex)
				{
					await Application.Current!.MainPage!.DisplayAlert("Error", ex.Message, "Aceptar");
				}
			}
			else if (result.Error != null) 
			{
				await Application.Current!.MainPage!.DisplayAlert("Error", result.Error.ToString(), "Aceptar");
			}
			else
			{
				await Application.Current!.MainPage!.DisplayAlert("tittle", "contraseña o usuario erroneos", "Aceptar");
			}
		}
	}
}
