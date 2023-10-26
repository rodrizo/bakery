using Microsoft.AspNetCore.Mvc;
using Panaderia.Services;
using PanaderiaModels.Entities;

namespace Panaderia.Controllers
{
    [ApiController]
    [Route("api/pan")]
    public class PanController : ControllerBase
    {
        private readonly IPanService _panService;

        public PanController(IPanService panService) => this._panService = panService;

        [HttpGet]
        public async Task<IActionResult> ObtenerPanes()
        {
            try
            {
                var panes = _panService.ObtenerPanes();
                return Ok(panes);
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
