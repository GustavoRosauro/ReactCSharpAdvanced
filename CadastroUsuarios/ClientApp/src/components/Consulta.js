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
    render() {
        return (
            <div>
                <table className="table table-striped">
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
                            <tr key={usuario.id}>
                                <td>{usuario.nome}</td>
                                <td>{usuario.idade}</td>
                                <td>{usuario.email}</td>
                                <td>{usuario.endereco}</td>
                                <td>{usuario.cpf}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div >
        );
    }
}
