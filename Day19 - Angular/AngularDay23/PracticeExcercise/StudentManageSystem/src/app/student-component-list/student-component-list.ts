import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IStudent } from '../student';
import { StudentService } from '../student-service';

@Component({
  selector: 'app-student-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './student-component-list.html',
  styleUrls: ['./student-component-list.css']
})
export class StudentListComponent implements OnInit {
  @Input() students: IStudent[] = [];
  @Output() studentDeleted = new EventEmitter<number>();

  constructor(private studentService: StudentService) {}

  ngOnInit(): void {
    // If not passed from parent, fetch from service
    if (this.students.length === 0) {
      this.studentService.getStudents().subscribe(data => this.students = data);
    }
  }

  deleteStudent(id: number) {
    this.studentDeleted.emit(id);
  }

  editStudent(student: IStudent) {
    const updatedName = prompt('Enter new name:', student.name);
    const updatedMarks = prompt('Enter new marks:', student.marks.toString());

    if (updatedName !== null && updatedMarks !== null) {
      student.name = updatedName.trim() || student.name;
      student.marks = Number(updatedMarks) || student.marks;
    }
  }
}
