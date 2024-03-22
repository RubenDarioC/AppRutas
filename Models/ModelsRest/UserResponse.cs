namespace RutaSeguimientoApp.Models.ModelsRest
{
	public class UserResponse : BaseResponseRest
	{
		public string? UserId { get; set; }
		public string? Name { get; set; }
		public string? FullName { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }
		public string? Telephone { get; set; }
		public string? Token { get; set; }
	}

}
