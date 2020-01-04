import React, { Component } from 'react';
import { Modal, Button } from 'react-bootstrap';
export class Consulta extends Component {

    constructor(props) {
        super(props);
        this.state = {
            usuarios: [], show: false, item: {} };
        this.retornaUsuarios();
        this.onChangeNome = this.onChangeNome.bind(this);
        this.onChangeIdade = this.onChangeIdade.bind(this);
        this.onChangeEmail = this.onChangeEmail.bind(this);
        this.onChangeEndereco = this.onChangeEndereco.bind(this);
        this.onChangeCPF = this.onChangeCPF.bind(this);
    }

    retornaUsuarios() {
        fetch('api/Empresa')
            .then(resp => resp.json())
            .then(data => {
                this.setState({ usuarios: data });
            })
    }
    removeUsuario(cpf) {
        fetch(`api/Empresa/${cpf}`, {
            method: 'DELETE'
        })
            .then(data => {
                this.retornaUsuarios();
                this.render();
            })
    }
    onChangeNome(e) {
        this.setState({
            item:{
                nome: e.target.value,
                idade: this.state.item.idade,
                email: this.state.item.email,
                endereco: this.state.item.endereco,
                cpf: this.state.item.cpf
        }
        })
    }
    onChangeIdade(e) {
        this.setState({
            item: {
                nome: this.state.item.nome,
                idade: e.target.value,
                email: this.state.item.email,
                endereco: this.state.item.endereco,
                cpf: this.state.item.cpf
            }
        })
    } onChangeEmail(e) {
        this.setState({
            item: {
                nome: this.state.nome,
                idade: this.state.item.idade,
                email: e.target.value,
                endereco: this.state.item.endereco,
                cpf: this.state.item.cpf
            }
        })
    } onChangeEndereco(e) {
        this.setState({
            item: {
                nome: this.state.nome,
                idade: this.state.item.idade,
                email: this.state.item.email,
                endereco: e.target.value,
                cpf: this.state.item.cpf
            }
        })
    } onChangeCPF(e) {
        this.setState({
            item: {
                nome: this.state.nome,
                idade: this.state.item.idade,
                email: this.state.item.email,
                endereco: this.state.item.endereco,
                cpf: e.target.value
            }
        })
    }
    render() {
        const handleClose = () => {
            this.setState({ show: false })
        };
        const AlterarPessoa = (item) => {
            console.log(item);
            fetch(`api/Empresa/${item.cpf}`, {
                method: 'PUT',
                body: JSON.stringify(item),
                headers: { 'Content-Type': 'application/json' }
            })
                .then(data => {
                    this.retornaUsuarios();
                    this.setState({show:false});
                });
        }
        const handleShow = (item) => {
            this.setState({ show: true,item:item })
        };
        return (
            <div>
                <table className="table">
                    <thead>
                        <tr>
                            <th>Nome</th>
                            <th>Idade</th>
                            <th>Email</th>
                            <th>Endereco</th>
                            <th>CPF/CPNJ</th>
                        </tr>
                    </thead>
                        <tbody>
                        {this.state.usuarios.map(usuario =>
                            <tr className={usuario.cpf.length > 11?'pj':null} key={usuario.cpf}>
                                    <td>{usuario.nome}</td>
                                    <td>{usuario.idade}</td>
                                    <td>{usuario.email}</td>
                                    <td>{usuario.endereco}</td>
                                <td>{usuario.cpf}</td>
                                <td className='btn btn-warning' onClick={handleShow.bind(this, usuario)}>Editar</td>
                                <td className='btn btn-danger' onClick={this.removeUsuario.bind(this, usuario.cpf)}>Deletar</td>
                                </tr>
                            )}
                        </tbody>
                </table>
                <Modal show={this.state.show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Editar</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <label>Nome</label>
                        <input className='form-control' value={this.state.item.nome} onChange={this.onChangeNome} />
                        <label>Idade</label>
                        <input className='form-control' value={this.state.item.idade} onChange={this.onChangeIdade} />
                        <label>Email</label>
                        <input className='form-control' value={this.state.item.email} onChange={this.onChangeEmail} />
                        <label>Endereço</label>
                        <input className='form-control' value={this.state.item.endereco} onChange={this.onChangeEndereco} />
                        <label>CPF</label>
                        <input className='form-control' value={this.state.item.cpf} onChange={this.onChangeCPF} />
                        <br />
                      </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
                        <Button variant="primary" onClick={AlterarPessoa.bind(this, this.state.item)}>
            Save Changes
          </Button>
        </Modal.Footer>
      </Modal>
            </div >

        );
    }
}