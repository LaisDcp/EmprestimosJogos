using EmprestimosJogos.Domain.Core.DTO;
using EmprestimosJogos.Domain.Core.Enum;
using EmprestimosJogos.Infra.CrossCutting.ExceptionHandler.Extensions;
using EmprestimosJogos.Infra.CrossCutting.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace EmprestimosJogos.Infra.CrossCutting.ExceptionHandler.Providers
{
    public static class ExceptionHandlerMiddleware
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = async context =>
                {
                    IExceptionHandlerPathFeature _exceptionHandler = context.Features.Get<IExceptionHandlerPathFeature>();
                    if (_exceptionHandler == null)
                        return;

                    IEnumerable<ApiErrorCodes> _errorsCodes = _exceptionHandler.Error is ApiException
                                                                ? ((ApiException)_exceptionHandler.Error).ApiErrorsCodes
                                                                : new List<ApiErrorCodes>() { ApiErrorCodes.UNEXPC };

                    IEnumerable<string> _exceptionTitles = new List<string>();
                    IEnumerable<string> _exceptionMessages = new List<string>() { _exceptionHandler.Error.Message };
                    IDictionary<string, string> _errorFields = new Dictionary<string, string>();
                    string _requestData = string.Empty;

                    int _httpStatusCode = StatusCodes.Status500InternalServerError;

                    _requestData = _exceptionHandler.Error is ApiException
                                    ? ((ApiException)_exceptionHandler.Error).RequestData?.ToJson()
                                    : string.Empty;

                    if (_exceptionHandler.Error is WebException && (_exceptionHandler.Error as WebException)?.Response != null)
                    {
                        HttpWebResponse _httpWebResponse = (_exceptionHandler.Error as WebException)?.Response as HttpWebResponse;

                        using (Stream responseStream = _httpWebResponse.GetResponseStream())
                        {
                            using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
                            {
                                reader.ReadToEnd().TryDeserializeJson(out ExceptionDetailsDTO _exception);

                                _exceptionMessages = new List<string>() { _exception?.Message };
                                _httpStatusCode = _exception != null
                                                    ? _exception.HttpStatusCode
                                                    : (int)_httpWebResponse.StatusCode;
                            }
                        }
                    }
                    else if (_exceptionHandler.Error is ApiException _exception)
                    {
                        _httpStatusCode = (int)_exception.StatusCode;
                        _exceptionMessages = _exception.Messages ?? new List<string> { _exceptionHandler.Error.Message };
                        _exceptionTitles = _exception.Titles ?? new List<string>();
                        _errorFields = _exception.ErrorFields ?? new Dictionary<string, string>();
                    }
                    else
                        _exceptionMessages = new List<string> { _exceptionHandler.Error.Message };

                    context.Response.StatusCode = _httpStatusCode;
                    context.Response.ContentType = "application/json";
                    List<ExceptionMessageDTO> _messages = new List<ExceptionMessageDTO>();

                    if (_exceptionMessages.Any())
                    {
                        _messages = _exceptionMessages.Select((s, index) => new ExceptionMessageDTO
                        {
                            Message = s,
                            Code = index < _errorsCodes.Count()
                                                        ? _errorsCodes.ElementAt(index).ToString()
                                                        : _errorsCodes.FirstOrDefault().ToString(),
                            Title = index < _exceptionTitles.Count()
                                                        ? _exceptionTitles.ElementAt(index)?.ToString()
                                                        : _exceptionTitles.FirstOrDefault()?.ToString(),
                            Field = index < _errorFields.Count()
                                                        ? _errorFields.ElementAt(index).Key
                                                        : _errorFields.FirstOrDefault().Key
                        }).ToList();
                    }

                    if (_errorFields.Any())
                    {
                        _messages = _errorFields.Select((s, index) => new ExceptionMessageDTO
                        {
                            Field = s.Key,
                            Message = s.Value,
                            Code = index < _errorsCodes.Count()
                                                    ? _errorsCodes.ElementAt(index).ToString()
                                                    : _errorsCodes.FirstOrDefault().ToString(),
                            Title = index < _exceptionTitles.Count()
                                                    ? _exceptionTitles.ElementAt(index)?.ToString()
                                                    : _exceptionTitles.FirstOrDefault()?.ToString()
                        }).ToList();
                    }

                    await context.Response.WriteAsync(new ExceptionDetailsDTO
                    {
                        HttpStatusCode = _httpStatusCode,
                        Errors = _messages
                    }.ToJson());
                }
            });
        }
    }
}
