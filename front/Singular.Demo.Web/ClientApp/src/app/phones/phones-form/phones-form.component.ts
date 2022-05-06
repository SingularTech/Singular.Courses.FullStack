import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Phone } from '../models/phone.model';
import { PhonesService } from '../services/phones.service';

@Component({
  selector: 'app-phones-form',
  templateUrl: './phones-form.component.html',
  styleUrls: ['./phones-form.component.css']
})
export class PhonesFormComponent implements OnInit {
  form!: FormGroup;
  result: string = '';

  constructor(
    private formBuilder: FormBuilder,
    private phonesService: PhonesService,
    private routing: Router
  ) {

  }

  createForm() {
    this.form = this.formBuilder.group({
      id: [null],
      brand: [null, [Validators.required, Validators.minLength(2), Validators.maxLength(255)]],
      model: [null, [Validators.required, Validators.minLength(2), Validators.maxLength(255)]],
      number: [null, [Validators.required, Validators.minLength(2), Validators.maxLength(255)]]
    });
  }

  ngOnInit(): void {
    this.createForm();
  }

  save() {
    let phone = new Phone({ ...this.form.value });

    phone.id = phone.id ?? 0;

    if (this.form.valid) {
      this.phonesService.create(phone).subscribe(_ => {
        this.result = 'El teléfono fue creado correctamente... 😎';
        setTimeout(() => {
          this.routing.navigate(['/phones']);
        }, 3000);
      }, error => {
        this.result = 'Ocurrió un error guardando el teléfono... 😥';
        console.error(error);
      });
    }
  }
}
