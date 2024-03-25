namespace RutaSeguimientoApp.Models.ModelsEnum
{
	public enum EnumExceptions
	{
		#region CodigosRestReservados

		[EnumDescription("Error sin contenido")]
		ErrorNoContent = 204,

		[EnumDescription("Error en solicitud")]
		ErrorBadRequest = 400,

		[EnumDescription("Error en la autorizacion")]
		ErrorUnauthorized = 401,

		#endregion

		#region Login 1 al 50
		[EnumDescription("Error al validar las credenciales", "Error Autenticacion")]
		LoginError = 1,

		[EnumDescription("No se encontro el usuario", "Error Autenticacion")]
		UserNotFound = 2,

		[EnumDescription("Usuario o Contraseña Incorrectos", "Error Autenticacion")]
		UserCredentialsError = 3,

		[EnumDescription("Error al redireccionar a la pagina principal")]
		UserRedirectionMainPageError = 4,
		#endregion

		#region Peticiones Rest 51 al 100

		[EnumDescription("Error al construir la peticion rest contacte al administrador")]
		ErrorBuildRequest = 51,

		[EnumDescription("Error al insertar los header y cookies en la peticion")]
		ErrorInsertGlobalHeadersCookies = 52,

		[EnumDescription("Error al enviar la peticion")]
		ErrorSendRequest = 53,

		[EnumDescription("Respuesta del servidor no pudo ser leida")]
		ErrorStatusReadResponse = 54,

		[EnumDescription("Error al construir la url para peticion")]
		ErrorBuildUrl = 56,

		[EnumDescription("Error al construir la url para peticion")]
		ErrorUrlNoPermited = 57,
		#endregion

		#region Errores internos globales del sistema 1001 al 1100

		[EnumDescription("Error interna en la aplicacion si el error persiste consulte a soporte", "Error en la aplicacion ")]
		ExceptionNotControlled = 1001,

		[EnumDescription("Error al tratar de crear la instancia en fluent validation")]
		ExceptionCreateInstanceFluentValidation = 1002,

		[EnumDescription("Error al tratar de validar el modelo con fluent validation")]
		ExceptionFluentValidationNotControlled = 1003,

		#endregion
	}
}
