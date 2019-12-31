using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroUsuarios.Data;
using CadastroUsuarios.Data.Models;
using CadastroUsuarios.Unidade_de_Trabalho;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CadastroUsuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        public EmpresaController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        [HttpPost("[action]/pj")]
        public void InserirPessoa<T>(bool pj, [FromBody] T obj)
        {
            if (pj == true)
            {
                var pessoaJurdica = obj as PessoaJuridica;
                _unitOfWork.InserirPessoa(obj);
            }
            else
            {
                var pessoaFisica = obj as PessoaFisica;
                _unitOfWork.InserirPessoa(obj);
            }
        }
    }
}