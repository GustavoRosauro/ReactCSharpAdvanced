using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroUsuarios.Data.Models
{
    public abstract class Pessoa
    {
        public Pessoa(string nome,int idade, string email, string endereco)
        {
            this.Nome = nome;
            this.Idade = idade;
            this.Email = email;
            this.Endereco = endereco;
        }
        [Key]
        public int Id { get; set; }
        [MaxLength(200)]
        public string Nome { get; set; }
        public int Idade { get; set; }
        [MaxLength(400)]
        public string Email { get; set; }
        [MaxLength(400)]
        public string Endereco { get; set; }
    }
}
