using RutaSeguimientoApp.Models.ModelsRest;

namespace RutaSeguimientoApp.Services.Interfaces
{
	public interface ILoginRestService
	{
		Task<UserResponse> LoginUser(string user, string password);
	}
}
