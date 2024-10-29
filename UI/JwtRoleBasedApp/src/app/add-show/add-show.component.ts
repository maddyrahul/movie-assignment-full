import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminService } from '../services/admin.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ShowDTO } from '../models/show-dto';

@Component({
  selector: 'app-add-show',
  templateUrl: './add-show.component.html',
  styleUrl: './add-show.component.css'
})
export class AddShowComponent {
  showForm: FormGroup;
  movieId: number = 0;
  movieName: string = ''; // Variable to hold the movie name
  successMessage: string = '';
  errorMessage: string = '';
  selectedStartDate: string = ''; // To store the selected start date

  constructor(
    private fb: FormBuilder,
    private adminService: AdminService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.showForm = this.fb.group({
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      timing: ['', [Validators.required]], // e.g., 14:30
      numberOfSeats: ['', [Validators.required, Validators.min(1), Validators.max(100)]],
      price: ['', [Validators.required, Validators.min(0)]],
      screenNumber: ['', [Validators.required, Validators.min(1)]]
    });
  }

  ngOnInit(): void {
    // Extract movieId from the route and fetch movie details
    this.route.params.subscribe(params => {
      this.movieId = +params['movieId'];
      this.getMovieDetails(this.movieId); // Fetch movie name
    });

    // Subscribe to changes in the form control 'startDate'
    this.showForm.get('startDate')?.valueChanges.subscribe(value => {
      this.selectedStartDate = value; // Capture the selected start date
    });
  }

  // Fetch movie details to display the movie name
  getMovieDetails(movieId: number): void {
    this.adminService.getMovieById(movieId).subscribe({
      next: (movie) => {
        this.movieName = movie.name; // Set the movie name for display
      },
      error: (err) => {
        console.error('Failed to load movie details', err);
      }
    });
  }

  // Method to add a show
  addShow(): void {
    if (this.showForm.valid) {
      const newShow: ShowDTO = {
        ...this.showForm.value,
        movieId: this.movieId
      };

      this.adminService.addShowToMovie(this.movieId, newShow).subscribe({
        next: (show) => {
          this.successMessage = `Show added successfully for Movie ${this.movieName}`;
          this.showForm.reset(); // Reset form after successful submission
          this.errorMessage = ''; // Clear any previous error message
          this.goBack();
        },
        error: (err) => {
          if (err.status === 400) {
            this.errorMessage = err.error; // This will display the exact backend error message
          } else {
            this.errorMessage = 'Failed to book ticket. Please try again.';
          }
          this.successMessage = ''; // Clear any previous success message
        }
      });
    } else {
      this.errorMessage = 'Please fill in all the required fields correctly.';
    }
  }

  // Navigate back to the movie list
  goBack(): void {
    this.router.navigate(['/dashboard']);
  }
}
