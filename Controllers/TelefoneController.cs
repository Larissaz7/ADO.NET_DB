using Microsoft.AspNetCore.Mvc;
using projetoFinalBDD.Data;
using ProjetoFinalBDD.Models;

namespace ProjetoFinalBDD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelefoneController : ControllerBase
    {
        private readonly TelefoneDataAccess _telefoneDataAccess;

        public TelefoneController(TelefoneDataAccess telefoneDataAccess)
        {
            _telefoneDataAccess = telefoneDataAccess;
        }

        [HttpPost]
        public IActionResult AddTelefone(Telefone telefone)
        {
            _telefoneDataAccess.AddTelefone(telefone);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateTelefone(Telefone telefone)
        {
            _telefoneDataAccess.UpdateTelefone(telefone);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTelefone(int id)
        {
            _telefoneDataAccess.DeleteTelefone(id);
            return Ok();
        }

        [HttpGet("byPessoa/{pessoaId}")]
        public IActionResult GetTelefonesByPessoaId(int pessoaId)
        {
            var telefones = _telefoneDataAccess.GetTelefonesByPessoaId(pessoaId);
            return Ok(telefones);
        }

        [HttpGet("search")]
        public IActionResult GetTelefoneByCodigoENumero([FromQuery] int id, [FromQuery] string numero)
        {
            var telefone = _telefoneDataAccess.GetTelefoneByCodigoENumero(id, numero);
            if (telefone == null) return NotFound();
            return Ok(telefone);
        }
    }
}
