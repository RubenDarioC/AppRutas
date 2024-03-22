using RutaSeguimientoApp.Models.ModelsRest;

namespace RutaSeguimientoApp.Services.Interfaces
{
	public interface ILoginRestService
	{
		Task<UserResponse> LoginUse(string user, string password);
	}
}
