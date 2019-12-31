using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroUsuarios.Data.Models
{
    public class PessoaJuridica:Pessoa
    {
        public PessoaJuridica(string nome, int idade, string email, string endereco)
            :base(nome, idade, email, endereco)
        { }
        [MaxLength(14)]
        public string CNPJ { get; set; }
    }
}
