namespace RutaSeguimientoApp.Models.ModelsEnum
{
	/// <summary>
	/// Clase atributo, que permite decorar los enumeradores para poder asignarles un valor o una descripcion
	/// </summary>
	[AttributeUsage(AttributeTargets.All)]
	public class EnumDescriptionAttribute : Attribute
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public EnumDescriptionAttribute(string description, string? title = null)
		{
			Description = description;
			Title = title ?? string.Empty;
		}
	}
}
