namespace RutaSeguimientoApp.MVVM.Models
{
	/// <summary>
	/// LoginModel
	/// </summary>
	public class LoginModel
	{
		public string Email { get; set; } = null!;
		public string Password { get; set; } = null!;
		public bool Remember { get; set; }
	}
}
