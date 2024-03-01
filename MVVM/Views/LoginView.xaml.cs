using RutaSeguimientoApp.MVVM.ViewModels;

namespace RutaSeguimientoApp.MVVM.Views;

public partial class LoginView : ContentPage
{
	public LoginView()
	{
		InitializeComponent();
		BindingContext = new LoginViewModel();
	}
}