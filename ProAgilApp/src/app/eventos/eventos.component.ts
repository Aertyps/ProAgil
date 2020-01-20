import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventoService } from '../_services/evento.service';
import { Evento } from '../_modules/Evento';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';


@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  filtroList: string;
  get filtroLista(): string {
    return this.filtroList;
  }
  set filtroLista(value: string) {
    this.filtroList = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }
  eventosFiltrados: Evento[];
  eventos: Evento[];
  imagemAltura = 50;
  imagemMargem =  2;
  mostrarImagem = false;
  modalRef: BsModalRef;

  constructor( private eventoService: EventoService, private modalService: BsModalService) {
  }

  ngOnInit() {
    this.getEventos();
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  filtrarEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }
  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }
  getEventos() {
    this.eventoService.getAllEvento().subscribe(
      ( _eventos: Evento[]) => {
      this.eventos = _eventos; console.log(_eventos); },
       error => { console.log(error);
  });

  }

}
