namespace RutaSeguimientoApp.Services.Interfaces
{
	public interface IReadWritePermission
	{
		Task<PermissionStatus> CheckStatusAsync();
		Task<PermissionStatus> RequestAsync();
	}
}
