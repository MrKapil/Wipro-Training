import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

interface Student {
  id: number;
  name: string;
  marks: number;
}

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App {
  // initial data
  students: Student[] = [
    { id: 1001, name: 'Kapil', marks: 90 },
    { id: 1002, name: 'Raju',  marks: 80 },
    { id: 1003, name: 'Shyam', marks: 70 }
  ];

  // ADD inputs
  addId = '';
  addName = '';
  addMarks = '';

  // DELETE input
  delId = '';

  // MODIFY inputs
  editId = '';
  editName = '';
  editMarks = '';

  // --- ADD ---
  addStudent() {
    const id = Number(this.addId);
    const marks = Number(this.addMarks);
    if (!Number.isFinite(id) || id <= 0) { alert('Enter valid numeric ID'); return; }
    if (!this.addName.trim()) { alert('Enter name'); return; }
    if (!Number.isFinite(marks) || marks < 0) { alert('Enter valid marks'); return; }
    if (this.students.some(s => s.id === id)) { alert('ID already exists'); return; }

    this.students.push({ id, name: this.addName.trim(), marks });
    this.addId = ''; this.addName = ''; this.addMarks = '';
  }

  // --- DELETE ---
  deleteStudent() {
    const id = Number(this.delId);
    if (!Number.isFinite(id)) { alert('Enter ID to delete'); return; }
    const before = this.students.length;
    this.students = this.students.filter(s => s.id !== id);
    if (this.students.length === before) { alert('Student not found'); }
    this.delId = '';
  }

  // --- MODIFY (by ID) ---
  modifyStudent() {
    const id = Number(this.editId);
    if (!Number.isFinite(id)) { alert('Enter valid ID'); return; }
    const s = this.students.find(x => x.id === id);
    if (!s) { alert('Student not found'); return; }

    if (this.editName.trim()) s.name = this.editName.trim();
    if (this.editMarks !== '') {
      const m = Number(this.editMarks);
      if (!Number.isFinite(m)) { alert('Enter valid marks'); return; }
      s.marks = m;
    }
    this.editId = ''; this.editName = ''; this.editMarks = '';
  }
}
