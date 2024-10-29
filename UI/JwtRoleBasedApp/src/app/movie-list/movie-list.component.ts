import { Component, OnInit } from '@angular/core';
import { MovieDTO } from '../models/movie-dto';
import { CustomerService } from '../services/customer.service';
import { Router } from '@angular/router';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-movie-list',
  templateUrl: './movie-list.component.html',
  styleUrl: './movie-list.component.css'
})
export class MovieListComponent implements OnInit {
  movies: MovieDTO[] = [];
  selectedDate: string;

  constructor(private movieService: CustomerService, private router: Router) { 
    // Set the default date to the current date
    this.selectedDate = formatDate(new Date(), 'yyyy-MM-dd', 'en');
  }

  ngOnInit(): void {
    // Load all movies for the selected date (initially current date)
    this.loadMoviesForDate(this.selectedDate);
  }

  // Fetch movies for the selected date
  loadMoviesForDate(date: string): void {
    this.movieService.getMoviesByDate(date).subscribe({
      next: (data: MovieDTO[]) => {
        this.movies = data;
      },
      error: (error) => {
        console.error('Error fetching movies:', error);
      }
    });
  }

  // Handle movie selection and redirect to movie details page
  onMovieSelect(movieId: number): void {
    // Navigate to movie details with selected date and movieId as query params
    this.router.navigate(['/movie-details'], { queryParams: { movieId, date: this.selectedDate } });
  }

  // Update selected date and load movies again
  onDateChange(event: any): void {
    this.selectedDate = event.target.value;
    this.loadMoviesForDate(this.selectedDate);
  }
  
  // Navigate to ViewShowComponent with the selected movie's ID
  viewShowDetails(movieId: number): void {
    this.router.navigate(['/view-show', movieId]);
  }

}
