namespace RutaSeguimientoApp.Models.ModelsConfig
{
	/// <summary>
	/// clase que representa la configuracion de <see cref="SecretsConfig"/> en el appsettings
	/// </summary>
	public class SecretsConfig
	{
		public const string Key = "Secrets";
		public string DataBaseRemote { get; set; } = string.Empty;
	}
}
