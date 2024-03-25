namespace RutaSeguimientoApp.MVVM.Templates;

public partial class MenuTemplate : ContentView
{
	public static readonly BindableProperty OpenFlyoutMenuCommandProperty =
			BindableProperty.Create(nameof(OpenFlyoutMenuCommand), typeof(Command), typeof(MenuTemplate), 
				defaultValueCreator: bindable => new Command( ()=> Shell.Current.FlyoutIsPresented = true));

	public Command OpenFlyoutMenuCommand
	{
		get => (Command)GetValue(MenuTemplate.OpenFlyoutMenuCommandProperty);
		set => SetValue(MenuTemplate.OpenFlyoutMenuCommandProperty, value);
	}

	public MenuTemplate()
	{
		InitializeComponent();	
	}
}