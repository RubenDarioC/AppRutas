using RutaSeguimientoApp.MVVM.ViewModels;

namespace RutaSeguimientoApp.MVVM.Views;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		BindingContext = new AppShelViewModel();
	}
}