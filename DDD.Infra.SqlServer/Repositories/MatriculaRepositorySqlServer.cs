using DDD.Domain.SecretariaContext;
using DDD.Infra.SqlServer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Infra.SqlServer.Repositories
{
    public class MatriculaRepositorySqlServer : IMatriculaRepository
    {
        private readonly SqlServerContext _context;

        public MatriculaRepositorySqlServer(SqlServerContext sqlServerContext)
        {
            _context = sqlServerContext;
        }

        public Matricula GetMatriculaById(int id)
        {
            return _context.Matriculas.Find(id);
        }

        public List<Matricula> GetMatriculas()
        {
            return _context.Matriculas.ToList();
        }

        public Matricula InsertMatricula(int idAluno, int idDisciplina)
        {
            var aluno = _context.Alunos .First(i => i.UserId == idAluno);
            var disciplina = _context.Disciplinas.First(i => i.DisciplinaId == idDisciplina);
 
            var matricula = new Matricula
            {
                Aluno = aluno,
                Disciplina = disciplina
            };

            try
            {
                _context.Matriculas.Add(matricula);
                _context.SaveChanges();//EF
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                //log
                throw;
            }

            return matricula;
        }

        public void UpdateMatricula(Matricula matricula)
        {
            try
            {
                _context.Entry(matricula).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void DeleteMatricula(Matricula matricula)
        {
            try
            {
                _context.Set<Matricula>().Remove(matricula);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                var erro = ex.Message;//log
                throw;
            }
        }
    }
}
