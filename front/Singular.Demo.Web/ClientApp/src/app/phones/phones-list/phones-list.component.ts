import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-phones-list',
  templateUrl: './phones-list.component.html',
  styleUrls: ['./phones-list.component.css']
})
export class PhonesListComponent implements OnInit {
  title: string = 'Phones List';

  phones: any[] = [
    { id: 1, brand: 'Samsung', model: 'S9', number: '+51968133858' }
  ]

  constructor() { }

  ngOnInit(): void {
  }

  onClear() {
    this.phones = []
  }
}
