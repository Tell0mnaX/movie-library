import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; // 👈 obligatoire pour ngModel
import { Router } from '@angular/router';
import { ShowService } from '../show.service';
import { UserService, User } from '../user.service';
import { Show } from '../show.model';
import { ShowCreateDto } from '../ShowCreateDto.model';

@Component({
  standalone: true,
  selector: 'app-show-form',
  templateUrl: './show-form.component.html',
  imports: [CommonModule, FormsModule],
})
export class ShowFormComponent {
  newShow: ShowCreateDto = {
    title: '',
    creator: '',
    genre: '',
    seasons: 1,
    startYear: new Date().getFullYear(),
    rating: 0.0,
    imageUrl: '',
    userId: 0,
  };

  users: User[] = []; // 🔥 Tableau d'utilisateurs pour le <select>

  successMessage: string = '';
  errorMessage: string = '';
  
  isSubmitting: boolean = false;

  constructor(
    private showService: ShowService,
    private userService: UserService,
    private router: Router
  ) {}

  ngOnInit() {
    this.userService.getUsers().subscribe(users => {
      this.users = users;
    });
  }

  addShow(): void {
    if (this.newShow.title && this.newShow.genre && this.newShow.startYear) {
      
      // Récupérer le UserId depuis le UserService
      const selectedUserId = this.userService.getSelectedUser();

      if (selectedUserId == 0) {
        this.errorMessage = "❌ Tu dois choisir un utilisateur pour ajouter une série.";
        this.successMessage = '';
        return;
      }

      // ✅ Affecter le userId correct
      this.newShow.userId = selectedUserId;

      console.log(this.newShow);
      this.showService.addShow(this.newShow).subscribe({
        next: () => {
          this.successMessage = "✅ Série ajoutée avec succès !";
          this.errorMessage = '';
          // (Eventuellement tu peux vider les champs du formulaire ici si tu veux)
        },
        error: (error) => {
          console.error('Erreur ajout', error);
          this.errorMessage = "❌ Erreur lors de l'ajout.";
          this.successMessage = '';
        }
      });
    }
  }
  
}