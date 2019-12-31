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
    }
}
