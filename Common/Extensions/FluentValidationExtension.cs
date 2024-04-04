using FluentValidation;
using FluentValidation.Results;
using RutaSeguimientoApp.Common.Exceptions;
using RutaSeguimientoApp.Models.ModelsEnum;
using RutaSeguimientoApp.MVVM.Models;

namespace RutaSeguimientoApp.Common.Extensions
{
	public static class FluentValidationExtension
	{
		/// <summary>
		/// Clase encargada de validar los errores de un modelo con datos, retornando un mensaje con los campos que no pasen las validaciones, se puede validar que algunas de las propiedades no pasaron validando
		/// en la propiedad <see cref="BaseResponseRequest.Success"/> que sera marcada como false
		/// y los detalles de las propiedades en <see cref="BaseResponseRequest.MessageError"/>
		/// </summary>
		/// <typeparam name="T">Es el modelo de datos a validar</typeparam>
		/// <typeparam name="V">Es la clase que contiene las reglas de validacion</typeparam>
		/// <param name="element"></param>
		/// <returns><see cref="BaseResponseRequest"/></returns>
		public static async Task<BaseResponseRequest> ValidateAndThrowsAsync<T, V>(this T element, params object[] args) where V : IValidator<T>, new() where T : class, new()
		{
			try
			{
				V validator = (V?)Activator.CreateInstance(typeof(V), args) ?? throw new BussinnesException(EnumExceptions.ExceptionCreateInstanceFluentValidation);
				ValidationResult result = await validator.ValidateAsync(element);
				if (!result.IsValid)
				{
					IEnumerable<string> listerror = result.Errors.Select(err => err.ErrorMessage).Distinct();
					(string tittle, string description) = EnumExceptions.ErrorValidatePropertiesValidateFluent.GetEnumDescriptionAndTittle();
					return new()
					{
						Success = false,
						MessageError = new()
						{
							CodeError = (int)EnumExceptions.ErrorValidatePropertiesValidateFluent,
							Title = tittle,
							DetailsError = description + string.Join("\n", listerror)
						}
					};
				}
				return new() { Success = true };
			}
			catch (MemberAccessException)
			{
				V validator = (V?)Activator.CreateInstance(typeof(V)) ?? throw new BussinnesException(EnumExceptions.ExceptionCreateInstanceFluentValidation);
				ValidationResult result = await validator.ValidateAsync(element);
				if (!result.IsValid)
				{
					IEnumerable<string> listerror = result.Errors.Select(err => err.ErrorMessage).Distinct();
					(string tittle, string description) = EnumExceptions.ErrorValidatePropertiesValidateFluent.GetEnumDescriptionAndTittle();
					return new()
					{
						Success = false,
						MessageError = new()
						{
							CodeError = (int)EnumExceptions.ErrorValidatePropertiesValidateFluent,
							Title = tittle,
							DetailsError = description + string.Join("\n", listerror)
						}
					};
				}
				return new() { Success = true };
			}
			catch (Exception ex)
			{
				(string title, string details) = EnumExceptions.ExceptionNotControlled.GetEnumDescriptionAndTittle();
				return new()
				{
					Success = false,
					MessageError = new()
					{
						Title = title,
						DetailsError = details,
						CodeError = (int)EnumExceptions.ExceptionFluentValidationNotControlled
					}
				};
			}
		}
	}
}
