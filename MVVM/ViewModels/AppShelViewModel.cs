using RutaSeguimientoApp.MVVM.Views;

namespace RutaSeguimientoApp.MVVM.ViewModels
{
	public class AppShelViewModel
	{
		public Command SingOutCommand => new(SingOutUser);

		private async void SingOutUser()
		{
			Preferences.Clear();

			await Shell.Current.GoToAsync($"{nameof(LoginView)}");
		}
	}
}
