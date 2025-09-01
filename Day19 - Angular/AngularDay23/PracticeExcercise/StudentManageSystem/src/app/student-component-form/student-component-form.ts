import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IStudent } from '../student';

@Component({
  selector: 'app-student-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './student-component-form.html',
  styleUrls: ['./student-component-form.css']
})
export class StudentFormComponent {
  @Output() studentAdded = new EventEmitter<IStudent>();

  newStudent: IStudent = { id: 0, name: '', marks: 0 };

  addStudent() {
    this.studentAdded.emit(this.newStudent);
    this.newStudent = { id: 0, name: '', marks: 0 }; // reset form
  }
}
