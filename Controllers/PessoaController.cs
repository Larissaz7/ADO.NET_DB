using Microsoft.AspNetCore.Mvc;
using projetoFinalBDD.Data;
using ProjetoFinalBDD.Models;

namespace ProjetoFinalBDD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly PessoaDataAccess _pessoaDataAccess;

        public PessoaController(PessoaDataAccess pessoaDataAccess)
        {
            _pessoaDataAccess = pessoaDataAccess;
        }

        [HttpPost]
        public IActionResult AddPessoa(Pessoa pessoa)
        {
            _pessoaDataAccess.AddPessoa(pessoa);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdatePessoa(Pessoa pessoa)
        {
            _pessoaDataAccess.UpdatePessoa(pessoa);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePessoa(int id)
        {
            _pessoaDataAccess.DeletePessoa(id);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllPessoas()
        {
            var pessoas = _pessoaDataAccess.GetAllPessoas();
            return Ok(pessoas);
        }

        [HttpGet("{id}")]
        public IActionResult GetPessoaById(int id)
        {
            var pessoa = _pessoaDataAccess.GetPessoaById(id);
            if (pessoa == null) return NotFound();
            return Ok(pessoa);
        }

        [HttpGet("search")]
        public IActionResult GetPessoasByCodigoENome([FromQuery] int id, [FromQuery] string nome)
        {
            var pessoas = _pessoaDataAccess.GetPessoasByCodigoENome(id, nome);
            return Ok(pessoas);
        }
    }
}
