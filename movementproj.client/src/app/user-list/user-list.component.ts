import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  users: any[] = [];
  page: number = 1;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.userService.getUsers(this.page).subscribe(data => {
      this.users = data;
    }, error => {
      console.error('Error fetching users', error);
    });
  }
  deleteUser(id: number): void {
    this.userService.deleteUser(id).subscribe(() => {
      this.loadUsers();
    }, error => {
      console.error('Error deleting user', error);
    });
  }

  viewUserDetails(id: number): void {
    // Navigate to user details component
  }
}
