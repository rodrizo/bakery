﻿using Microsoft.AspNetCore.Mvc;
using Panaderia.Services;
using PanaderiaModels.Entities;

namespace Panaderia.Controllers
{
    [ApiController]
    [Route("api/receta")]
    public class RecetaController : ControllerBase
    {
        private readonly IRecetaService _recetaService;

        public RecetaController(IRecetaService recetaService) => this._recetaService = recetaService;

        [HttpGet]
        public IActionResult ObtenerRecetas()
        {
            try
            {
                var recetas = _recetaService.ObtenerRecetas();
                return Ok(recetas);
            }
            catch (Exception exp)
            {
                return Ok(exp.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerItemsRecetas([FromRoute] int id)
        {
            try
            {
                var recetas = _recetaService.ObtenerItemsRecetas(id);
                return Ok(recetas);
            }
            catch (Exception exp)
            {
                return Ok(exp.Message);
            }
        }

        [Route("add/{id}")]
        [HttpPost]
        public async Task<IActionResult> CrearItemReceta(int id, ItemReceta model)
        {
            try
            {
                var recetas = _recetaService.CrearItemReceta(id, model);
                return Ok(recetas);
            }
            catch (Exception exp)
            {
                return Ok(exp.Message);
            }
        }


        [HttpPut]
        public async Task<IActionResult> EditarItemReceta(int id, ItemReceta model)
        {
            try
            {
                var recetas = _recetaService.EditarItemReceta(id, model);
                return Ok(recetas);
            }
            catch (Exception exp)
            {
                return Ok(exp.Message);
            }
        }
    }
}
