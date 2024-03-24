
namespace RutaSeguimientoApp.Services.Interfaces
{
	public interface IException
	{
		string Title { get; }
		string ErrorDetails { get; }
		int CodeError { get; }
		Exception? ExceptionOriginal { get; }
	}
}
