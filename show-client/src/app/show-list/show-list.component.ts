import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShowService, Show } from '../show.service';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { UserService, User } from '../user.service';


@Component({
  selector: 'app-show-list',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule], // 👈 ici !
  templateUrl: './show-list.component.html',
  styleUrls: ['./show-list.component.css']
})
export class ShowListComponent implements OnInit {
  shows: Show[] = [];

  users: User[] = [];
  selectedUserId: number = 0;

  genres: string[] = []; // Tu peux l’enrichir
  selectedGenre: string = 'All';

  searchKeyword: string = ""

  isLoading: boolean = false;

  successMessage: string = '';
  errorMessage: string = '';

  constructor(private showService: ShowService, private userService: UserService) 
  { }

  ngOnInit(): void {
    this.getShows();
    // 🔥 On écoute les changements d'utilisateur
    this.userService.selectedUser$.subscribe(userId => {
      console.log('🎯 Changement détecté :', userId);
      this.filterByUser(userId);
    });
  }

  onSearchInputChange(): void {
    const keyword = this.searchKeyword.trim();
  
    if (keyword === '') {
      this.getShows(); // Recharge toute la liste si champ vide
    } else {
      this.showService.searchShowsWithKeyword(keyword).subscribe(shows => {
        this.shows = shows;
      });
    }
  }

  getShows(): void {
    this.isLoading = true; // ⏳ On démarre le chargement
  
    this.showService.getShows().subscribe({
      next: (data) => {
        this.shows = data;
        this.isLoading = false; // ✅ Chargement terminé
        
        // ✅ Extraire les genres uniques
        const uniqueGenres = [...new Set(this.shows.map(show => show.genre))].sort();

        // ✅ Ajouter "All" au début
        this.genres = ['All', ...uniqueGenres];
      },
      error: (error) => {
        console.error('Erreur lors de la récupération des séries', error);
        this.errorMessage = "Erreur lors du chargement des séries.";
        this.isLoading = false; // ❌ Chargement terminé même en cas d’erreur
      }
    });
  }

  filterByUser(userId:number): void {
    if (userId == 0 || !userId) {
      // Recharger tous les shows
      this.getShows();
    } else {
      // Filtrer par utilisateur
      this.showService.getShowsByUsername(userId).subscribe(data => {
        this.shows = data;
      });
    }
  }
  

  getShowsByUsername(userId: number): void {
    this.showService.getShowsByUsername(userId).subscribe(data => {
      this.shows = data;

      // ✅ Extraire les genres uniques
    const uniqueGenres = [...new Set(this.shows.map(show => show.genre))].sort();

    // ✅ Ajouter "All" au début
    this.genres = ['All', ...uniqueGenres];
    });
  }

  deleteShow(id: number): void {
    if (confirm("Es-tu sûr de vouloir supprimer cette série ?")) {
      this.showService.deleteShow(id).subscribe({
        next: () => {
          this.shows = this.shows.filter(show => show.id !== id);
          this.successMessage = "✅ Série supprimée avec succès !";
          this.errorMessage = '';
        },
        error: (error) => {
          console.error('Erreur suppression', error);
          this.errorMessage = "❌ Erreur lors de la suppression.";
          this.successMessage = '';
        }
      });
    }
  }  

  getSortedShowsByYear():void{
    this.showService.getSortedShowsByYear().subscribe(sortedShows => {
      this.shows = sortedShows;
    });
  }

  getTopShows():void{
    this.showService.getTopShows().subscribe(shows => {
      this.shows = shows;
    });
  }

  filterByGenre(): void {
    if (this.selectedGenre === 'All') {
      this.getShows(); // recharge tous les shows
    } else {
      this.showService.getShowsByGenre(this.selectedGenre).subscribe(shows => {
        this.shows = shows;
      });
    }
  }
}
