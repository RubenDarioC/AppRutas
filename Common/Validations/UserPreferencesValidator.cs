using FluentValidation;
using RutaSeguimientoApp.Models.ModelsPreference;

namespace RutaSeguimientoApp.Common.Validations
{
	/// <summary>
	/// clase que valida 
	/// </summary>
	public class UserPreferencesValidator : AbstractValidator<UserPreference>
	{
		public UserPreferencesValidator()
		{
			RuleFor(u => u.Name).Must(name => !string.IsNullOrEmpty(name));
			RuleFor(u => u.Password).Must(name => !string.IsNullOrEmpty(name));
		}
	}
}
