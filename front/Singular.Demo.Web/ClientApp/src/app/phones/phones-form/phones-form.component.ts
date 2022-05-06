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
  id: number = 0;
  form!: FormGroup;
  result: string = '';

  constructor(
    private formBuilder: FormBuilder,
    private phonesService: PhonesService,
    private routing: Router,
    private activatedRoute: ActivatedRoute
  ) {
    this.activatedRoute.params.subscribe(params => {
      this.id = params['id'] ? parseInt(params['id']) : 0;
      this.loadPhone();
    });
  }

  loadPhone() {
    this.phonesService.get(this.id).subscribe(phone => {
      this.form.patchValue(phone);
    });
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

    if (this.id == 0) {
      phone.id = phone.id ?? 0;
      this.create(phone);
    }
    else {
      phone.id = this.id;
      this.update(phone);
    }
  }

  create(phone: Phone) {
    if (this.form.valid) {
      this.phonesService.create(phone).subscribe(_ => {
        this.result = 'El teléfono fue creado correctamente... 😎';
        setTimeout(() => {
          this.routing.navigate(['/phones']);
        }, 3000);
      }, error => {
        this.result = 'Ocurrió un error crear el teléfono... 😥';
        console.error(error);
      });
    }
  }

  update(phone: Phone) {
    if (this.form.valid) {
      this.phonesService.update(phone).subscribe(_ => {
        this.result = 'El teléfono fue actualizado correctamente... 😎';
        setTimeout(() => {
          this.routing.navigate(['/phones']);
        }, 3000);
      }, error => {
        this.result = 'Ocurrió un error actualizar el teléfono... 😥';
        console.error(error);
      });
    }
  }
}
