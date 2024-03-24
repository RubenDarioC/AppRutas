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
		/// Validates and throws a specific business exception with the related validation errors.
		/// </summary>
		/// <typeparam name="T">Es el modelo de datos a validar</typeparam>
		/// <typeparam name="V">Es la clase que contiene las reglas de validacion</typeparam>
		/// <param name="element"></param>
		/// <returns></returns>
		public static async Task<BaseResponseRequest> ValidateAndThrowsAsync<T, V>(this T element, params object[] args) where V : IValidator<T>, new() where T : class, new()
		{
			try
			{
				V validator = (V?)Activator.CreateInstance(typeof(V), args) ?? throw new BussinnesException(EnumExceptions.ExceptionCreateInstanceFluentValidation);
				ValidationResult result = await validator.ValidateAsync(element);
				if (!result.IsValid)
				{
					IEnumerable<string> listerror = result.Errors.Select(err => err.ErrorMessage).Distinct();
					return new()
					{
						Success = false,
						Data = listerror
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
					return new()
					{
						Success = false,
						Data = listerror
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
