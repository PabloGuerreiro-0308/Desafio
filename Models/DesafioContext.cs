using Microsoft.EntityFrameworkCore;

namespace DESAFIO.Models
{
    public class DesafioContext : DbContext 
    {
            public DesafioContext(DbContextOptions<DesafioContext> options): base(options){}
                public DbSet<estudante> estudantes {get; set;}
                public DbSet<notas> notas {get; set;}
    }

}
