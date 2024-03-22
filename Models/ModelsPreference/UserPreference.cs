using RutaSeguimientoApp.Models.ModelsRest;

namespace RutaSeguimientoApp.Models.ModelsPreference
{
	public class UserPreference
	{
		public string? UserId { get; set; }
		public string? Name { get; set; }
		public string? FullName { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }
		public string? Telephone { get; set; }
		public string? Token { get; set; }
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
