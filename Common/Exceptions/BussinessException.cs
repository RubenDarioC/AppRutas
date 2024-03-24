using RutaSeguimientoApp.Common.Extensions;
using RutaSeguimientoApp.Services.Interfaces;

namespace RutaSeguimientoApp.Common.Exceptions
{
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

		public string Title { get; }
		public string ErrorDetails { get; }
		public int CodeError { get; }
		public Exception? ExceptionOriginal { get; }

	}
}
