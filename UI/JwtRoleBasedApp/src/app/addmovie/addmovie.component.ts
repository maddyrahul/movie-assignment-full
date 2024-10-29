import { Component } from '@angular/core';
import { MovieDTO } from '../models/movie-dto';
import { AdminService } from '../services/admin.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-addmovie',
  templateUrl: './addmovie.component.html',
  styleUrl: './addmovie.component.css'
})
export class AddmovieComponent {
  errorMessage: string = '';
  movie: MovieDTO = {
    movieId: 0,
    name: '',
    genre: '',
    director: '',
    description: ''
  };

  constructor(private movieService: AdminService, private router: Router) {}


onSubmit(): void {
  this.movieService.addMovie(this.movie).subscribe(
    (response) => {
      console.log('Movie added successfully:', response);
      // Redirect to the dashboard after successful movie addition
      this.router.navigate(['/dashboard']);
    },
    (err) => {
      // Error handling
      if (err.status === 400) {
        this.errorMessage = err.error; // Display the exact backend error message
      } else {
        this.errorMessage = 'Failed to book ticket. Please try again.'; 
      }
      
      console.error('Error adding movie:', err);
    }
  );
}

}
