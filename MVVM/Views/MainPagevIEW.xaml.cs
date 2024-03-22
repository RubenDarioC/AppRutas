using RutaSeguimientoApp.Common.Extensions;
using RutaSeguimientoApp.Models.ModelsPreference;
using RutaSeguimientoApp.MVVM.Views;

namespace RutaSeguimientoApp
{
	public partial class MainPageView : ContentPage
	{
		int count = 0;

		public MainPageView()
		{
			InitializeComponent();
		}

		protected override async void OnAppearing()
		{
			UserPreference user = new UserPreference().GetPreferencesByModel();
			if (user.Remember || !string.IsNullOrEmpty(user.Token))
			{
				var consultarest = true;

				if (consultarest)
				{

				}
			}
			else if (Shell.Current.CurrentState.Location.ToString() == $"//{nameof(MainPageView)}/{nameof(LoginView)}")
			{
				Preferences.Clear();
			}
			else
			{
				Preferences.Clear();
				try
				{
					await Shell.Current.GoToAsync($"{nameof(LoginView)}");
				}
				catch (Exception ex)
				{
					throw new Exception("mensa", ex);
				}
			}
			
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
