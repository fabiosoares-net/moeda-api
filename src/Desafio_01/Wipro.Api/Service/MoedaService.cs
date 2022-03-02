using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wipro.Api.Model;

namespace Wipro.Api.Service
{
    public class MoedaService
    {
        public MoedaResponse AddItemFila(List<MoedaModel> listaMoedaModel)
        {
            var moedaResponse = new MoedaResponse();

            var listaMoedaArquivo = ListarMoedaArquivo();

            listaMoedaModel.ForEach(moeda => 
            {
                if (ValidarObrigatoriedade(moeda) &&
                        !listaMoedaArquivo.Any(x => x.Moeda.Trim().Equals(moeda.Moeda.Trim()) 
                          && Convert.ToDateTime(x.Data_Inicio) == Convert.ToDateTime(moeda.Data_Inicio)
                          && Convert.ToDateTime(x.Data_Fim) == Convert.ToDateTime(moeda.Data_Fim)))
                {
                    moedaResponse.ListaIncluidoComSucesso.Add(moeda);
                }
            });

            if (moedaResponse.ListaIncluidoComSucesso.Any())
            {
                listaMoedaArquivo.AddRange(moedaResponse.ListaIncluidoComSucesso);

                RescreverArquivo(listaMoedaArquivo);
            }

            return moedaResponse;
        }

        public MoedaModel GetItemFila()
        {
            var listaMoedaArquivo = ListarMoedaArquivo();

            var moedaItem = listaMoedaArquivo.LastOrDefault();

            if (moedaItem != null)
            {
                var itemIdex = listaMoedaArquivo
                    .FindIndex(x => x.Moeda.Equals(moedaItem.Moeda));

                listaMoedaArquivo.RemoveAt(itemIdex);
                
                RescreverArquivo(listaMoedaArquivo);
            }

            return moedaItem;
        }

        private bool ValidarObrigatoriedade(MoedaModel moedaModel)
        {
            bool resultado = true;

            if (string.IsNullOrEmpty(moedaModel.Moeda) ||
                string.IsNullOrEmpty(moedaModel.Data_Inicio) ||
                string.IsNullOrEmpty(moedaModel.Data_Fim))
            {
                return resultado = false;
            }

            if (DateTime.TryParse(moedaModel.Data_Inicio, out DateTime tempDataInicio) == false)
            {
                return resultado = false;
            }

            if (DateTime.TryParse(moedaModel.Data_Fim, out DateTime tempDataFim) == false)
            {
                return resultado = false;
            }

            if (Convert.ToDateTime(moedaModel.Data_Inicio) > Convert.ToDateTime(moedaModel.Data_Fim))
            {
                return resultado = false;
            }

            return resultado;
        }

        private List<MoedaModel> ListarMoedaArquivo()
        {
            var caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "moeda.json");

            var json = File.ReadAllText(caminhoArquivo);

            var listaMoedaModel = JsonConvert.DeserializeObject<List<MoedaModel>>(json);

            return listaMoedaModel;
        }

        private void RescreverArquivo(List<MoedaModel> listaMoedaModel)
        {
            var caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "moeda.json");

            var json = JsonConvert.SerializeObject(listaMoedaModel, Formatting.Indented);

            File.WriteAllText(caminhoArquivo, json);
        }
    }
}
