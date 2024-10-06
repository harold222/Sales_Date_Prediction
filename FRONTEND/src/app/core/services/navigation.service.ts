import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NavigationService {

  public navigation = signal({ title: 'Sales Date Prediction App', color: '#000000' });
  
  constructor() { }
}
