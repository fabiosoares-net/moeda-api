using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wipro.ConsoleApp.Model;
using Wipro.ConsoleApp.Service;

namespace Wipro.ConsoleApp.Controller
{
    public class MoedaController
    {
        public void ProcessarDadoMoeda()
        {
            var webApiClientService = new WebApiClientService();

            var moedaDTO = webApiClientService.GetItemFila("api/moeda");

            if (moedaDTO != null)
            {
                var moedaService = new MoedaService();
                var listaArquivo = new List<MoedaDataCotacao>();

                var listaDadoMoedaData = moedaService.ListarDadoMoeda();

                var listaMoedaData = listaDadoMoedaData
                    .Where(item => (item.DataRef >= moedaDTO.Data_Inicio && item.DataRef <= moedaDTO.Data_Fim) ||
                           item.IdMoeda.Trim().Equals(moedaDTO.Moeda.Trim()))
                    .OrderBy(x => x.DataRef).ThenBy(x => x.IdMoeda)
                    .ToList();

                var listaDadoCotacao = moedaService.ListarDadoCotacao();
                var listaDadoValorCotacao = moedaService.ListarDadoValorCotacao();

                var queryMoedaCotacao = (from dc in listaDadoCotacao
                                         join dvc in listaDadoValorCotacao
                                            on dc.CodCotacao equals dvc.CodCotacao
                                         select new MoedaCotacao()
                                         {
                                             IdMoeda = dvc.IdMoeda,
                                             VlrCotacao = dc.VlrCotacao,
                                             DataCotacao = dc.DataCotacao
                                         }).Distinct().ToList();

                foreach (var item in listaMoedaData)
                {
                    var existeMoedaCotacao = queryMoedaCotacao
                        .FirstOrDefault(md => md.IdMoeda.Equals(item.IdMoeda) && md.DataCotacao == item.DataRef);

                    if (existeMoedaCotacao != null)
                    {
                        listaArquivo.Add(new MoedaDataCotacao()
                        {
                            IdMoeda = existeMoedaCotacao.IdMoeda,
                            DataRef = existeMoedaCotacao.DataCotacao.ToString("yyyy-MM-dd"),
                            VlrCotacao = existeMoedaCotacao.VlrCotacao
                        });
                    }
                }

                moedaService.GerarArquivo(listaArquivo);
            }
        }
    }
}
