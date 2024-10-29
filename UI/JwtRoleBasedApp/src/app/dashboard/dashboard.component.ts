import { Component } from '@angular/core';
import { MovieDTO } from '../models/movie-dto';
import { CustomerService } from '../services/customer.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {

  movies: MovieDTO[] = [];
  errorMessage: string = '';

  constructor(private customerService: CustomerService, private router: Router) { }

  ngOnInit(): void {
    this.getMovies();
  }

  // Fetch list of movies from the service
  getMovies(): void {
    this.customerService.getAllMovies().subscribe({
      next: (movies) => {
        this.movies = movies;
      },
      error: (err) => {
        this.errorMessage = 'Failed to fetch movies. Please try again.';
        console.error(err);
      }
    });
  }

  // Navigate to the Add Show component with the selected movie's ID
  addShow(movieId: number): void {
    this.router.navigate(['/add-show', movieId]);
  }
}
