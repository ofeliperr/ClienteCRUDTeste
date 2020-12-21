import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, observable } from 'rxjs';
import { Cliente } from '../_models/Cliente';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {

  baseURL = 'https://localhost:5001/api/cliente';

  constructor(private http: HttpClient) { }

  getAllCliente(): Observable<Cliente[]> {
    return this.http.get<Cliente[]>(this.baseURL);
  }

  // tslint:disable-next-line:typedef
  postCliente(cliente: Cliente){
    const cli = { nome : cliente.nome, telefone: cliente.telefone, endereco:  cliente.endereco, email: cliente.email};
    return this.http.post(this.baseURL, cli);
  }

  // tslint:disable-next-line:typedef
  putCliente(cliente: Cliente) {
    return this.http.put(`${this.baseURL}/${cliente.id}`, cliente);
  }

 // tslint:disable-next-line:typedef
  deleteCliente(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }
}
