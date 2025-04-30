import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ShowService, Show } from '../show.service';

@Component({
  standalone: true,
  selector: 'app-show-edit',
  templateUrl: './show-edit.component.html',
  styleUrls: ['./show-edit.component.css'],
  imports: [CommonModule, FormsModule]
})
export class ShowEditComponent implements OnInit {
  show: Show = {
    id: 0,
    title: '',
    creator: '',
    genre: '',
    seasons: 1,
    startYear: new Date().getFullYear(),
    rating: 0.0,
    imageUrl: '',
    userId: 0
  };

  successMessage: string = '';
  isSubmitting: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private showService: ShowService
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.showService.getShow(id).subscribe(data => {
      this.show = data;
      console.log("🎯 Show reçu :", this.show);
    });
  }

  updateShow(): void {
    this.isSubmitting = true;

    this.showService.updateShow(this.show).subscribe(() => {
      this.successMessage = '✅ Série ajoutée avec succès !';

    setTimeout(() => {
      this.successMessage = '';
      this.isSubmitting = false;
      this.router.navigate(['/']);
    }, 2000);
    });
  }
}
