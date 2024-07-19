using Contatos.Application.Dtos;
using Contatos.Application.Interface;
using Contatos.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Contatos.API.Controllers
{
    public class ContatoController : ControllerBase
    {
        private readonly IContatoService _contatoService;
        public ContatoController(IContatoService contatoService)
        {
            _contatoService = contatoService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ContatoRequestDto entrada)
        {
           var result = _contatoService.CreateContato(entrada);

            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ContatoUpdateDto entrada)
        {
            var result = _contatoService.UpdateContato(entrada);

            return Ok(result);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
           var result =  _contatoService.GetAll();

            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result =  _contatoService.Delete(Id);

            return Ok(result);
        }
        [HttpGet("GetById")]
        public IActionResult GetById(int Id)
        {
            var result = _contatoService.GetById(Id);

            return Ok(result);
        }
        [HttpPut("Inativar")]
        public IActionResult DesativarContato(int Id)
        {
            var result = _contatoService.DesativarContato(Id);

            return Ok(result);
        }
    }
}
