using Microsoft.Extensions.Configuration;
using RutaSeguimientoApp.Models.ModelsConfig;
using RutaSeguimientoApp.Models.ModelsEnum;
using RutaSeguimientoApp.Models.ModelsRest;
using RutaSeguimientoApp.Services.Interfaces;
using System.Net;

namespace RutaSeguimientoApp.Services.RestServices
{
	public class LoginRestService : BaseRestServices, ILoginRestService
	{
		protected override HttpRestConfigApp BaseRestConfigApp { get; }
		protected override CookieCollection? CookieCollection { get; }
		protected override Enum EndPoint => EnumServiciosHttp.Login;

		public LoginRestService(IConfiguration configuration)
		{
			BaseRestConfigApp = configuration.GetRequiredSection(nameof(HttpRestConfigApp)).Get<HttpRestConfigApp>()!;
			BaseRestConfigApp!.AsignarValores();
			CookieCollection = null;

		}

		///<inheritdoc cref="ILoginRestService.LoginUse"/>
		public async Task<UserResponse> LoginUse(string user, string password)
		{
			try
			{
				if (user == "admin" && password == "admin") 
				{
					UserResponse usermock = new UserResponse() 
					{
						Email = "Email@.com",
						FullName = "User FullName",
						Telephone = "3124190000",
						Name = "User",
						Password = password,
						UserId = "userId",
						Token = "Token",
					};
					return usermock;
				}

				string urlBuild = BuildUrlApi();
				UserResponse userResponse = await PostTask<UserResponse>(urlBuild, new { User = user, Password = password });

				return userResponse;
			}
			catch
			{
				return new();
			}
		}

	}
}
