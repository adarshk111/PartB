import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import {GlossaryTerm} from '../models/GlossaryTerm';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class GlossaryTermsService {
  private baseurl = environment.apiUrl;
  constructor(private http : HttpClient) { }
  getGlossaryTerms(): Observable<GlossaryTerm[]>{
  return this.http.get(`${this.baseurl}api/GlossaryTerms`).pipe(
  map((res: any) => res));
  }
  CreateGlossaryTerm(glossaryTerm : GlossaryTerm) : Observable<GlossaryTerm>{
    return this.http.post(`${this.baseurl}api/GlossaryTerms`,glossaryTerm).pipe(
      map((res: any) => res));
  }
  UpdateGlossaryTerm(glossaryTerm : GlossaryTerm) {
    return this.http.put(`${this.baseurl}api/GlossaryTerms`, glossaryTerm).pipe(
      map((res: any) => res));
  }
  DeleteGlossaryTerm(id : number) {
    return this.http.delete(`${this.baseurl}api/GlossaryTerms/${id}`).pipe(
      map((res: any) => res));
  }
}
