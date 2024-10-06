import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environments';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class GeneralService {
  protected readonly base: string = environment.apiEndpoint;
  protected http = inject(HttpClient);
  constructor() { }
}
