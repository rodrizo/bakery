using Microsoft.AspNetCore.Mvc;
using Panaderia.Services;

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

        //Get: api/Pan
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
    }
}
