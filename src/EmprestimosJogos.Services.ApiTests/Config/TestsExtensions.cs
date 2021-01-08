using System.Net.Http;
using System.Net.Http.Headers;

namespace EmprestimosJogos.Services.ApiTests.Config
{
    public static class TestsExtensions
    {
        public static void AtribuirToken(this HttpClient client, string token)
        {
            client.AtribuirJsonMediaType();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public static void AtribuirJsonMediaType(this HttpClient client)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static void AtribuirUsuarioId(this HttpClient client, string usuarioId)
        {
            client.DefaultRequestHeaders.Add("UsuarioId", usuarioId);
        }
    }
}