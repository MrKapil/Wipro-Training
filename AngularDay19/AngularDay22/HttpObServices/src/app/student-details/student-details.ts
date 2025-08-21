import { Component, OnInit } from '@angular/core';
import { StudentService } from '../student';
import { StudentMarks } from '../student-marks/student-marks';

@Component({
  selector: 'app-student-details',
  imports: [],
  templateUrl: './student-details.html',
  styleUrls: ['./student-details.css']
})
export class StudentDetails {
  
  constructor(private studentService: StudentService){}

  ngOnInit(){
    this.studentService.getStudent().subscribe(data => data);
  }
}
