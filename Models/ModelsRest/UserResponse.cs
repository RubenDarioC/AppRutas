namespace RutaSeguimientoApp.Models.ModelsRest
{
	/// <summary>
	/// clase
	/// </summary>
	public class UserResponse : BaseResponseRest
	{
		public string? UserId { get; set; }
		public string? Name { get; set; }
		public string? FullName { get; set; }
		public string Email { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public string? Telephone { get; set; }
		public string Token { get; set; } = string.Empty;
	}

}
