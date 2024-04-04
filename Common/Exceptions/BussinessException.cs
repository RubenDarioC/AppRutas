using RutaSeguimientoApp.Common.Extensions;
using RutaSeguimientoApp.Services.Interfaces;

namespace RutaSeguimientoApp.Common.Exceptions
{
	/// <summary>
	/// Clase encargada del manejo de excepciones de negocio
	/// </summary>
	public class BussinnesException : Exception, IException
	{
		public BussinnesException(Enum enumBussinessException, Exception? exception = null)
		{
			(string, string) titileAndDescription = enumBussinessException.GetEnumDescriptionAndTittle();
			Title = titileAndDescription.Item1;
			ErrorDetails = titileAndDescription.Item2;
			CodeError = Convert.ToInt32(enumBussinessException);
			ExceptionOriginal = exception;
		}

		/// <summary>
		/// Titulo del error, si son excepciones internas no se le debe asignar titulo
		/// </summary>
		public string Title { get; }

		/// <summary>
		/// Detalles del error se debe tartar de especificar donde ocurrio la excepcion o el por que 
		/// </summary>
		public string ErrorDetails { get; }
		
		/// <summary>
		/// Codigo de error unico para identificar el error
		/// </summary>
		public int CodeError { get; }
		
		/// <summary>
		/// Excepcion interna no esperada 
		/// </summary>
		public Exception? ExceptionOriginal { get; }

	}
}
