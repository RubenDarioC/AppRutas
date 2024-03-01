using RutaSeguimientoApp.MVVM.Models;
using RutaSeguimientoApp.MVVM.Views;
using System.Windows.Input;

namespace RutaSeguimientoApp.MVVM.ViewModels
{
	public class LoginViewModel
	{
		public LoginModel Login { get; set; }

		public ICommand LoginCommand => new Command(LoginUser);

		public LoginViewModel()
		{
			Login = new LoginModel();
		}

		public void LoginUser()
		{
			if (Login.Email == "1" && Login.Password == "1")
			{
				App.Current!.MainPage = new AppShell();
			}
			else if (string.IsNullOrEmpty(Login.Email)) 
			{
				App.Current!.MainPage = new AppShell();
			}
			else
			{
				App.Current!.MainPage!.DisplayAlert("tittle", "message contraseña errronea", "Aceptar");
			}
		}
	}
}
