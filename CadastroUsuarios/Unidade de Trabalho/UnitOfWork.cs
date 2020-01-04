using CadastroUsuarios.Data;
using CadastroUsuarios.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroUsuarios.Unidade_de_Trabalho
{
    /// <summary>
    /// Herda nossa interface que já é populada quando quando inicia nossa aplicação
    /// </summary>
    public class UnitOfWork:IUnitOfWork
    {
        private EmpresaDbContext _empresaDbContext;
        public UnitOfWork(EmpresaDbContext empresaDbContext)
        {
            this._empresaDbContext = empresaDbContext;
        }
        /// <summary>
        /// Inseri Pessoa no banco de dados
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void InserirPessoa<T>(T obj)
        {
            var props = obj.GetType().GetProperties();
            bool pj = props.Any(x => x.Name == "CNPJ");
            if (pj)
                _empresaDbContext.PessoaJuridica.Add(obj as PessoaJuridica);
            else
                _empresaDbContext.PessoaFisica.Add(obj as PessoaFisica);
            _empresaDbContext.SaveChanges();
        }
        /// <summary>
        /// Retorna todas as pessoas fazendo um union entre pessoa fisica e juridica
        /// </summary>
        /// <returns></returns>
        public IEnumerable<object> RetornaPessoas()
        {
            var lista = (from pj in _empresaDbContext.PessoaJuridica.ToList()
                         select new {
                             Nome = pj.Nome,
                             Idade= pj.Idade,
                             Email = pj.Email,
                             Endereco = pj.Endereco,
                             CPF = pj.CNPJ
                         }).Concat
                        (from pf in  _empresaDbContext.PessoaFisica.ToList()
                         select new {
                             Nome =  pf.Nome,
                             Idade = pf.Idade,
                             Email = pf.Email,
                             Endereco = pf.Endereco,
                             CPF = pf.CPF
                         }).OrderBy(x => x.CPF.Length);
            return lista;
        }
        /// <summary>
        /// Remove a pessoa da base de acordo com o cpf ou cpj
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void RemoverPessoa<T>(T obj)
        {
            var props = obj.GetType().GetProperties();
            bool pj = props.Any(x => x.Name == "CNPJ");
            if (pj)
            {
                var pessoaJuridica = obj as PessoaJuridica;
                pessoaJuridica = _empresaDbContext.PessoaJuridica.Where(x => x.CNPJ == pessoaJuridica.CNPJ).FirstOrDefault();
                _empresaDbContext.PessoaJuridica.Remove(pessoaJuridica);
                _empresaDbContext.SaveChanges();
            }
            else 
            {
                var pessoaFisica = obj as PessoaFisica;
                pessoaFisica = _empresaDbContext.PessoaFisica.Where(x => x.CPF == pessoaFisica.CPF).FirstOrDefault();
                _empresaDbContext.PessoaFisica.Remove(pessoaFisica);
                _empresaDbContext.SaveChanges();
            }
        }
        /// <summary>
        /// Altera pessoa da base de acordo com cpf ou cnpj
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void AlterarPessoa<T>(T obj)
        {
            var props = obj.GetType().GetProperties();
            bool pj = props.Any(x => x.Name == "CNPJ");
            if (pj)
            {
                var pessoaJuridica = obj as PessoaJuridica;
                var oldPerson = _empresaDbContext.PessoaJuridica.Where(x => x.CNPJ == pessoaJuridica.CNPJ).First();
                oldPerson.Nome = pessoaJuridica.Nome;
                oldPerson.Idade = pessoaJuridica.Idade;
                oldPerson.Email = pessoaJuridica.Email;
                oldPerson.Endereco = pessoaJuridica.Endereco;
                oldPerson.CNPJ = pessoaJuridica.CNPJ;
                _empresaDbContext.SaveChanges();
            }
            else
            {
                var pessoaFisica = obj as PessoaFisica;
                var oldPerson = _empresaDbContext.PessoaFisica.Where(x => x.CPF == pessoaFisica.CPF).First();
                oldPerson.Nome = pessoaFisica.Nome;
                oldPerson.Idade = pessoaFisica.Idade;
                oldPerson.Email = pessoaFisica.Email;
                oldPerson.Endereco = pessoaFisica.Endereco;
                oldPerson.CPF = pessoaFisica.CPF;
                _empresaDbContext.SaveChanges();
            }
        }
    }
}   