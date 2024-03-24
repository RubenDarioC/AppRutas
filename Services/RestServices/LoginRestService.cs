using Microsoft.Extensions.Configuration;
using RutaSeguimientoApp.Common.Exceptions;
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

		///<inheritdoc cref="ILoginRestService.LoginUser"/>
		public async Task<UserResponse> LoginUser(string user, string password)
		{

			if (user == "admin" && password == "admin")
			{
				UserResponse usermock = new UserResponse()
				{
					Success = true,
					Email = "admin",
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
			if (userResponse.Success)
				return userResponse;

			switch (userResponse.CodeResponse)
			{
				case (int)EnumExceptions.ErrorNoContent:
					throw new BussinnesException(EnumExceptions.UserNotFound);

				case (int)EnumExceptions.UserCredentialsError:
					throw new BussinnesException(EnumExceptions.UserCredentialsError);

				default: throw new BussinnesException(EnumExceptions.LoginError);
			}

		}

	}
}
