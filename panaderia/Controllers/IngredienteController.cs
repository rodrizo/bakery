using Microsoft.AspNetCore.Mvc;
using Panaderia.Services;
using PanaderiaModels.Entities;

namespace Panaderia.Controllers
{
    [ApiController]
    [Route("api/ingrediente")]
    public class IngredienteController : ControllerBase
    {
        IIngredienteService _ingredienteService;

        public IngredienteController(IIngredienteService ingredienteService)
        {
            _ingredienteService = ingredienteService;
        }

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
        public async Task<IActionResult> CrearPan(Pan model)
        {
            try
            {
                var panes = _panService.CrearPan(model);
                return Ok(panes);
            }
            catch (Exception exp)
            {
                return Ok(exp.Message);
            }
        }


        [HttpPut]
        public async Task<IActionResult> EditarPan(int id, Pan model)
        {
            try
            {
                var panes = _panService.EditarPan(id, model);
                return Ok(panes);
            }
            catch (Exception exp)
            {
                return Ok(exp.Message);
            }
        }
    }
}
