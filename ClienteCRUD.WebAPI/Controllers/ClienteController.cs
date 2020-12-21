using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClienteCRUD.Domain;
using ClienteCRUD.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClienteCRUD.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        public readonly IClienteCRUDRepository _repo;

        public ClienteController(IClienteCRUDRepository repo)
        {
            _repo = repo;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {   var results = await _repo.GetAllClienteAsync();

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
        }

        // GET api/values/5
        [HttpGet("{ClienteId}")]
        public async Task<IActionResult> Get(int ClienteId)
        {
            try
            {   
                var results = await _repo.GetClienteByIdAsync(ClienteId);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Cliente model)
        {
            try
            {
                _repo.Add(model);

                if(await _repo.SaveChangesAsync())
                {
                    return Created($"/api/cliente/{model.Id}", model);
                }
              

            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de Dados Falhou - {ex.Message} - HttpPost");
            }

            return BadRequest();
        }

        [HttpPut("{ClienteId}")]
        public async Task<IActionResult> Put(int ClienteId, Cliente model)
        {
            try
            {
                var cliente = await _repo.GetClienteByIdAsync(ClienteId);

                if(cliente == null) return NotFound();

                _repo.Update(model);

                if(await _repo.SaveChangesAsync())
                {
                    return Created($"/api/cliente/{model.Id}", model);
                }
              

            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de Dados Falhou - {ex.Message} - HttpPost");
            }

            return BadRequest();
        }

        [HttpDelete("{ClienteId}")] 
        public async Task<IActionResult> Delete(int ClienteId)
        {
            try
            {
                var cliente = await _repo.GetClienteByIdAsync(ClienteId);

                if(cliente == null) return NotFound();
                
                _repo.Delete(cliente);

                if(await _repo.SaveChangesAsync())
                {
                    return Ok();
                }

            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de Dados Falhou - {ex.Message} - HttpPost");
            }

            return BadRequest();
        }
    }
}
