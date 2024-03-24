using FluentValidation;
using RutaSeguimientoApp.Models.ModelsPreference;

namespace RutaSeguimientoApp.Common.Validations
{
	public class UserPreferencesValidator : AbstractValidator<UserPreference>
	{
		public UserPreferencesValidator()
		{
			RuleFor(u => u.Name).Must(name => !string.IsNullOrEmpty(name));
			RuleFor(u => u.Password).Must(name => !string.IsNullOrEmpty(name));
		}
	}
}
