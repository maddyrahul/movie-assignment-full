import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ShowDTO } from '../models/show-dto';
import { Observable } from 'rxjs/internal/Observable';
import { MovieDTO } from '../models/movie-dto';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  private baseUrl = 'https://localhost:7110/api/Admin';  

  constructor(private http: HttpClient) { }

  // 1. View all current shows for a specific date
  getShowsByDate(date: string): Observable<ShowDTO[]> {
    return this.http.get<ShowDTO[]>(`${this.baseUrl}/shows/${date}`);
  }

  // 2. Add a new movie
  addMovie(movie: MovieDTO): Observable<MovieDTO> {
    return this.http.post<MovieDTO>(`${this.baseUrl}/movies`, movie);
  }

  // 3. Add additional showtime to an existing movie
  addShowToMovie(movieId: number, show: ShowDTO): Observable<ShowDTO> {
    return this.http.post<ShowDTO>(`${this.baseUrl}/movies/${movieId}/show`, show);
  }

  // Helper method to get movie by ID
  getMovieById(id: number): Observable<MovieDTO> {
    return this.http.get<MovieDTO>(`${this.baseUrl}/movies/${id}`);
  }
}
