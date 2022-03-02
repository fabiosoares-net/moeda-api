using ClosedXML.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Wipro.ConsoleApp.Model;

namespace Wipro.ConsoleApp.Service
{
    public class MoedaService
    {
        public void GerarArquivo(List<MoedaDataCotacao> listaMoedaDataCotacao)
        {
            var caminho = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Arquivos");

            if (!System.IO.Directory.Exists(caminho))
            {
                System.IO.Directory.CreateDirectory(caminho);
            }

            var arquivo = $"Resultado_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}.csv";
            var caminhoArquivo = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Arquivos", arquivo);

            using (var fs = new FileStream(caminhoArquivo, FileMode.Create, FileAccess.Write))
            using (var sw = new StreamWriter(fs))
            {
                sw.WriteLine($"ID_MOEDA;DATA_REF;VLR_COTACAO");

                foreach (var item in listaMoedaDataCotacao)
                {
                    sw.WriteLine($"{item.IdMoeda};{item.DataRef};{item.VlrCotacao}");
                }
            }
        }

        public List<Moeda> ListarDadoMoeda()
        {
            string[] linhaSeparada = null;
            Moeda moeda = null;
            var listaDadoMoeda = new List<Moeda>();
            var caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "DadosMoeda.csv");

            using (StreamReader sr = new StreamReader(caminhoArquivo))
            {
                string linha;

                while ((linha = sr.ReadLine()) != null)
                {
                    linhaSeparada = linha.Split(';');

                    if (!linhaSeparada.Any(item => item.Equals("ID_MOEDA")))
                    {
                        var idMoeda = linhaSeparada[0];
                        var dataRef = DateTime.Parse(linhaSeparada[1]);

                        moeda = new Moeda()
                        {
                            IdMoeda = idMoeda,
                            DataRef = dataRef
                        };

                        listaDadoMoeda.Add(moeda);
                    }
                }
            }

            return listaDadoMoeda;
        }

        public List<Cotacao> ListarDadoCotacao()
        {
            string[] linhaSeparada = null;
            Cotacao cotacao = null;
            var listaDadoCotacao = new List<Cotacao>();
            var caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "DadosCotacao.csv");

            using (StreamReader sr = new StreamReader(caminhoArquivo))
            {
                string linha;

                while ((linha = sr.ReadLine()) != null)
                {
                    linhaSeparada = linha.Split(';');

                    if (!linhaSeparada.Any(item => item.Equals("vlr_cotacao")))
                    {
                        var vlrCotacao = double.Parse(linhaSeparada[0]);
                        var codCotacao = int.Parse(linhaSeparada[1]);
                        var datCotacao = DateTime.Parse(linhaSeparada[2]);

                        cotacao = new Cotacao()
                        {
                            VlrCotacao = vlrCotacao,
                            CodCotacao = codCotacao,
                            DataCotacao = datCotacao
                        };

                        listaDadoCotacao.Add(cotacao);
                    }
                }
            }

            return listaDadoCotacao;
        }

        public List<DadoValorCotacao> ListarDadoValorCotacao()
        {
            string[] linhaSeparada = null;
            DadoValorCotacao dadoValorCotacao = null;
            var listaDadoValorCotacao = new List<DadoValorCotacao>();
            var caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "DadoValorCotacao.csv");

            using (StreamReader sr = new StreamReader(caminhoArquivo))
            {
                string linha;

                while ((linha = sr.ReadLine()) != null)
                {
                    linhaSeparada = linha.Split(',');

                    if (!linhaSeparada.Any(item => item.Equals("id_moeda")))
                    {
                        var idMoeda = linhaSeparada[0];
                        var codCotacao = int.Parse(linhaSeparada[1]);

                        dadoValorCotacao = new DadoValorCotacao()
                        {
                            IdMoeda = idMoeda,
                            CodCotacao = codCotacao
                        };

                        listaDadoValorCotacao.Add(dadoValorCotacao);
                    }
                }
            }

            return listaDadoValorCotacao;
        }
    }
}
