using CadastroUsuarios.Data;
using CadastroUsuarios.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroUsuarios.Unidade_de_Trabalho
{
    public class UnitOfWork:IUnitOfWork
    {
        private EmpresaDbContext _empresaDbContext;
        public UnitOfWork(EmpresaDbContext empresaDbContext)
        {
            this._empresaDbContext = empresaDbContext;
        }
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
    }
}   