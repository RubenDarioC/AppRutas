
namespace RutaSeguimientoApp.Models.ModelsConfig
{
	public class BaseRestConfigApp
	{
		public string UrlBase { get; set; } = string.Empty;
		public string ContentType { get; set; } = string.Empty;
		public Dictionary<string, string> HeadersGloblas { get; set; } = [];
		public virtual Dictionary<Enum, ServiciosHttpConfig> Servicios { get; set; } = [];

	}
}
