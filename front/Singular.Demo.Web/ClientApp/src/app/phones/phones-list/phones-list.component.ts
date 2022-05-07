import { Component, OnInit } from '@angular/core';
import { Phone } from '../models/phone.model';
import { PhonesService } from '../services/phones.service';

@Component({
  selector: 'app-phones-list',
  templateUrl: './phones-list.component.html',
  styleUrls: ['./phones-list.component.css']
})
export class PhonesListComponent implements OnInit {
  title: string = 'Phones List';
  result: string = '';
  search: string = '';
  phones: Phone[] = []

  constructor(private phonesService: PhonesService) {

  }

  ngOnInit(): void {
    this.onSearch('');
  }

  onSearch(q: string) {
    this.phonesService.list(q ?? this.search).subscribe(response => {
      this.phones = response;
      this.result = '';
    }, (_) => this.result = 'Ocurrió un error cargando los teléfonos... 😥');
  }

  onClear() {
    this.search = '';
    this.onSearch('');
  }

  onDelete(id: number) {
    this.phonesService.delete(id).subscribe(_ => {
      this.result = 'El teléfono fue eliminado correctamente... 😎';
      setTimeout(() => {
        this.onSearch('');
      }, 3000);
    }, (_) => this.result = 'Hubo un error eliminando el teléfono... 😥');
  }
}
