using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DESAFIO.Models
{
    public class notas
    {   [Key]
        public int codDisciplina { get; set; }
        public string nomeDisciplina {get; set;}
        public float av1  {get; set;}
        public float av2 {get; set;}
        public float av3 {get; set;}
        public float  media  {get; set;}
               
        [ForeignKey("estudante")]
                            
        public int matricula {get; set;}
        public virtual estudante estudante {get; set;}

        internal static object Distinct(string v)
        {
            throw new NotImplementedException();
        }
    }
}