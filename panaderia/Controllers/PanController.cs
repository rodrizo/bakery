using Microsoft.AspNetCore.Mvc;
using Panaderia.Services;
using PanaderiaModels.Entities;

namespace Panaderia.Controllers
{
    [ApiController]
    [Route("api/pan")]
    public class PanController : ControllerBase
    {
        IPanService _panService;

        public PanController(IPanService panService)
        {
            _panService = panService;
        }

        [HttpGet]
        public IActionResult ObtenerPanes()
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
    }
}
