import { ApplicationInitStatus, Component, NgModule, Pipe, PipeTransform, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { StudentDetails } from './student-details/student-details';
import { StudentMarks } from './student-marks/student-marks';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { StudentService } from './student';
import { HttpClient, httpResource } from '@angular/common/http';
@Component({
  selector: 'app-root',
  imports: [StudentDetails, StudentMarks, BrowserModule, FormsModule, HttpClient],
  providers: [StudentService],
  templateUrl: './app.html',
  styleUrl: './app.css'
})

@Pipe({
    name: 'namePipe',
    standalone: true
  })
export class NamePipe implements PipeTransform
{
  protected readonly title = signal('HttpObServices');
  
    transform(value : string, defaultValue : string) : string{
      if(value != ""){
        return value;
      } else {
        return defaultValue;
      }
    }
}
@NgModule({
  declarations: [],
  imports: [BrowserModule,FormsModule],
  providers: [],
  bootstrap: [App]
})
export class App {
  title = 'HttpObServices';
  constructor() {
    console.log('App component initialized');
  }
}

