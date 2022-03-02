using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Wipro.ConsoleApp.Controller;
using Wipro.ConsoleApp.Service;

namespace Wipro.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch Cronometro = null;

            while (true)
            {
                Cronometro = Stopwatch.StartNew();
                
                Thread.Sleep(TimeSpan.FromSeconds(5));

                Console.WriteLine("Processo em execução...");

                var moedaController = new MoedaController();
                moedaController.ProcessarDadoMoeda();

                var tempo = Cronometro.Elapsed.ToString(@"hh\:mm\:ss");

                Console.WriteLine($"Tempo de processamento: {tempo}");

                Console.WriteLine($"O processamento será reiniciado em 2 minutos, Aguarde...");

                Thread.Sleep(TimeSpan.FromMinutes(2)); 
            }
        }
    }
}
