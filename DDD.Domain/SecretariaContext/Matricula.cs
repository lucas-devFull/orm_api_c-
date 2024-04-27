using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.SecretariaContext
{
    public class Matricula
    {
        public int MatriculaId { get; set; }

        public required Aluno Aluno { get; set; }

        public required Disciplina Disciplina { get; set; }

        public DateTime Data { get; set; }

    }
}
