import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Phone } from '../models/phone.model';

@Injectable({
  providedIn: 'root'
})
export class PhonesService {
  private backend: string = "https://localhost:7163/api/phone";

  constructor(private httpClient: HttpClient) {

  }

  create = (phone: Phone): Observable<Phone> => this.httpClient.post<Phone>(`${this.backend}`, phone);

  update = (phone: Phone): Observable<Phone> => this.httpClient.put<Phone>(`${this.backend}`, phone);

  delete = (id: number): Observable<Phone> => this.httpClient.delete<Phone>(`${this.backend}/${id}`);

  get = (id: number): Observable<Phone> => this.httpClient.get<Phone>(`${this.backend}/${id}`);

  list = (): Observable<Phone[]> => this.httpClient.get<Phone[]>(`${this.backend}`);
}
