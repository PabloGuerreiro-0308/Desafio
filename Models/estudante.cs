using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DESAFIO.Models
{
    public class estudante
    {   [Key]
        public int matricula { get; set; }
        public string nome { get; set; }

        public virtual ICollection<notas> notas {get; set;}
    }
}