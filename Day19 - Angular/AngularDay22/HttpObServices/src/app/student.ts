import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IStudent } from './student1';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudentService {
private url: string = 'src/assets/students.json';
  constructor(private http: HttpClient){}

  getStudent(){
    return this.http.get<IStudent[]>(this.url);
  } 
}
