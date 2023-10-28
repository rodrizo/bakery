using Microsoft.AspNetCore.Mvc;
using Panaderia.Services;
using PanaderiaModels.Entities;

namespace Panaderia.Controllers
{
    [ApiController]
    [Route("api/pedido")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService) => this._pedidoService = pedidoService;

        [HttpGet("getAll/{id}")]
        public IActionResult ObtenerPedidos([FromRoute] int id)
        {
            try
            {
                var pedidos = _pedidoService.ObtenerPedidos(id);
                return Ok(pedidos);
            }
            catch (Exception exp)
            {
                return Ok(exp.Message);
            }
        }


        [HttpGet("{pedidoId}")]
        public IActionResult ObtenerPedidoById([FromRoute] int pedidoId)
        {
            try
            {
                var pedidos = _pedidoService.ObtenerPedidoById(pedidoId);
                return Ok(pedidos);
            }
            catch (Exception exp)
            {
                return Ok(exp.Message);
            }
        }

        [HttpGet("item/{pedidoId}")]
        public IActionResult ObtenerItemsPedido([FromRoute] int pedidoId)
        {
            try
            {
                var items = _pedidoService.ObtenerItemsPedido(pedidoId);

                return Ok(items);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

        [Route("add/{sucursalId}")]
        [HttpPost]
        public async Task<IActionResult> CrearPedido(int sucursalId, Pedido model)
        {
            try
            {
                var pedido = _pedidoService.CrearPedido(sucursalId, model);
                return Ok(pedido);
            }
            catch (Exception exp)
            {
                return Ok(exp.Message);
            }
        }

        [Route("add/item/{pedidoId}")]
        [HttpPost]
        public async Task<IActionResult> CrearPedidoItem(int pedidoId, PedidoPan model)
        {
            try
            {
                var pedido = _pedidoService.CrearPedidoItem(pedidoId, model);

                return ((pedido).Contains("Solo") || (pedido).Contains("Error")) ? BadRequest(pedido) : Ok(pedido);

            }
            catch (Exception exp)
            {
                return Ok(exp.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditarPedido(int id, Pedido model)
        {
            try
            {
                var recetas = _pedidoService.EditarPedido(id, model);
                return Ok(recetas);
            }
            catch (Exception exp)
            {
                return Ok(exp.Message);
            }
        }

        [Route("item")]
        [HttpPut]
        public async Task<IActionResult> EditarPedidoItem(int id, PedidoPan model)
        {
            try
            {
                var recetas = _pedidoService.EditarPedidoItem(id, model);
                return Ok(recetas);
            }
            catch (Exception exp)
            {
                return Ok(exp.Message);
            }
        }
    }
}
