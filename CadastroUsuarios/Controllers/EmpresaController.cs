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
    /// <summary>
    /// Api Net Core Aonde será feita à Inserção, Alteração e Remoção da PessoaFisica e PessoaJuridica
    /// Author:Gustavo Rosauro
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        /// <summary>
        /// No contructor é aonde iremos receber a nossa classe que possue comunicação com o banco de dados
        /// por meio de injeção de dependencia essa interface já recebe os parametros da classe quando inicia a aplicação
        /// </summary>
        private IUnitOfWork _unitOfWork;
        public EmpresaController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Verifica se é pessoa fisica ou juridica e faz a inserção
        /// </summary>
        /// <param name="pj"></param>
        /// <param name="obj"></param>
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
        /// <summary>
        /// Retorna todas as pessaos por meio de um union via linq no unitofwork
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<object> RetornaPessoas()
        {
            return _unitOfWork.RetornaPessoas();
        }
        /// <summary>
        /// Nosso CPF é unico então é por meio dele que faremos a remoção das pessoas 
        /// </summary>
        /// <param name="cpf"></param>
        [HttpDelete("{cpf}")]
        public void RemoverPessoa(string cpf)
        {
            object pessoa = new object();
            if (cpf.Length < 12) {
                pessoa = new PessoaFisica("", 0, "", "") { CPF = cpf };
            }
            else {
                pessoa = new PessoaJuridica("", 0, "", "") { CNPJ = cpf };
            }
            _unitOfWork.RemoverPessoa(pessoa);
        }
        /// <summary>
        /// Para alterar a pessoa validamos se é pessoa fisica ou juridica 
        /// Logo em seguida mandamos para a nossa unidade de trabalho aonde ira alterar a nossa base
        /// </summary>
        /// <param name="cpf"></param>
        /// <param name="obj"></param>
        [HttpPut("{cpf}")]
        public void AlterarPessoa(string cpf, [FromBody] object obj)
        {
            var pessoa = JsonConvert.DeserializeObject<PessoaFisica>(obj.ToString());
            if (cpf.Length <= 11)
            {
                _unitOfWork.AlterarPessoa(pessoa);
            }
            else
            {
                PessoaJuridica pessoaJuridica = new PessoaJuridica(pessoa.Nome, pessoa.Idade,
                                                    pessoa.Email, pessoa.Endereco)
                {
                    CNPJ = pessoa.CPF
                };
                _unitOfWork.AlterarPessoa(pessoaJuridica);
            }
        }
    }
}