
namespace RutaSeguimientoApp.Models.ModelsConfig
{
	/// <summary>
	/// clase base que representa la configuracion de <see cref="BaseRestConfigApp"/> en el appsettings
	/// </summary>
	public class BaseRestConfigApp
	{
		public string UrlBase { get; set; } = string.Empty;
		public string ContentType { get; set; } = string.Empty;
		public Dictionary<string, string> HeadersGloblas { get; set; } = [];
		public virtual Dictionary<Enum, ServiciosHttpConfig> Servicios { get; set; } = [];

	}
}
