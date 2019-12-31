import React, { Component } from 'react';

export class Home extends Component {
    constructor(props) {
        super(props);
        this.onchangeCnpj = this.onchangeCnpj.bind(this);
        this.onchangeNome = this.onchangeNome.bind(this);
        this.onchangeCpfCnpj = this.onchangeCpfCnpj.bind(this);
        this.valueCpfCnpj = this.valueCpfCnpj.bind(this);
        this.onchangeIdade = this.onchangeIdade.bind(this);
        this.onchangeEmail = this.onchangeEmail.bind(this);
        this.onchangeEndereco = this.onchangeEndereco.bind(this);
        this.enviar = this.enviar.bind(this);
    this.state = {
        contaChars: '',
        validaCnpj:false
        }
        this.pessoa = {
            nome: '',
            idade: null,
            email: '',
            endereco: '',
            cpfcnpj: '',
        }
    }
    onchangeCpfCnpj(e) {
        if (this.validaChars(e.target.value)) {
            this.pessoa.cpfcnpj = e.target.value;
            this.valueCpfCnpj(e);
        }
    }
    onchangeNome(e) {
        this.pessoa.nome = e.target.value;
    }
    valueCpfCnpj(e) {
        this.setState({
            validaCnpj: this.state.validaCnpj,
            contaChars: e.target.value
        })
    }
    validaChars(value) {
        if (isNaN(value)) {
            return false
        }
        return true;
    }
    enviar() {
        let pessoa = { }
        if (this.state.validaCnpj) {
            pessoa.nome = this.pessoa.nome;
            pessoa.idade = this.pessoa.idade;
            pessoa.email = this.pessoa.email;
            pessoa.endereco = this.pessoa.endereco;
            pessoa.cnpj = this.pessoa.cpfcnpj;
        } else {
            pessoa.nome = this.pessoa.nome;
            pessoa.idade = this.pessoa.idade;
            pessoa.email = this.pessoa.email;
            pessoa.endereco = this.pessoa.endereco;
            pessoa.cpf = this.pessoa.cpfcnpj;
        }
        fetch(`api/Empresa/InserirPessoa/${this.state.validaCnpj}`, {
            method: 'POST',
            body: JSON.stringify(pessoa),
            headers: { 'Content-Type': 'application/json' }
        }).then(res => alert('ok'))
            .catch(err => console.log(err));
    }
    onchangeIdade(e) {
        this.pessoa.idade = e.target.value;
    }
    onchangeEmail(e) {
        this.pessoa.email = e.target.value;
    }
    onchangeEndereco(e) {
        this.pessoa.endereco = e.target.value;
    }
    onchangeCnpj(e) {
        let valor = e.target.value == 'true';
        this.setState({
            validaCnpj: valor
        });
        this.render();
    }
    static displayName = Home.name;
    render() {
        let dados = null;
        if (this.state.validaCnpj) {
            dados = <div> <label>CNPJ</label>
                <input className="form-control" maxLength="14" type="text" pattern="[0-9]*" onChange={this.onchangeCpfCnpj} value={this.state.contaChars} />
            </div>
        } else {
            dados = <div> <label>CPF</label>
                <input className="form-control" maxLength='11' type="text" pattern="[0-9]*" onChange={this.onchangeCpfCnpj} value={this.state.contaChars}/>
            </div>
        }
        return (
            <div>
                <h1>Tela de Cadastro</h1>
                <select className="form-control" onChange={this.onchangeCnpj} value={this.state.validaCnpj}>
                    <option value='false'>Pessoa Fisica</option>
                    <option value='true'>Pessoa Juridica</option>
                </select>
                <label>Nome</label>
                <input className="form-control" onChange={this.onchangeNome} />
                <label>Idade</label>
                <input className="form-control" onChange={this.onchangeIdade} />
                <label>Email</label>
                <input className="form-control" onChange={this.onchangeEmail} />
                <label>Endereco</label>
                <input className="form-control" onChange={this.onchangeEndereco} />
                {dados}
                <br />
                <button className="btn btn-success" onClick={this.enviar}>Salvar</button>
            </div>
        );
    }
}
