import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { User } from '@core/models/user.model';


@Component({
  selector: 'app-admin-users',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './users.html',
  styleUrls: ['./users.css']
})
export class UsersComponent implements OnInit {
  users: User[] = [];
  showForm = false;
  currentUser: Partial<User> = {};
  isEditing = false;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.http.get<User[]>(`${environment.apiUrl}/Users`).subscribe(
      data => this.users = data
    );
  }

  addUser(): void {
    this.currentUser = { role: 'User', isActive: true };
    this.isEditing = false;
    this.showForm = true;
  }

  editUser(user: User): void {
    this.currentUser = { ...user };
    this.isEditing = true;
    this.showForm = true;
  }

  saveUser(): void {
    if (this.isEditing) {
      this.http.put(`${environment.apiUrl}/Users/${this.currentUser.userId}`, this.currentUser)
        .subscribe(() => {
          this.loadUsers();
          this.closeForm();
        });
    } else {
      this.http.post(`${environment.apiUrl}/Users`, this.currentUser)
        .subscribe(() => {
          this.loadUsers();
          this.closeForm();
        });
    }
  }

  deleteUser(userId: number): void {
    if (confirm('Are you sure you want to delete this user?')) {
      this.http.delete(`${environment.apiUrl}/Users/${userId}`)
        .subscribe(() => this.loadUsers());
    }
  }

  closeForm(): void {
    this.showForm = false;
    this.currentUser = {};
  }
}
