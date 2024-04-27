using DDD.Domain.SecretariaContext;
using DDD.Infra.SqlServer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDD.Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatriculaController : ControllerBase
    {
        readonly IMatriculaRepository _matriculaRepository;

        public MatriculaController(IMatriculaRepository matriculaRepository)
        {
            _matriculaRepository = matriculaRepository;
        }

        [HttpGet]
        public ActionResult<List<Matricula>> Get()
        {
            return Ok(_matriculaRepository.GetMatriculas());
        }

        [HttpGet("{id}")]
        public ActionResult<Matricula> GetById(int id)
        {
            return Ok(_matriculaRepository.GetMatriculaById(id));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Matricula> CreateMatricula(int idAluno, int idDisciplina)
        {
            if (idAluno > 0)
            {
                return BadRequest("Aluno invalido");
            }

            if (idDisciplina > 0)
            {
                return BadRequest("Disciplina invalida");
            }

            Matricula matriculaIdSaved = _matriculaRepository.InsertMatricula(idAluno, idDisciplina);
            return CreatedAtAction(nameof(matriculaIdSaved), new { id = matriculaIdSaved.MatriculaId }, matriculaIdSaved);
        }

        [HttpPut]
        public ActionResult Put([FromBody] Matricula matricula)
        {
            try
            {
                if (matricula == null)
                {
                    return NotFound();
                }

                _matriculaRepository.UpdateMatricula(matricula);
                return Ok("Matricula atualizada com sucesso");

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete()]
        public ActionResult Delete([FromBody] Matricula matricula)
        {
            try
            {
                if (matricula == null)
                {
                    return NotFound();
                }

                _matriculaRepository.DeleteMatricula(matricula);
                return Ok("Matricula deletada com sucesso");

            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
