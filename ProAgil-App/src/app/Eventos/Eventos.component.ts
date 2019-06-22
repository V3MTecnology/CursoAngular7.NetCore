import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventoService } from '../_services/evento.service';
import { Evento } from '../_models/Evento';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { FormGroup, FormControl, Validators } from '@angular/forms';


@Component({
  selector: 'app-Eventos',
  templateUrl: './Eventos.component.html',
  styleUrls: ['./Eventos.component.css']
})
export class EventosComponent implements OnInit {
  
  eventosFiltrados: Evento[];
  eventos: Evento[] = [];
  imagemLargura = 50;
  imagemMargim = 2;
  mostrarImagem = false;
  modalRef: BsModalRef;
  registerForm: FormGroup;
  
  _filtroLista = '';
  
  constructor(
    private eventoService: EventoService
    , private modalService: BsModalService
    )  {  }
    
    get filtroLista(): string {
      return this._filtroLista;
    }
    set filtroLista(value: string) {
      this._filtroLista = value;
      
      this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
    }
    
    openModal(template: TemplateRef<any>){
      this.modalRef = this.modalService.show(template);
      
    }
    
    ngOnInit() {
      this.validation();
      this.getEventos();
    }
    
    
    filtrarEventos(filtrarPor: string): Evento[] {
      filtrarPor = filtrarPor.toLocaleLowerCase();
      return this.eventos.filter(
        evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
        );
      }
      
      alternarImagem()
      {
        this.mostrarImagem = !this.mostrarImagem;
      }

      validation()
      {
        this.registerForm = new FormGroup({
          tema: new FormControl('',
            [Validators.required, Validators.minLength(4), Validators.maxLength(50)]
          ),
          local: new FormControl('', Validators.required),
          dataEvento: new FormControl('', Validators.required),
          qtdPessoas: new FormControl('',
              [Validators.required, Validators.maxLength(120000)]
            ),
          imagemURL: new FormControl('',
              [Validators.required, Validators.email]
              ),
          telefone: new FormControl('', Validators.required),
          email: new FormControl('', Validators.required)
        });
      }

      salvarAlteracao()
      {

      }


      
      getEventos()
      {
        this.eventoService.getAllEvento().subscribe(
          (_eventos: Evento[]) => {
            this.eventos =_eventos;
            this.eventosFiltrados = this.eventos;
            console.log(_eventos);
            
          }, error => {
            console.log(error);
          } );
        }

      }