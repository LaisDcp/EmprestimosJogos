using EmprestimosJogos.Domain.Core.DTO;
using EmprestimosJogos.Domain.Interfaces.Facades;
using EmprestimosJogos.Infra.CrossCutting.BuscaCEP.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmprestimosJogos.Infra.CrossCutting.BuscaCEP.Facades
{
    public class BuscaCEPFacade : IBuscaCEPFacade
    {
        private readonly IConfiguration _configuration;
        private readonly IRequestFacade _facadeRequest;

        private readonly BuscaCEPFacadeSettings _buscaCEPFacadeSettings;

        public BuscaCEPFacade(IConfiguration configuration,
                              IRequestFacade requestHelper)
        {
            _configuration = configuration;
            _facadeRequest = requestHelper;

            _buscaCEPFacadeSettings = _configuration.GetSection(nameof(BuscaCEPFacadeSettings))?.Get<BuscaCEPFacadeSettings>();

        }

        public async Task<ViaCEPRetornoBuscaDTO> GetCEPAsync(string cep)
        {
            ViaCEPRetornoBuscaDTO _cep = await _facadeRequest.CreateAsync<ViaCEPRetornoBuscaDTO>(HttpMethod.Get, string.Format(_buscaCEPFacadeSettings.BaseUrl, cep));
            return _cep;
        }
    }
}
