using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroUsuarios.Data;
using CadastroUsuarios.Data.Models;
using CadastroUsuarios.Unidade_de_Trabalho;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        [HttpPost("[action]/{pj}")]
        public void InserirPessoa([FromRoute] bool pj, [FromBody] object obj)
        {
            if (pj == true)
            {
                var pessoaJurdica = JsonConvert.DeserializeObject<PessoaJuridica>(obj.ToString());
                _unitOfWork.InserirPessoa(pessoaJurdica);
            }
            else
            {            
                var pessoaFisica = JsonConvert.DeserializeObject<PessoaFisica>(obj.ToString());
                _unitOfWork.InserirPessoa(pessoaFisica);
            }
        }
    }
}