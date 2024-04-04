using RutaSeguimientoApp.Models.ModelsRest;

namespace RutaSeguimientoApp.Models.ModelsPreference
{
	/// <summary>
	/// clase para insercion de la preferencias basicas del usuario
	/// </summary>
	public class UserPreference
	{
		public string? UserId { get; set; }
		public string? Name { get; set; }
		public string? FullName { get; set; }
		public string Email { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public string? Telephone { get; set; }
		public string Token { get; set; } = string.Empty;
		public bool Remember { get; set; }

		public static explicit operator UserPreference(UserResponse userResponse) 
		{
			return new() 
			{
				Email = userResponse.Email,
				FullName = userResponse.FullName,
				Password = userResponse.Password,
				Telephone = userResponse.Telephone,
				Token = userResponse.Token,
				Name = userResponse.Name,
				UserId = userResponse.UserId
			};		
		}
	}
}
