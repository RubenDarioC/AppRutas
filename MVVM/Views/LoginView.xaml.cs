using RutaSeguimientoApp.MVVM.ViewModels;

namespace RutaSeguimientoApp.MVVM.Views;

public partial class LoginView : ContentPage
{
	public LoginView()
	{
		InitializeComponent();
		BindingContext = new LoginViewModel();
	}

	/// <summary>
	/// Evita el volver atars en la pagina del login
	/// </summary>
	/// <returns></returns>
	protected override bool OnBackButtonPressed()
	{
		return true;
	}
}