using Microsoft.AspNetCore.Mvc;
using Panaderia.Services;
using PanaderiaModels.Entities;

namespace Panaderia.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService) => this._stockService = stockService;

        [HttpGet]
        public IActionResult ObtenerStock()
        {
            try
            {
                var stock = _stockService.ObtenerStock();
                return Ok(stock);
            }
            catch (Exception exp)
            {
                return Ok(exp.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditarStock(int id, Stock model)
        {
            try
            {
                var stock = _stockService.EditarStock(id, model);
                return Ok(stock);
            }
            catch (Exception exp)
            {
                return Ok(exp.Message);
            }
        }
    }
}
