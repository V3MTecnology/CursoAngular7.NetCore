using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        public readonly IProAgilRepository _repo ;

        //dependencie inject 
        public EventoController(IProAgilRepository repo)
        {
            _repo = repo;
        }

          // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {           
            try
            {
                 var results = await  _repo.GetAllEventoAsync(true);//Treu para incluir os palestrantes 

                return Ok(results); //StatusCodes = 200
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados Falhou");                
            }
        }

        [HttpGet("{EventoId}")]
        public async Task<IActionResult> Get(int EventoId)
        {           
            try
            {
                 var results = await  _repo.GetEventoAsyncById(EventoId, true);//Treu para incluir os palestrantes 

                return Ok(results); //StatusCodes = 200
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados Falhou");                
            }
        }

         [HttpGet("getByTema/{tema}")]
        public async Task<IActionResult> Get(string tema)
        {           
            try
            {
                 var results = await  _repo.GetAllEventoAsyncByTema(tema, true);//Treu para incluir os palestrantes 

                return Ok(results); //StatusCodes = 200
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados Falhou");                
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            //cadastra o evento 

            try
            {
                _repo.Add(model);

                if (await _repo.SaveChangesAsync())
                {
                    //Chamar o método get   [HttpGet("{EventoId}")]
                    return Created($"/api/evento/{model.Id}" ,model);
                }
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados Falhou");                
            }

            return BadRequest();            
        }

         [HttpPut("{EventoId}")]
        public async Task<IActionResult> Put(int EventoId, Evento model)
        {
            //Atualiza o evento 
            //Explicação sobre 52. Tracker e No Tracker (EF Core)
            try
            {
                var evento = await _repo.GetEventoAsyncById(EventoId, false);
                if (evento == null) return NotFound();
                                
                _repo.Add(model);

                if (await _repo.SaveChangesAsync())
                {
                    //Chamar o método get   [HttpGet("{EventoId}")]
                    return Created($"/api/evento/{model.Id}" ,model);
                }
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados Falhou");                
            }

            return BadRequest();            
        }

        [HttpDelete("{EventoId}")]
        public async Task<IActionResult> Delete(int EventoId)
        {
            try
            {
                //Explicação sobre 52. Tracker e No Tracker (EF Core)
                var evento = await _repo.GetEventoAsyncById(EventoId, false);
                if (evento == null) return NotFound();
                                
                _repo.Delete(evento);

                if (await _repo.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados Falhou");                
            }

            return BadRequest();            
        }
        
    }
}