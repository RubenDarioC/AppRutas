namespace RutaSeguimientoApp.Models.ModelsEnum
{
	[AttributeUsage(AttributeTargets.All)]
	public class EnumDescriptionAttribute : Attribute
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public EnumDescriptionAttribute(string description, string? title = null)
		{
			Description = description;
			Title = title;
		}
	}
}
