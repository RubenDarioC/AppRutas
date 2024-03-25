using RutaSeguimientoApp.Common.Extensions;
using RutaSeguimientoApp.Models.ModelsEnum;
using RutaSeguimientoApp.MVVM.Models;
using RutaSeguimientoApp.Services.Interfaces;

namespace RutaSeguimientoApp.MVVM.ViewModels
{
	public class ViewModelBase
	{
		public Command OpenFlyoutMenuCommand { get; }
		private BaseResponseRequest Response { get; set; }
		public ViewModelBase()
		{
			Response = new BaseResponseRequest();
			OpenFlyoutMenuCommand = new Command(OpenFlyout);
		}

		public void OpenFlyout()
		{
			Shell.Current.FlyoutIsPresented = true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="requestHandler"></param>
		/// <param name="idTransaction"></param>
		/// <returns></returns>
		public BaseResponseRequest CentralizadorDePeticiones<TResult>(Func<TResult> requestHandler)
		{
			try
			{
				Response.TraceId = Guid.NewGuid();
				Response.Date = DateTime.Now;

				TResult? Results = requestHandler.Invoke();
				if (Results != null)
				{

					Response.Data = Results;
					Response.MessageError = null;

				}
				return Response;
			}
			catch (Exception ex)
			{
				(string title, string message, int codeError, Exception? e) = ex switch
				{
					IException serviceException => (serviceException.Title, serviceException.ErrorDetails, serviceException.CodeError, serviceException?.ExceptionOriginal),
					_ => (EnumExceptions.ExceptionNotControlled.ToString(), EnumExceptions.ExceptionNotControlled.GetEnumDescription(), 1, null)
				};

				Response.Success = false;
				Response.MessageError = new()
				{
					Title = string.IsNullOrEmpty(title) ? "Error Aplicacion" : title,
					DetailsError = message,
					CodeError = codeError
				};

				return Response;

			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="requestHandler"></param>
		/// <param name="idTransaction"></param>
		/// <returns></returns>
		public async Task<BaseResponseRequest> CentralizadorDePeticiones<TResult>(Func<Task<TResult>> requestHandler)
		{
			try
			{
				Response.TraceId = Guid.NewGuid();
				Response.Date = DateTime.Now;

				TResult? Results = await requestHandler();
				if (Results != null)
				{
					Response.Data = Results;
					Response.MessageError = null;
					Response.Success = true;
				}
				return Response;
			}
			catch (Exception ex)
			{
				(string title, string message, int codeError, Exception? e) = ex switch
				{
					IException serviceException => (serviceException.Title, serviceException.ErrorDetails, serviceException.CodeError, serviceException?.ExceptionOriginal),
					_ => (EnumExceptions.ExceptionNotControlled.ToString(), EnumExceptions.ExceptionNotControlled.GetEnumDescription(), 1, null)
				};

				if (string.IsNullOrEmpty(title))
				{
					(title, message) = EnumExceptions.ExceptionNotControlled.GetEnumDescriptionAndTittle();
					message = $"{message} code - {codeError}";
				}
				Response.Success = false;
				Response.MessageError = new()
				{
					Title = title,
					DetailsError = message,
					CodeError = codeError
				};

				return Response;
			}
		}
	}
}
