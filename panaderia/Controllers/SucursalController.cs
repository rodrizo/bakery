using Microsoft.AspNetCore.Mvc;
using Panaderia.Services;
using PanaderiaModels.Entities;

namespace Panaderia.Controllers
{
    [ApiController]
    [Route("api/sucursal")]
    public class SucursalController : ControllerBase
    {
        ISucursalService _sucursalService;

        public SucursalController(ISucursalService sucursalService)
        {
            _sucursalService = sucursalService;
        }

        [HttpGet]
        public IActionResult ObtenerSucursales()
        {
            try
            {
                var sucursales = _sucursalService.ObtenerSucursales();
                return Ok(sucursales);
            }
            catch (Exception exp)
            {
                return Ok(exp.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CrearSucursal(Sucursal model)
        {
            try
            {
                var sucursales = _sucursalService.CrearSucursal(model);
                return Ok(sucursales);
            }
            catch (Exception exp)
            {
                return Ok(exp.Message);
            }
        }


        [HttpPut]
        public async Task<IActionResult> EditarSucursal(int id, Sucursal model)
        {
            try
            {
                var sucursales = _sucursalService.EditarSucursal(id, model);
                return Ok(sucursales);
            }
            catch (Exception exp)
            {
                return Ok(exp.Message);
            }
        }
    }
}
