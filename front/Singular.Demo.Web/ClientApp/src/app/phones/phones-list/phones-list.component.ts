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

  phones: Phone[] = []

  constructor(private phonesService: PhonesService) {

  }

  ngOnInit(): void {
    this.onSearch();
  }

  onSearch() {
    this.phonesService.list().subscribe(response => {
      this.phones = response;
      this.result = '';
    }, (_) => this.result = 'Ocurrió un error cargando los teléfonos... 😥');
  }

  onClear() {
    this.phones = []
  }
}
