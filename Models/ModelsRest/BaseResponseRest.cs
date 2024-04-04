namespace RutaSeguimientoApp.Models.ModelsRest
{
	/// <summary>
	/// clase base para las respuestas de las peticiones rest
	/// </summary>
	public class BaseResponseRest
	{
		public bool Success { get; set; }
		public int CodeResponse {  get; set; }
		public object? Error { get; set; }
	}
}
