using RutaSeguimientoApp.Models.ModelsEnum;

namespace RutaSeguimientoApp.Models.ModelsConfig
{
	/// <summary>
	/// clase que representa la configuracion de <see cref="HttpRestConfigApp"/> en el appsettings
	/// </summary>
	public class HttpRestConfigApp : BaseRestConfigApp
	{
		public new Dictionary<EnumServiciosHttp, ServiciosHttpConfig> Servicios { get; set; }

		public HttpRestConfigApp(Dictionary<EnumServiciosHttp, ServiciosHttpConfig> servicios)
		{
			Servicios = servicios;
			AsignarValores();
		}
		public void AsignarValores()
		{
			foreach (var kvp in Servicios)
			{
				base.Servicios[(Enum)(kvp.Key)] = kvp.Value;
			}
		}
	}
}
