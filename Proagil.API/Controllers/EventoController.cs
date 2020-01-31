using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proagil.API.Dtos;
using ProAgil.Domain;
using ProAgil.Repository;

namespace Proagil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController: ControllerBase
    {
        private readonly IProAgilRepository _repo;

        public readonly IMapper Mapper;

        public EventoController(IProAgilRepository repo, IMapper mapper)
        {
            _repo = repo;
            Mapper = mapper;
        }
    

    // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await  _repo.GetAllEventosAsync(true);
                var results = Mapper.Map<IEnumerable<EventoDto>>(eventos);
                 return Ok(results);
            }
            catch (System.Exception)
            {
                
               return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados falhou");
            }
           
        }

        [HttpPost("upload")]
        public async Task<IActionResult>upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources","Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(),folderName);

                if(file.Length >0)
                {

                    var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                    var fullPath = Path.Combine(pathToSave,filename.Replace("\""," ").Trim());
                    
                    using(var strem = new FileStream(fullPath, FileMode.Create));
                    
                }

                 return Ok();

            }
            
            catch (System.Exception)
            {
                
               return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados falhou");
            }
           
        }

        [HttpGet("{EventoId}")]
        public async Task<IActionResult> Get(int EventoId)
        {
            try
            {
                var eventos = await  _repo.GetAllEventosAsyncById(EventoId, true);
                var results = Mapper.Map<IEnumerable<EventoDto>>(eventos);
                 return Ok(results);
            }
            catch (System.Exception)
            {
                
               return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados falhou 10");
            }
           
        }

        [HttpGet("getByTema/{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                var eventos = await  _repo.GetAllEventosAsyncByTema(tema,true);
                var results = Mapper.Map<IEnumerable<EventoDto>>(eventos);
                 return Ok(results);
            }
            catch (System.Exception)
            { 
               return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados falhou");
            }
           
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDto model)
        {
            try
            {
                var evento = Mapper.Map<EventoDto>(model);
                _repo.Add(evento);
                if(await _repo.SaveChangesAsync())
                {
                    return Created($"/api/evento/{model.Id}",Mapper.Map<EventoDto>(evento));
                }
                 
            }
            catch (System.Exception)
            { 
               return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados falhou");
            }

           return BadRequest();
        }
        
        [HttpPut("{EventoId}")]
        public async Task<IActionResult> Put(int EventoId, EventoDto model)
        {
            try
            {
                var evento = await _repo.GetAllEventosAsyncById(EventoId, false);
                if(evento == null)return NotFound();
                Mapper.Map(model, evento);

                _repo.Update(evento);

                if(await _repo.SaveChangesAsync()){
                    return Created($"/api/evento/{model.Id}",Mapper.Map<EventoDto>(evento));
                }
                 
            }
            catch (System.Exception)
            { 
               return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados falhou");
            }
            
           return BadRequest();
        }

        
        [HttpDelete("{EventoId}")]
        public async Task<IActionResult> Delete(int EventoId)
        {
            try
            {
                var evento = await _repo.GetAllEventosAsyncById(EventoId,false);
                if(evento == null)return NotFound();

                _repo.Delete(evento);

                if(await _repo.SaveChangesAsync()){
                    return Ok();
                }
                 
            }
            catch (System.Exception)
            { 
               return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados falhou");
            }
            
           return BadRequest();
        }
    }
}