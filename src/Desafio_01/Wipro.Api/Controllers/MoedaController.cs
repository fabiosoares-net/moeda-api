using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Wipro.Api.Model;
using Wipro.Api.Service;

namespace Wipro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoedaController : ControllerBase
    {
        [HttpPost]
        public IActionResult AddItemFila(List<MoedaModel> listaMoedaModel)
        {
            try
            {
                var moedaService = new MoedaService();

                return Ok(moedaService.AddItemFila(listaMoedaModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetItemFila()
        {
            try
            {
                var moedaService = new MoedaService();

                var moeda = moedaService.GetItemFila();

                if (moeda == null)
                {
                    return BadRequest("Não existe Moeda a ser retornada");
                }

                return Ok(moeda);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}