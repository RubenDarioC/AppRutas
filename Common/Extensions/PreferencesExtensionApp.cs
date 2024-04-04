using System.ComponentModel;
using System.Reflection;

namespace RutaSeguimientoApp.Common.Extensions
{
	public static class PreferencesExtensionApp
	{
		/// <summary>
		/// Inserta las preferecias basado en un clase y sus propiedades validar que los modelos de datos no se repitan para evitar sobre escritura de los valores
		/// puede consultar en namespace <see langword="RutaSeguimientoApp.Models.ModelsPreference"/>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="model"></param>
		/// <returns></returns>
		public static bool InsertPreferencesByModel<T>(T model)
		{
			if (model == null)
				return false;

			foreach (PropertyInfo property in typeof(T).GetProperties().Where(property => property.GetValue(model) != default))
			{
				object value = property.GetValue(model)!;
				switch (value)
				{
					case DateTime when (DateTime)value == DateTime.MinValue:
					case int when (int)value == 0:
					case string when (string)value == string.Empty:
						break;

					default:
						Preferences.Set(property.Name, value: value.ToString());
						break;
				}
			}
			return true;
		}


		/// <summary>
		/// Obtiene los valores alamacenados en <see cref="Preferences"/> basado en el modelo de datos enviado
		/// puede consultar en namespace <see langword="RutaSeguimientoApp.Models.ModelsPreference"/> para saber que datos se almacenan
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="model"></param>
		/// <returns></returns>
		public static T GetPreferencesByModel<T>(this T model)
		{
			foreach (PropertyInfo property in typeof(T).GetProperties().Where(property => Preferences.ContainsKey(property.Name)))
			{
				string valueFromPreferences = Preferences.Get(property.Name, "");

				TypeConverter converter = TypeDescriptor.GetConverter(property.PropertyType);
				object? convertedValue = converter.ConvertFrom(valueFromPreferences);
				if (convertedValue != null)
					property.SetValue(model, convertedValue);
			}

			return model;
		}
	}
}
