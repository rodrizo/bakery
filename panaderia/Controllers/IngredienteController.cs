using Microsoft.AspNetCore.Mvc;
using Panaderia.Services;
using PanaderiaModels.Entities;

namespace Panaderia.Controllers
{
    [ApiController]
    [Route("api/ingrediente")]
    public class IngredienteController : ControllerBase
    {
        private readonly IServiceIngrediente _ingredienteService;

        public IngredienteController(IServiceIngrediente ingredienteService) => this._ingredienteService = ingredienteService;

        [HttpGet]
        public IActionResult ObtenerIngredientes()
        {
            try
            {
                var ingredientes = _ingredienteService.ObtenerIngredientes();
                return Ok(ingredientes);
            }
            catch (Exception exp)
            {
                return Ok(exp.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CrearIngrediente(Ingrediente model)
        {
            try
            {
                var ingredientes = _ingredienteService.CrearIngrediente(model);
                return Ok(ingredientes);
            }
            catch (Exception exp)
            {
                return Ok(exp.Message);
            }
        }


        [HttpPut]
        public async Task<IActionResult> EditarIngrediente(int id, Ingrediente model)
        {
            try
            {
                var ingredientes = _ingredienteService.EditarIngrediente(id, model);
                return Ok(ingredientes);
            }
            catch (Exception exp)
            {
                return Ok(exp.Message);
            }
        }
    }
}
