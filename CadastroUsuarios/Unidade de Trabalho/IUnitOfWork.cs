using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroUsuarios.Unidade_de_Trabalho
{
    public interface IUnitOfWork
    {
        void InserirPessoa<T>(T obj);
    }
}
