using DDD.Domain.SecretariaContext;
using DDD.Infra.SqlServer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDD.Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisciplinaController : ControllerBase
    {
        readonly IDisciplinaRepository _disciplinaRepository;

        public DisciplinaController(IDisciplinaRepository disciplinaRepository)
        {
            _disciplinaRepository = disciplinaRepository;
        }

        [HttpGet]
        public ActionResult<List<Disciplina>> Get()
        {
            return Ok(_disciplinaRepository.GetDisciplinas());
        }

        [HttpGet("{id}")]
        public ActionResult<Disciplina> GetById(int id)
        {
            return Ok(_disciplinaRepository.GetDisciplinaById(id));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Disciplina> CreateDisciplina(Disciplina disciplina)
        {
            if (disciplina.Nome.Length < 3 || disciplina.Nome.Length > 30)
            {
                return BadRequest("Nome deve ser maior que 3 e menor que 30");
            }

            _disciplinaRepository.InsertDisciplina(disciplina);
            return CreatedAtAction(nameof(GetById), new { id = disciplina.DisciplinaId }, disciplina);
        }

        [HttpPut]
        public ActionResult Put([FromBody] Disciplina disciplina)
        {
            try
            {
                if (disciplina == null)
                {
                    return NotFound();
                }

                _disciplinaRepository.UpdateDisciplina(disciplina);
                return Ok("Disciplina atualizada com sucesso");

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete()]
        public ActionResult Delete([FromBody] Disciplina disciplina)
        {
            try
            {
                if (disciplina == null)
                {
                    return NotFound();
                }

                _disciplinaRepository.DeleteDisciplina(disciplina);
                return Ok("Disciplina deletada com sucesso");

            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
