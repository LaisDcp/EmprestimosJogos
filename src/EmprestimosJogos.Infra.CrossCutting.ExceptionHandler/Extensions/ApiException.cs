using EmprestimosJogos.Domain.Core.Enum;
using EmprestimosJogos.Infra.CrossCutting.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace EmprestimosJogos.Infra.CrossCutting.ExceptionHandler.Extensions
{
    /// <summary>
    /// API exception customizada para a aplicação, podendo guardar o tipo da exception através do recurso {System.Net.HttpStatusCode}.
    /// <para>Através dessa exception, o retorno da API será com o status em questão.</para>
    /// </summary>
    public class ApiException : Exception
    {
        public ApiException() { }

        /// <summary>
        /// Mensagem e status customizado 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="statusCode"></param>
        /// <param name="innerException"></param>
        public ApiException(string message,
                HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
                Exception innerException = null
                )
           : base(message, innerException)
        {
            StatusCode = statusCode;
            Message = message;
        }

        /// <summary>
        /// Mensagem customizada com status e código de erro definidos
        /// </summary>
        /// <param name="message"></param>
        /// <param name="apiErrorCode"></param>
        public ApiException(string message,
                 ApiErrorCodes apiErrorCode = ApiErrorCodes.UNEXPC
                 )
        {
            StatusCode = ExtensionMethodHelpers.GetStatusCode(apiErrorCode);
            ApiErrorsCodes = new List<ApiErrorCodes>() { apiErrorCode };
            Message = message;
        }

        /// <summary>
        /// Código de erro definido 
        /// </summary>
        /// <param name="apiErrorCode"></param>
        /// <param name="requestData"></param>
        public ApiException(ApiErrorCodes apiErrorCode = ApiErrorCodes.UNEXPC,
                object requestData = null)
        {
            ApiErrorsCodes = new List<ApiErrorCodes>() { apiErrorCode };
            RequestData = requestData;
            StatusCode = ExtensionMethodHelpers.GetStatusCode(apiErrorCode);
            Message = ExtensionMethodHelpers.GetDescription(apiErrorCode);
            Titles = new List<string>() { ExtensionMethodHelpers.GetTitle(apiErrorCode) };
        }

        /// <summary>
        /// Código de erro definido 
        /// </summary>
        /// <param name="apiErrorCode"></param>
        /// <param name="requestData"></param>
        public ApiException(ApiErrorCodes apiErrorCode,
                string field)
        {
            ApiErrorsCodes = new List<ApiErrorCodes>() { apiErrorCode };
            StatusCode = ExtensionMethodHelpers.GetStatusCode(apiErrorCode);
            Message = ExtensionMethodHelpers.GetDescription(apiErrorCode);
            Titles = new List<string>() { ExtensionMethodHelpers.GetTitle(apiErrorCode) };
            ErrorFields = new Dictionary<string, string>() { { field, Message } };
        }

        /// <summary>
        /// Lista de códigos de erros definidos 
        /// </summary>
        /// <param name="apiErrorsCodes"></param>
        /// <param name="httpStatusCode"></param>
        /// <param name="requestData"></param>
        public ApiException(IEnumerable<ApiErrorCodes> apiErrorsCodes,
                HttpStatusCode? httpStatusCode = null,
                object requestData = null
            )
        {
            ApiErrorsCodes = apiErrorsCodes;
            RequestData = requestData;
            StatusCode = httpStatusCode ?? ExtensionMethodHelpers.GetStatusCode(apiErrorsCodes.FirstOrDefault());
            Messages = ExtensionMethodHelpers.GetDescription(apiErrorsCodes);
            Titles = ExtensionMethodHelpers.GetTitle(apiErrorsCodes);
        }

        /// <summary>
        /// Lista de mensagens customizadas com código de erro definido 
        /// </summary>
        /// <param name="messages"></param>
        /// <param name="apiErrorCode"></param>
        /// <param name="requestData"></param>
        public ApiException(IEnumerable<string> messages,
                ApiErrorCodes apiErrorCode = ApiErrorCodes.UNEXPC,
                object requestData = null)
        {
            ApiErrorsCodes = new List<ApiErrorCodes>() { apiErrorCode };
            RequestData = requestData;
            StatusCode = ExtensionMethodHelpers.GetStatusCode(apiErrorCode);
            Messages = messages;
        }

        public ApiException(
            Dictionary<string, string> errors,
            ApiErrorCodes apiErrorCode = ApiErrorCodes.UNEXPC
        )
        {
            ApiErrorsCodes = new List<ApiErrorCodes>() { apiErrorCode };
            StatusCode = ExtensionMethodHelpers.GetStatusCode(apiErrorCode);
            ErrorFields = errors;
        }

        public override string Message { get; }

        public IEnumerable<string> Messages { get; set; }

        public IEnumerable<string> Titles { get; set; }

        public Dictionary<string, string> ErrorFields { get; set; }

        public IEnumerable<ApiErrorCodes> ApiErrorsCodes { get; set; }

        public object RequestData { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}
