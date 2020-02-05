import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventoService } from '../_services/evento.service';
import { Evento } from '../_modules/Evento';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { defineLocale, BsLocaleService, ptBrLocale } from 'ngx-bootstrap';
import { error } from 'util';
defineLocale('pt-br', ptBrLocale);



@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  filtroList: string;
  eventosFiltrados: Evento[];
  eventos: Evento[];
  evento: Evento;
  imagemAltura = 50;
  imagemMargem =  2;
  mostrarImagem = false;
  registerForm: FormGroup;
  decisao = 'put';
  bodyDeletarEvento = '';

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private fb: FormBuilder,
    private localeService: BsLocaleService
     ) {
       this.localeService.use('pt-br');
  }

  get filtroLista(): string {
    return this.filtroList;
  }
  set filtroLista(value: string) {
    this.filtroList = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  ngOnInit() {
    this.validation();
    this.getEventos();
  }

  openModal(template: any) {
  this.decisao = 'post';
  this.registerForm.reset();
  template.show();
  }

  editarModal(template: any, evento: Evento) {
    this.decisao = 'put';
    this.openModal(template);
    this.evento = evento;
    this.registerForm.patchValue(evento);
  }

  filtrarEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  validation() {
    this.registerForm = this.fb.group({
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local : ['', Validators.required],
      dataEvento : ['', Validators.required],
      qtdPessoas : ['', [Validators.required, Validators.max(120000)]],
      imagemUrl: ['', Validators.required],
      telefone : ['', Validators.required],
      email : ['', [Validators.required, Validators.email]]
    });
  }

  salvarAlteracao(template: any) {
    if ( this.registerForm.valid) {
      this.evento = Object.assign({}, this.registerForm.value);
      this.eventoService.postEvento(this.evento).subscribe(
        (novoEvento: Evento) => {
          console.log(novoEvento);
          template.hide();
          this.getEventos();
        }, error => {
          console.log(error);
        }
        );
    }
  }

  registrar(template: any) {
     this.putEvento(template);
  }

  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }


  getEventos() {
    this.eventoService.getAllEvento().subscribe(
      (eventos: Evento[]) => {
      this.eventos = eventos;
      this.eventosFiltrados = this.eventos;
      console.log(eventos); } ,
      error => { console.log(error);
  });

  }

  putEvento(template: any) {
    if ( this.registerForm.valid) {
      this.evento = Object.assign({id: this.evento.id}, this.registerForm.value);
      this.eventoService.putEvento(this.evento).subscribe(
        () => {
          template.hide();
          this.getEventos();
        }, error => {
          console.log(error);
        }
        );
    }
  }

  excluirEvento(evento: Evento, template: any) {
    this.openModal(template);
    this.evento = evento;
    this.bodyDeletarEvento = `Tem certeza que deseja excluir o Evento: ${evento.tema}, Código: ${evento.id}`;
  }
  
  confirmeDelete(template: any) {
    this.eventoService.deleteEvento(this.evento.id).subscribe(
      () => {
          template.hide();
          this.getEventos();
        }, error => {
          console.log(error);
        }
    );
  }

}
