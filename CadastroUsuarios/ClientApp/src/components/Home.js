import React, { Component } from 'react';

export class Home extends Component {
    constructor(props) {
        super(props);
        this.onchangeCnpj = this.onchangeCnpj.bind(this);
    this.state = {
        nome: '',
        idade: null,
        email: '',
        endereco: '',
        cpfcnpj: '',
        validaCnpj:false
    }
    }
    onchangeCnpj(e) {
        let valor = e.target.value == 'true';
        this.setState({
            nome: '',
            idade: null,
            email: '',
            endereco: '',
            cpfcnpj: '',
            validaCnpj: valor
        });
        this.render();
    }
    static displayName = Home.name;
    render() {
        let dados = null;
        if (this.state.validaCnpj) {
            dados = <div> <label>CNPJ</label>
                <input className="form-control" maxLength="14" type="number" value={this.state.cpfcnpj} />
            </div>
        } else {
            dados = <div> <label>CPF</label>
                <input className="form-control" maxLength="11" type="number" value={this.state.cpfcnpj} />
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
                <input className="form-control" value={this.state.nome} />
                <label>Idade</label>
                <input className="form-control" value={this.state.idade} />
                <label>Email</label>
                <input className="form-control" value={this.state.email} />
                <label>Endereco</label>
                <input className="form-control" value={this.state.endereco} />
                {dados}
            </div>
        );
    }
}
