using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RutaSeguimientoApp.Models
{
	public partial class MapRouteUserModel : ObservableObject
	{
		[ObservableProperty]
		private bool isFrameVisible;

		[RelayCommand]
		public void ToggleFrameVisibility()
		{
			IsFrameVisible = !IsFrameVisible;
		}
	}
}
