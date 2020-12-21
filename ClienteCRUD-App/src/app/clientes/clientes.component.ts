import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Cliente } from '../_models/Cliente';
import { ClienteService } from '../_services/cliente.service';

@Component({
  selector: 'app-clientes',
  templateUrl: './clientes.component.html',
  styleUrls: ['./clientes.component.css']
})
export class ClientesComponent implements OnInit {

  titulo = 'Clientes';

  clientesFiltrados: Cliente[];
  clientes: Cliente[];
  cliente: Cliente;
  modoSalvar = 'post';
  registerForm: FormGroup;
  bodyDeletarCliente = '';
  pag  = 1;
  contador = 5;
  filtroListaX: string;
  submitted = false;

  constructor(
    private clienteService: ClienteService
  , private fb: FormBuilder
  , private toastr: ToastrService
  ) {
  }

  get filtroLista(): string {
    return this.filtroListaX;
  }

  set filtroLista(value: string) {
    this.filtroListaX = value;
    this.clientesFiltrados = this.filtroLista ? this.filtrarClientes(this.filtroLista) : this.clientes;
  }

  filtrarClientes(filtrarPor: string): Cliente[] {
    filtrarPor =  filtrarPor.toLocaleLowerCase();
    return this.clientes.filter(
      cliente => cliente.nome.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  ngOnInit(): void {
    this.validation();
    this.getClientes();
  }

  validation(): void {
    this.registerForm = this.fb.group({
      id: [],
      nome: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(150)]],
      telefone: ['', Validators.required],
      endereco: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  // convenience getter for easy access to form fields
  get f(): any { return this.registerForm.controls; }

  novoCliente(template: any): void {
    this.modoSalvar = 'post';
    this.openModal(template);
  }

  editarCliente(cliente: Cliente, template: any): void {
    this.modoSalvar = 'put';
    this.openModal(template);
    this.cliente = Object.assign({}, cliente);
    this.registerForm.patchValue(this.cliente);
  }

  deletarCliente(cliente: Cliente, template: any): void {
    this.openModal(template);
    this.cliente = cliente;
    this.bodyDeletarCliente = `Tem certeza que deseja excluir o Cliente: ${cliente.nome}, Código: ${cliente.id}`;
  }

  openModal(template: any): void {
    this.registerForm.reset();
    this.submitted = false;
    template.show();
  }

  getClientes(): void {
    this.clienteService.getAllCliente().subscribe(
      (pax: Cliente[]) => {
      // console.log(pax);
      this.clientes = pax;
      this.clientesFiltrados = this.clientes;
    }, error => {
      this.toastr.error(`Erro ao tentar carregar eventos: ${error}!`);
    });
  }

  salvarAlteracao(template: any): void {

    this.submitted = true;

    // stop here if form is invalid
    if (this.registerForm.invalid) {
        return;
    }

    if (this.registerForm.valid) {
      if (this.modoSalvar === 'post') {

        this.cliente = Object.assign({}, this.registerForm.value);
        // console.log(this.cliente);

        this.clienteService.postCliente(this.cliente).subscribe(
          () => {
            // console.log(novoCliente);
            template.hide();
            this.getClientes();
            this.toastr.success('Inserido com Sucesso!', 'Cliente');
          }, error => {
            this.toastr.error('Erro ao inserir!', 'Cliente');
          }
        );

      } else {

        this.cliente = Object.assign({id: this.cliente.id}, this.registerForm.value);

        this.clienteService.putCliente(this.cliente).subscribe(
          () => {
            template.hide();
            this.getClientes();
            this.toastr.success('Alterado com Sucesso!', 'Cliente');
          }, error => {
            this.toastr.error('Erro ao alterar!', 'Cliente');
          }
        );
      }
    }
  }

  onSubmit(): boolean {
    this.submitted = true;

    // stop here if form is invalid
    if (this.registerForm.invalid) {
        return false;
    }

    return true;

    // display form values on success
    // alert('SUCCESS!! :-)\n\n' + JSON.stringify(this.registerForm.value, null, 4));
}

  confirmeDelete(template: any): void {
    this.clienteService.deleteCliente(this.cliente.id).subscribe(
      () => {
          template.hide();
          this.getClientes();
          this.toastr.success('Deletado com Sucesso!', 'Cliente');
        }, error => {
          this.toastr.error('Erro ao deletar!', 'Cliente');
          console.log(error);
        }
    );
  }

  pageChanged(event: any): void {
    this.pag = event;
  }


}
