using CadastroUsuarios.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroUsuarios.Data
{
    public class EmpresaDbContext : DbContext
    {
        public DbSet<PessoaFisica> PessoaFisica { get; set; }
        public DbSet<PessoaJuridica> PessoaJuridica { get; set; }
        public EmpresaDbContext(DbContextOptions<EmpresaDbContext> options)
            : base(options)
        { }
    }
}
