using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace Proagil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PalestranteController : ControllerBase
    {
        private readonly IProAgilRepository _repo;
            public PalestranteController(IProAgilRepository repo){
                _repo = repo;
            }

        // GET api/values
        [HttpGet("{Nome}")]
        public async Task<IActionResult> Get(string Nome)
        {
            try
            {
                var results = await  _repo.GetAllPalestrantesAsyncByName( Nome,true);
                 return Ok(results);
            }
            catch (System.Exception)
            {
                
               return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados falhou");
            }
           
        }

        [HttpGet("{PalestranteId}")]
        public async Task<IActionResult> Get(int PalestranteId)
        {
            try
            {
                var results = await  _repo.GetAllPalestrantesAsync( PalestranteId,true);
                 return Ok(results);
            }
            catch (System.Exception)
            {
                
               return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados falhou");
            }
           
        }

      

        [HttpPost]
        public async Task<IActionResult> Post(Palestrante model)
        {
            try
            {
                _repo.Add(model);
                if(await _repo.SaveChangesAsync()){
                    return Created($"/api/palestrante/{model.Id}",model);
                }
                 
            }
            catch (System.Exception)
            { 
               return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados falhou");
            }

           return BadRequest();
        }
        
        [HttpPut]
        public async Task<IActionResult> Put(int PalestranteId,Palestrante model)
        {
            try
            {
                var evento = await _repo.GetAllPalestrantesAsync(PalestranteId,false);
                if(evento == null)return NotFound();

                _repo.Update(model);

                if(await _repo.SaveChangesAsync()){
                    return Created($"/api/palestrante/{model.Id}",model);
                }
                 
            }
            catch (System.Exception)
            { 
               return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados falhou");
            }
            
           return BadRequest();
        }

        
        [HttpDelete]
        public async Task<IActionResult> Delete(int PalestranteId)
        {
            try
            {
                var evento = await _repo.GetAllPalestrantesAsync(PalestranteId,false);
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