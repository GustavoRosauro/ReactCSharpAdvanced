import React, { Component } from 'react';

export class Consulta extends Component {

    constructor(props) {
        super(props);
        this.state = { usuarios: [] };
        this.retornaUsuarios();
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
    render() {
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
                            <tr className={usuario.cpf.length > 11?'pj':null} key={usuario.id}>
                                    <td>{usuario.nome}</td>
                                    <td>{usuario.idade}</td>
                                    <td>{usuario.email}</td>
                                    <td>{usuario.endereco}</td>
                                <td>{usuario.cpf}</td>
                                <td className='btn btn-danger' onClick={this.removeUsuario.bind(this, usuario.cpf)}>Deletar</td>
                                </tr>
                            )}
                        </tbody>
                </table>
            </div >
        );
    }
}