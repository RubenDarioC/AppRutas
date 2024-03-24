namespace RutaSeguimientoApp.Models.ModelsRest
{
	public class BaseResponseRest
	{
		public bool Success { get; set; }
		public int CodeResponse {  get; set; }
		public object? Error { get; set; }
	}
}
