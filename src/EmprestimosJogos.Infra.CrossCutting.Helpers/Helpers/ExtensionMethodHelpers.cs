using FluentValidation.Results;
using EmprestimosJogos.Domain.Core.Attributes;
using EmprestimosJogos.Domain.Core.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using DescriptionAttribute = EmprestimosJogos.Domain.Core.Attributes.DescriptionAttribute;


namespace EmprestimosJogos.Infra.CrossCutting.Helpers
{
    public static class ExtensionMethodHelpers
    {
        /// <summary>
        /// Converter o objeto atual para JSON.
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="myObject"></param>
        /// <returns></returns>
        public static string ToJson<TType>(this TType myObject)
        {
            return JsonConvert.SerializeObject(myObject, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        /// <summary>
        /// Obter o valor Titulo da Annotation Description: [Description("Minha Descrição", "Meu titulo")]
        /// </summary>
        /// <typeparam name="TType">automaticamente obtido com o objeto.</typeparam>
        /// <param name="myEnum">objeto do tipo Enum.</param>
        /// <returns></returns>

        public static string GetTitle<TType>(this TType myEnum)
        {
            System.Reflection.FieldInfo _fieldInfo = myEnum.GetType().GetField(myEnum.ToString());

            DescriptionAttribute[] _customAttributes = (DescriptionAttribute[])_fieldInfo.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            return
                   _customAttributes != null && _customAttributes.Any()
                       ? _customAttributes[0].Title
                       : myEnum.ToString();
        }

        /// <summary>
        /// Obter o valor Titulo da Annotation Description de uma lista de itens: [Description("Minha Descrição", "Meu titulo")]
        /// </summary>
        /// <typeparam name="TType">automaticamente obtido com o objeto.</typeparam>
        /// <param name="myEnum">objeto do tipo Enum.</param>
        /// <returns></returns>
        public static IEnumerable<string> GetTitle<TType>(this IEnumerable<TType> myEnumList)
        {
            List<string> _titles = new List<string>();

            foreach (TType item in myEnumList)
            {
                System.Reflection.FieldInfo _fieldInfo = item.GetType().GetField(item.ToString());

                DescriptionAttribute[] _customAttributes = (DescriptionAttribute[])_fieldInfo.GetCustomAttributes(
                    typeof(DescriptionAttribute), false);

                string _title = _customAttributes != null && _customAttributes.Any()
                                       ? _customAttributes[0].Title
                                       : item.ToString();

                _titles.Add(_title);
            }

            return _titles;
        }

        /// <summary>
        /// Obter o valor da Annotation Description: [Description("Minha Descrição")]
        /// </summary>
        /// <typeparam name="TType">automaticamente obtido com o objeto.</typeparam>
        /// <param name="myEnum">objeto do tipo Enum.</param>
        /// <returns></returns>

        public static string GetDescription<TType>(this TType myEnum) where TType : Enum
        {
            System.Reflection.FieldInfo _fieldInfo = myEnum.GetType().GetField(myEnum.ToString());

            DescriptionAttribute[] _customAttributes = (DescriptionAttribute[])_fieldInfo?.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            return
                   _customAttributes != null && _customAttributes.Any()
                       ? _customAttributes[0].Description
                       : myEnum.ToString();
        }

        /// <summary>
        /// Obter o valor da Annotation Description de uma lista de itens: [Description("Minha Descrição")]
        /// </summary>
        /// <typeparam name="TType">automaticamente obtido com o objeto.</typeparam>
        /// <param name="myEnum">objeto do tipo Enum.</param>
        /// <returns></returns>
        public static IEnumerable<string> GetDescription<TType>(this IEnumerable<TType> myEnumList)
        {
            List<string> _descriptions = new List<string>();

            foreach (TType item in myEnumList)
            {
                System.Reflection.FieldInfo _fieldInfo = item.GetType().GetField(item.ToString());

                DescriptionAttribute[] _customAttributes = (DescriptionAttribute[])_fieldInfo.GetCustomAttributes(
                    typeof(DescriptionAttribute), false);

                string _description = _customAttributes != null && _customAttributes.Any()
                                       ? _customAttributes[0].Description
                                       : item.ToString();

                _descriptions.Add(_description);
            }

            return _descriptions;
        }

        /// <summary>
        /// Obter os erros do ModelState separados por ponto e vírgula.
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static string GetAllErrorsToString(this ModelStateDictionary modelState)
        {
            IEnumerable<string> _errors = modelState?.Values.SelectMany(x => x?.Errors)
                            ?.Select(x => x?.ErrorMessage);

            return _errors != null && _errors.Any()
                        ? string.Join(" ", _errors)
                        : "Verifique os dados inseridos.";
        }

        /// <summary>
        /// Obter os erros do ModelState separados por ponto e vírgula.
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetAllErrors(this ModelStateDictionary modelState)
        {
            IEnumerable<string> _errors = modelState?.Values.SelectMany(x => x?.Errors)
                            ?.Select(x => x?.ErrorMessage);

            return _errors != null && _errors.Any()
                        ? _errors
                        : new List<string>() { "Verifique os dados inseridos." };
        }

        /// <summary>
        /// Obter os erros do ModelState separados por ponto e vírgula.
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetAllErrorsFields(this ModelStateDictionary modelState)
        {
            List<string> _fields = modelState?.Keys.ToList();
            List<string> _errors = modelState?.Values.SelectMany(s => s?.Errors)
                            .Select(s => s?.ErrorMessage).ToList();

            return (from f in _fields
                    from e in _errors
                    select new { Field = f, Error = e }).ToDictionary(d => d.Field, d => d.Error);
        }

        /// <summary>
        /// Obter o valor da Annotation StatusCode: [StatusCode(500)]
        /// </summary>
        /// <typeparam name="TType">automaticamente obtido com o objeto.</typeparam>
        /// <param name="myEnum">objeto do tipo Enum.</param>
        /// <returns></returns>
        public static HttpStatusCode GetStatusCode<TType>(this TType myEnum)
        {
            System.Reflection.FieldInfo _fieldInfo = myEnum.GetType().GetField(myEnum.ToString());

            HttpStatusCodeAttribute[] _customAttributes = (HttpStatusCodeAttribute[])_fieldInfo.GetCustomAttributes(
                typeof(HttpStatusCodeAttribute), false);

            return
                   _customAttributes != null && _customAttributes.Any()
                       ? _customAttributes[0].HttpStatusCode
                       : HttpStatusCode.InternalServerError;
        }

        public static Dictionary<string, string> GetErrors(this ValidationResult validationResult)
        {
            return validationResult.Errors.DistinctBy(d => d.PropertyName).ToDictionary(e => e.PropertyName, e => e.ErrorMessage);
        }

        public static bool TryDeserializeJson<TType>(this object objToConvert, out TType resultParsed)
        {
            resultParsed = default(TType);

            try
            {
                if (!(objToConvert is string))
                    objToConvert = objToConvert.ToJson();

                resultParsed = JsonConvert.DeserializeObject<TType>(objToConvert as string);

                return resultParsed != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string StringToBase64(string input)
        {
            byte[] encodedBytes = System.Text.Encoding.Default.GetBytes(input);
            string encodedTxt = Convert.ToBase64String(encodedBytes, Base64FormattingOptions.None);
            return encodedTxt;
        }

        public static string Base64ToString(string input)
        {
            byte[] decodedBytes = ConvertFromBase64String(input);
            string decodedTxt2 = System.Text.Encoding.Default.GetString(decodedBytes);
            decodedTxt2 = decodedTxt2.Substring(0, decodedTxt2.Length);
            return decodedTxt2;
        }

        private static byte[] ConvertFromBase64String(string input)
        {
            // Github link to dotnet core bug
            // https://github.com/dotnet/corefx/issues/30793
            if (string.IsNullOrWhiteSpace(input))
                return null;

            try
            {
                string working = input.Replace('-', '+').Replace('_', '/');
                while (working.Length % 4 != 0)
                {
                    working += '=';
                }
                return Convert.FromBase64String(working);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<DateTime> GetDateRange(DateTime dataInicio, DateTime dataFim)
        {
            return Enumerable.Range(0, 1 + dataFim.Subtract(dataInicio).Days)
                                                    .Select(offset => dataInicio.AddDays(offset))
                                                    .ToList();
        }

        /// <summary>
        /// Verifica se um valor está presente em uma lista.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsInList<T>(this T value, IEnumerable<T> list) => list.Contains(value);

        /// <summary>
        /// Converte uma lista de string em uma string separado por um separador específicado.
        /// Exemplo de retorno: "Item A, Item B, Item C" (sem lastSeparador definido). ou "Item A, Item B e Item C" (com lastSeparador definido).
        /// </summary>
        /// <param name="list">Lista para converter</param>
        /// <param name="separator">String para separar os itens. Ex.: ", "</param>
        /// <param name="lastSeparator">(Opcional) String para separar o último item. Ex.: " e "</param>
        /// <returns></returns>
        public static string ToStringWithSeparator(this IEnumerable<string> list, string separator, string lastSeparator = null)
        {
            string _newString = list.Aggregate((current, next) => $"{current}{separator}{next}");

            if (lastSeparator == null)
                return _newString;

            return _newString.ReplaceLast(separator, lastSeparator);
        }

        /// <summary>
        /// Adiciona aspas (") em uma string.
        /// Ex.: str -> "str"
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string WithAspas(this string str)
        {
            return $"\"{str}\"";
        }

        /// <summary>
        /// Substítui a última ocorrencia de uma string por uma substring.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="find"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string ReplaceLast(this string str, string find, string replace)
        {
            int _index = str.LastIndexOf(find);

            if (_index == -1)
                return str;

            return str.Remove(_index, find.Length)
                      .Insert(_index, replace);
        }

        /// <summary>
        /// Verifica se o guid é vazio
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsEmpty(this Guid id)
        {
            return id == Guid.Empty;
        }

        public static string RemovePontosAndHifens(this string str)
        {
            return str.Replace(".", "").Replace("-", "");
        }
    }
}