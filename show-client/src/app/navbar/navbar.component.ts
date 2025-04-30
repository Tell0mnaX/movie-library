import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { User, UserService } from '../user.service';
import { NgIf, NgForOf } from '@angular/common';  // ➔ pour pouvoir utiliser *ngIf et *ngFor
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterModule, NgIf, NgForOf, FormsModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {

  users: User[] = [];
  selectedUserId: number = 0;

  constructor(private userService: UserService){ }

  ngOnInit(): void {
    this.userService.getUsers().subscribe(users => {
      this.users = users;
    });
  }

  onUserChange(userId: number): void {
    console.log('🔄 Utilisateur changé :', this.selectedUserId);
    this.userService.setSelectedUser(this.selectedUserId);
  }  

}
