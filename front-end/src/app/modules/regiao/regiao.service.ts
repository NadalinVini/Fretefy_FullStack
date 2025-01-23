import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class RegiaoService {
  private readonly baseUrl = 'http://localhost:55700/api';

  constructor(private http: HttpClient) {}

  listarRegioes(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/regiao`);
  }

  criarRegiao(regiao: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/regiao`, regiao);
  }

  atualizarRegiao(regiao: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/regiao`, regiao);
  }

  listarCidades(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/cidade`);
  }
}
