using RutaSeguimientoApp.Common.Exceptions;
using RutaSeguimientoApp.Common.Extensions;
using RutaSeguimientoApp.Common.Helpers;
using RutaSeguimientoApp.Models.ModelsEnum;
using RutaSeguimientoApp.Models.ModelsPreference;
using RutaSeguimientoApp.Models.ModelsRest;
using RutaSeguimientoApp.MVVM.Models;
using RutaSeguimientoApp.Services.Interfaces;

namespace RutaSeguimientoApp.MVVM.ViewModels
{
	public class LoginViewModel : ViewModelBase
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
			BaseResponseRequest result = await CentralizadorDePeticiones(async () => await _loginRestService.LoginUser(Login.Email, Login.Password));

			if (result.Success)
			{
				try
				{
					UserPreference userPreference = (UserPreference)(UserResponse)result.Data!;
					userPreference.Remember = Login.Remember;
					PreferencesExtensionApp.InsertPreferencesByModel(model: userPreference);
					if (await AppRequestPermission.ValiatePermissionsApp())
					{
						await MostarToastMensaje("Acceso exitoso");
						await Redireccionar($"//{nameof(MainPageView)}");
					}
				}
				catch (BussinnesException ex )
				{
					IException exception = ex;
					await DisplayAlertCentralizado(exception.Title, exception.ErrorDetails);
				}
				catch (Exception ex)
				{
					(string titulo, string descripcion) = EnumExceptions.UserRedirectionMainPageError.GetEnumDescriptionAndTittle();
					await DisplayAlertCentralizado(titulo, descripcion);
				}
			}
			else
			{
				await DisplayAlertCentralizado(result.MessageError?.Title, result.MessageError?.DetailsError);
			}
		}


	}
}
