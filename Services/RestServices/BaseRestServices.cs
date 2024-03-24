using RutaSeguimientoApp.Common.Exceptions;
using RutaSeguimientoApp.Models.ModelsConfig;
using RutaSeguimientoApp.Models.ModelsEnum;
using RutaSeguimientoApp.Models.ModelsRest;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace RutaSeguimientoApp.Services.RestServices
{
	public abstract class BaseRestServices
	{
		protected abstract BaseRestConfigApp BaseRestConfigApp { get; }
		protected abstract Enum EndPoint { get; }
		protected abstract CookieCollection? CookieCollection { get; }
		private Dictionary<string, string>? _headersGloblas { get; set; }
		private CookieContainer? _cookiesGloblas { get; set; }

		private readonly JsonSerializerOptions options = new()
		{
			Converters = { new JsonStringEnumConverter() },
			PropertyNameCaseInsensitive = true,
			ReferenceHandler = ReferenceHandler.Preserve,
			AllowTrailingCommas = true,
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
		};


		#region Task Rest Methods
		public async Task<T> PostTask<T>(string endPoint, object? body, NameValueCollection? headers = null, MultipartFormDataContent? multipartFormData = null) where T : BaseResponseRest, new()
		{
			return await SendRequestTask<T>(HttpMethod.Post, endPoint, body, headers: headers, multipartFormData: multipartFormData);
		}

		public async Task<T> PutTask<T>(string endPoint, object? body, NameValueCollection? headers = null) where T : BaseResponseRest, new()
		{
			return await SendRequestTask<T>(HttpMethod.Put, endPoint: endPoint, body: body, headers: headers);
		}

		public async Task<T> GetTask<T>(string endPoint, NameValueCollection? headers = null) where T : BaseResponseRest, new()
		{
			return await SendRequestTask<T>(HttpMethod.Get, endPoint, headers: headers);
		}

		public async Task<T> DeleteTask<T>(string endPoint, object? body = null, NameValueCollection? headers = null) where T : BaseResponseRest, new()
		{
			return await SendRequestTask<T>(HttpMethod.Delete, endPoint, body: body, headers: headers);
		}

		#endregion
		#region Metodos de apoyo

		private async Task<T> SendRequestTask<T>(HttpMethod httpMethod, string endPoint, object? body = null, NameValueCollection? headers = null, MultipartFormDataContent? multipartFormData = null) where T : BaseResponseRest, new()
		{
			try
			{
				bool validate = Uri.TryCreate(endPoint, UriKind.Absolute, out Uri? uri);
				if (!validate || uri is null)
				{
					throw new BussinnesException(EnumExceptions.ErrorBuildRequest);
				}

				InserGlobalParameters(uri, headers: headers, CookieCollection);
				using var handler = new HttpClientHandler() { CookieContainer = _cookiesGloblas ?? new() };
				using var httpClient = new HttpClient(handler);
				httpClient.Timeout = TimeSpan.FromMilliseconds(15000);

				HttpRequestMessage httpRequestMessage = new(httpMethod, uri);

				if (_headersGloblas != null && _headersGloblas.Any())
				{
					foreach (KeyValuePair<string, string> key in _headersGloblas)
					{
						httpRequestMessage.Headers.Add(key.Key, key.Value);
					}
				}

				if (body != null)
				{
					httpRequestMessage.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, BaseRestConfigApp.ContentType);
				}

				if (multipartFormData != null)
				{
					httpRequestMessage.Content = multipartFormData;
				}

				try
				{
					HttpResponseMessage httpResponseMessage = httpClient.Send(httpRequestMessage);
					T response = await ReadResponseTask<T>(httpResponseMessage);
					return response;

				}
				catch (BussinnesException) { throw; }
				catch (Exception ex)
				{
					throw new BussinnesException(EnumExceptions.ErrorSendRequest, ex);
				}
			}
			catch (Exception ex)
			{
				throw new BussinnesException(EnumExceptions.ErrorBuildRequest, ex);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TType"></typeparam>
		/// <param name="responseMessage"></param>
		/// <returns></returns>
		/// <exception cref="HttpRequestException"></exception>
		private async Task<TType> ReadResponseTask<TType>(HttpResponseMessage responseMessage) where TType : BaseResponseRest, new()
		{
			string? content = await responseMessage.Content.ReadAsStringAsync();
			switch (responseMessage.StatusCode)
			{
				case HttpStatusCode.OK:
					TType result = JsonSerializer.Deserialize<TType>(content, options) ?? new();
					result.Success = true;
					result.CodeResponse = (int)HttpStatusCode.OK;
					return result;

				case HttpStatusCode.BadRequest:
					return new() { CodeResponse = (int)HttpStatusCode.BadRequest, Error = content };

				case HttpStatusCode.Unauthorized:
					return new() { CodeResponse = (int)HttpStatusCode.Unauthorized, Error = content };

				case HttpStatusCode.NoContent:
					return new() { CodeResponse = (int)HttpStatusCode.NoContent, Error = content };	
			

				default: throw new BussinnesException(EnumExceptions.ErrorStatusReadResponse);

			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="uri"></param>
		/// <param name="headers"></param>
		/// <param name="cookieCollection"></param>
		/// <exception cref="BussinnesException"></exception>
		private void InserGlobalParameters(Uri uri, NameValueCollection? headers = null, CookieCollection? cookieCollection = null)
		{
			try
			{
				_headersGloblas = [];
				_cookiesGloblas = new();

				if (headers != null)
				{
					foreach (string? header in headers.AllKeys)
					{
						if (header != null)
							_headersGloblas.Add(header, headers.Get(header) ?? string.Empty);
					}
				}

				if (BaseRestConfigApp.HeadersGloblas != null)
				{
					foreach (var header in BaseRestConfigApp.HeadersGloblas)
					{
						if (header.Key != null)
							_headersGloblas.Add(header.Key, header.Value ?? string.Empty);
					}
				}

				if (cookieCollection != null)
				{
					_cookiesGloblas.Add(uri, cookieCollection);
				}
			}
			catch (Exception ex)
			{
				throw new BussinnesException(EnumExceptions.ErrorBuildRequest, ex);
			}
		}

		/// <summary>
		/// Construye una url <see langword="QueryString"/> tomando las llaves y los valores de la clase <see cref="NameValueCollection"/>
		/// <example>
		/// <code>
		/// NameValueCollection queryParameters = new (){{"author","Jane Austen"},{"language","english"}};
		/// strin urlEncode = BuildUrlApi(queryParameters);
		/// </code>
		/// retornando 
		/// <remarks>
		/// <![CDATA[ QueryString url: https://localhost:7220/api/Books?author=Jane%20Austen&language=english ]]>
		/// </remarks>
		/// </example>
		/// </summary>
		/// <param name="queryParameters"></param>
		/// <returns></returns>
		protected string BuildUrlApi(NameValueCollection queryParameters)
		{
			try
			{
				UriBuilder uriBuilder = new(BaseRestConfigApp.UrlBase)
				{
					Path = BaseRestConfigApp.Servicios[EndPoint].Path,
				};

				if (queryParameters != null)
				{
					NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);
					query.Add(queryParameters);
					uriBuilder.Query = query.ToString();
					return uriBuilder.ToString();
				}
				return uriBuilder.ToString();
			}
			catch (Exception ex)
			{
				throw new BussinnesException(EnumExceptions.ErrorBuildUrl, ex);
			}
		}

		/// <summary>
		/// Construye una url <see langword="PathSegments"/> tomando las llaves y los valores de la clase <see cref="NameValueCollection"/> 
		/// ye reemplazandolos en el path base enviado
		/// <example>
		/// <code>
		/// string pathBase = "Books/{userId}/Book/{bookId}"
		/// NameValueCollection pathParameters = new (){{"userId","12"},{"bookId","15"}};
		/// strin urlEncode = BuildUrlApi(pathBase,pathParameters);
		/// </code>
		/// retornando 
		/// <remarks>
		/// <![CDATA[ PathSegments url: https://localhost:7220/api/Books/12/Book/15 ]]>
		/// </remarks>
		/// </example>
		/// </summary>
		/// <param name="pathSegments"></param>
		/// <param name="pathParameters"></param>
		/// <returns></returns>
		protected string BuildUrlApi(string pathSegments, NameValueCollection pathParameters = null)
		{
			try
			{
				UriBuilder uriBuilder = new(BaseRestConfigApp.UrlBase)
				{
					Path = BaseRestConfigApp.Servicios[EndPoint].Path + pathSegments,
				};

				if (pathParameters != null)
				{
					foreach (var parameter in pathParameters.AllKeys)
					{

						string? key = Uri.EscapeDataString(string.Concat("{", parameter, "}"));
						string? value = HttpUtility.UrlEncode(pathParameters.Get(parameter));
						if (key != null && value != null)
						{
							uriBuilder.Path = uriBuilder.Path.Replace(key, value);
						}
					}
				}

				return uriBuilder.ToString();
			}
			catch (Exception ex)
			{
				throw new BussinnesException(EnumExceptions.ErrorBuildUrl, ex);
			}
		}

		/// <summary>
		/// retornala url base y el configurado en el appsettings
		/// </summary>
		/// <returns></returns>
		protected string BuildUrlApi()
		{
			try
			{
				UriBuilder uriBuilder = new(BaseRestConfigApp.UrlBase)
				{
					Path = BaseRestConfigApp.Servicios[EndPoint].Path,
				};
				return uriBuilder.ToString();
			}
			catch (Exception ex)
			{
				throw new BussinnesException(EnumExceptions.ErrorBuildUrl, ex);
			}
		}
		#endregion
	}
}
