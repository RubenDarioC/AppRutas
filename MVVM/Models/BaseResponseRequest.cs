namespace RutaSeguimientoApp.MVVM.Models
{
	public class BaseResponseRequest
	{
		public bool Success { get; set; }
		public Guid TraceId { get; set; }
		public object? Data { get; set; }
		public DateTime Date { get; set; }
		public MessageErrorResponse? MessageError { get; set; }
	}
}
