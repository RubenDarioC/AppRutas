using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using RutaSeguimientoApp.Common.Extensions;
using RutaSeguimientoApp.Models.ModelsEnum;
using RutaSeguimientoApp.MVVM.Models;
using RutaSeguimientoApp.Services.Interfaces;
using System.Diagnostics;


namespace RutaSeguimientoApp.MVVM.ViewModels
{
	public class ViewModelBase
	{
		public bool IsEnabled { get; set; }
		private bool isEnabled { get; set; }
		public Command OpenFlyoutMenuCommand { get; }
		private BaseResponseRequest Response { get; set; }
		public ViewModelBase()
		{
			Response = new BaseResponseRequest();
			OpenFlyoutMenuCommand = new Command(OpenFlyout);
		}

		/// <summary>
		/// Encargado de desplegar el menu lateral 
		/// </summary>
		public async void OpenFlyout()
		{
			IsEnabled = true;
			await ControlarPeticiones(() =>
			{
				return Task.Run(() =>
				{
					Shell.Current.FlyoutIsPresented = true;
				});
			});
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="requestHandler"></param>
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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="mensaje"></param>
		/// <returns></returns>
		public async Task MostarToastMensaje(string mensaje)
		{
			IToast toast = Toast.Make(mensaje);
			await toast.Show(new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token);
		}

		public async Task DisplayAlertCentralizado(string? titulo, string? mensaje)
		{
			try
			{
				await Application.Current!.MainPage!.DisplayAlert(titulo, mensaje, "Aceptar");
			}
			catch (Exception ex) 
			{
				Debug.WriteLine(ex.Message);
			}
		}
		public async Task<bool> DisplayAlertCentralizado(string? titulo, string? mensaje, bool aceptCancel)
		{
			try
			{
				if (aceptCancel)
				{
					bool result = await Application.Current!.MainPage!.DisplayAlert(titulo, mensaje, "Si", "No");
					return result;
				}
				return false;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return false;
			}
		}

		public async Task Redireccionar(string ruta) 
		{
			await Shell.Current.GoToAsync(ruta);
		}

		private async Task ControlarPeticiones(Func<Task> func) 
		{
			isEnabled = IsEnabled && !isEnabled;
			if(isEnabled)
			{
				await func.Invoke();
				IsEnabled = false;
				isEnabled = false;
			}
		}

	}
}
