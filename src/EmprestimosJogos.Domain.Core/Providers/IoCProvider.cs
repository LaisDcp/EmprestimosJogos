using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace EmprestimosJogos.Domain.Core.Providers
{
    public static class IoCProvider
    {
        public static T GetService<T>()
        {
            return new HttpContextAccessor().HttpContext.RequestServices.CreateScope().ServiceProvider.GetService<T>();
        }
    }
}
