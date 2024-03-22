using RutaSeguimientoApp.Services.Interfaces;
using System.Net;

namespace RutaSeguimientoApp.Common.Exceptions
{
	public class BussinnesException: Exception, IException 
	{
		public BussinnesException(Enum enumBussinessException, string messageDetails ) 
		{
			Title = enumBussinessException.ToString();
			ErrorDetails = messageDetails;
			CodeError = Convert.ToInt32(enumBussinessException);
		}

		public HttpStatusCode StatusCode { get; }
		public string Title { get; }
		public string ErrorDetails { get; }
		public int CodeError { get; }

	}
}
