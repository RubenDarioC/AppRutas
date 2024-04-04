using RutaSeguimientoApp.Models.ModelsEnum;
using System.Reflection;

namespace RutaSeguimientoApp.Common.Extensions
{
	/// <summary>
	/// Extencion de la clase Enum
	/// </summary>
	public static class EnumExtension
	{

		/// <summary>
		/// Metodo encargado de traer la descrupcion si la propiedad del enumerador contiene el atributo <see cref="EnumDescriptionAttribute"/> si esta no contiene el atributo devolvera vacio
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="e"></param>
		/// <returns></returns>
		public static string GetEnumDescription<T>(this T e) where T : Enum
		{
			try
			{
				string description = e.GetType().GetField(e.ToString())?.GetCustomAttribute<EnumDescriptionAttribute>(true)?.Description ?? string.Empty;
				return description;
			}
			catch
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// obtiene la descripcion y el titulo de un enumerador que contenga el atributo <see cref="EnumDescriptionAttribute"/>
		/// <see langword="item1"/> es titulo y <see langword="item2"/> es la descripcion, en caso de no ser encontrados esto retornara dos string vacios
		/// </summary>
		/// <typeparam name="T">tipo del enumerador</typeparam>
		/// <param name="e">enumerador</param>
		/// <returns></returns>
		public static (string,string) GetEnumDescriptionAndTittle<T>(this T e) where T : Enum
		{
			try
			{
				EnumDescriptionAttribute description = e.GetType().GetField(e.ToString())?.GetCustomAttribute<EnumDescriptionAttribute>(true) ?? new EnumDescriptionAttribute(string.Empty, string.Empty);
				return (description.Title, description.Description);
			}
			catch
			{
				return (string.Empty, string.Empty);
			}
		}
	}
}
