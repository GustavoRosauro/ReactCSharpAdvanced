using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroUsuarios.Data.Models
{
    public class PessoaFisica:Pessoa
    {
        public PessoaFisica(string nome, int idade, string email,string endereco)
            :base(nome,idade,email,endereco)
        { }
        [MaxLength(11)]
        public string CPF { get; set; }
    }
}
