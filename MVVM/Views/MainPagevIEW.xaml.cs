using RutaSeguimientoApp.Common.Extensions;
using RutaSeguimientoApp.Models.ModelsPreference;
using RutaSeguimientoApp.Models.ModelsRest;
using RutaSeguimientoApp.MVVM.Views;
using RutaSeguimientoApp.Services.Interfaces;

namespace RutaSeguimientoApp
{
	public partial class MainPageView : ContentPage
	{
		readonly ILoginRestService _loginRestService = ActivatorUtilities.GetServiceOrCreateInstance<ILoginRestService>(App.ServiceProvider);

		int count = 0;

		public MainPageView()
		{
			InitializeComponent();
		}

		protected override async void OnAppearing()
		{
			UserPreference user = new UserPreference().GetPreferencesByModel();
			if (string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Email)) 
			{
				await Shell.Current.GoToAsync($"{nameof(LoginView)}");
			}
			if( !user.Remember && Shell.Current.CurrentState.Location.ToString() == $"//{nameof(MainPageView)}")
			{
				Preferences.Clear();
				await Shell.Current.GoToAsync($"{nameof(LoginView)}");
			}
			else if(Shell.Current.CurrentState.Location.ToString() != $"//{nameof(MainPageView)}/{nameof(LoginView)}" && ValidateExpirationToken(user.Token))
			{
				try
				{
					UserResponse userRember = await _loginRestService.LoginUser(user.Email, user.Password);
					Preferences.Set(nameof(userRember.Token), userRember.Token);
					if (!userRember.Success)
					{
						await Shell.Current.GoToAsync($"{nameof(LoginView)}");
					}					
				}
				catch (Exception ex)
				{
					await Shell.Current.GoToAsync($"{nameof(LoginView)}");
				}
			}
		}

		private bool ValidateExpirationToken(string token) 
		{
			return string.IsNullOrEmpty(token);
		}
		private void OnCounterClicked(object sender, EventArgs e)
		{
			count++;

			if (count == 1)
				CounterBtn.Text = $"Clicked {count} time";
			else
				CounterBtn.Text = $"Clicked {count} times";

			SemanticScreenReader.Announce(CounterBtn.Text);
		}
	}

}
