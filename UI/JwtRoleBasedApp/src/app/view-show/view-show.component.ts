import { Component, OnInit } from '@angular/core';
import { ShowDTO } from '../models/show-dto';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomerService } from '../services/customer.service';
import { AuthService } from '../services/auth.service';
import { MatDialog } from '@angular/material/dialog';
import { SeatSelectionDialogComponent } from '../seat-selection-dialog/seat-selection-dialog.component';

@Component({
  selector: 'app-view-show',
  templateUrl: './view-show.component.html',
  styleUrl: './view-show.component.css'
})
export class ViewShowComponent implements OnInit {
  movieId: number = 0;
  shows: ShowDTO[] = [];
  errorMessage: string = '';

  constructor(
    private route: ActivatedRoute, 
    private customerService: CustomerService,
    private router: Router, 
    private authService: AuthService,
    private dialog: MatDialog // Import MatDialog
  ) {}

  ngOnInit(): void {
    // Get movieId from route params
    this.route.params.subscribe(params => {
      this.movieId = +params['movieId'];
      this.getShowsForMovie(this.movieId);
    });
  }

  // Fetch the shows for the selected movie
  getShowsForMovie(movieId: number): void {
    this.customerService.getAllShows().subscribe({
      next: (shows) => {
        this.shows = shows.filter(show => show.movieId === movieId);
        console.log("show",this.shows)
      },
      error: (err) => {
        this.errorMessage = 'Failed to fetch shows. Please try again.';
        console.error(err);
      }
    });
  }

  // Open Seat Selection Dialog
  bookTicket(showId: number): void {
    // Check if the user is logged in
    if (!this.authService.isAuthenticated()) {
      alert('You need to log in to book tickets.');
      return;  // Prevent further action if the user is not logged in
    }

    // Find the selected show from the list
    const selectedShow = this.shows.find(show => show.showId === showId);
    
    // Open the Seat Selection Dialog
    const dialogRef = this.dialog.open(SeatSelectionDialogComponent, {
      width: '400px',
      data: { show: selectedShow }
    });

    // Handle the result from the dialog (user confirms seat selection)
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log('Seats booked:', result.seats, 'Total Amount:', result.totalAmount);
        // Update the available seats for the selected show
      if (selectedShow) {
        selectedShow.numberOfSeats -= result.seats;
      }
      }
    });
  }
 
}
