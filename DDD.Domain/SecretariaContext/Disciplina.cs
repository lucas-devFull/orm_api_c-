using System.ComponentModel.DataAnnotations.Schema;

namespace DDD.Domain.SecretariaContext
{
    public class Disciplina
    {
        public int DisciplinaId { get; set; }
        public required string Nome { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }
        public bool Disponivel { get; set; }
        public bool Ead { get; set; }
        public IList<Aluno>? Alunos { get; set; }

    }
}
