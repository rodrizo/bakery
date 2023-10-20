using Microsoft.AspNetCore.Mvc;
using Panaderia.Services;
using PanaderiaModels.Entities;

namespace Panaderia.Controllers
{
    [ApiController]
    [Route("api/receta")]
    public class RecetaController : ControllerBase
    {
        IRecetaService _recetaService;

        public RecetaController(IRecetaService recetaService)
        {
            _recetaService = recetaService;
        }

        [HttpGet]
        public IActionResult ObtenerItemsRecetas()
        {
            try
            {
                var recetas = _recetaService.ObtenerItemsRecetas();
                return Ok(recetas);
            }
            catch (Exception exp)
            {
                return Ok(exp.Message);
            }
        }


        //[HttpPost]
        //public async Task<IActionResult> CrearIngrediente(Ingrediente model)
        //{
        //    try
        //    {
        //        var ingredientes = _ingredienteService.CrearIngrediente(model);
        //        return Ok(ingredientes);
        //    }
        //    catch (Exception exp)
        //    {
        //        return Ok(exp.Message);
        //    }
        //}


        //[HttpPut]
        //public async Task<IActionResult> EditarIngrediente(int id, Ingrediente model)
        //{
        //    try
        //    {
        //        var ingredientes = _ingredienteService.EditarIngrediente(id, model);
        //        return Ok(ingredientes);
        //    }
        //    catch (Exception exp)
        //    {
        //        return Ok(exp.Message);
        //    }
        //}
    }
}
