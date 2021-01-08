using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
namespace EmprestimosJogos.Infra.CrossCutting.IoC.Providers
{
    public static class IocProvider
    {
        public static T GetService<T>()
        {
            return new HttpContextAccessor().HttpContext.RequestServices.GetService<T>();
        }
    }
}
