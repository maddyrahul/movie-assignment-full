import { Injectable } from '@angular/core';
import { MovieDTO } from '../models/movie-dto';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { TicketDTO } from '../models/ticket-dto';
import { ShowDTO } from '../models/show-dto';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  private baseUrl = 'https://localhost:7110/api/Customer';  

  constructor(private http: HttpClient) { }

  // 1. View available movies for a specific day
  getMoviesByDate(date: string): Observable<MovieDTO[]> {
    return this.http.get<MovieDTO[]>(`${this.baseUrl}/movies/${date}`);
  }

  // 2. Get seat availability for a specific show
  getAvailableSeats(showId: number): Observable<number> {
    return this.http.get<number>(`${this.baseUrl}/shows/${showId}/seats`);
  }

  // 3. Book movie tickets
  bookTicket(ticket: TicketDTO): Observable<TicketDTO> {
    return this.http.post<TicketDTO>(`${this.baseUrl}/tickets`, ticket);
  }

  // 4. View all booked tickets for a customer
  getBookedTickets(userId: number): Observable<TicketDTO[]> {
    return this.http.get<TicketDTO[]>(`${this.baseUrl}/tickets/${userId}`);
  }

  // 5. Get all available movies
  getAllMovies(): Observable<MovieDTO[]> {
    return this.http.get<MovieDTO[]>(`${this.baseUrl}/movies`);
  }

  // 6. Get all available shows
  getAllShows(): Observable<ShowDTO[]> {
    return this.http.get<ShowDTO[]>(`${this.baseUrl}/shows`);
  }
}
