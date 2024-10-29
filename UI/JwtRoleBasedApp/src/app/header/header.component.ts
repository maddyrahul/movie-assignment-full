import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  role: string | null = null;
  username: string | null = null;

  constructor(private authService: AuthService) {}

  ngOnInit() {
    this.username = localStorage.getItem('username'); // Retrieve username from localStorage
    if (this.isLoggedIn()) {
      this.role = this.authService.getRole();
    }

    // Subscribe to username changes
    this.authService.getUsernameObservable().subscribe(username => {
      this.username = username;
      if (this.isLoggedIn()) {
        this.role = this.authService.getRole();
      }
    });
  }

  isLoggedIn(): boolean {
    return this.authService.isAuthenticated();
  }

  logout() {
    this.role = null;
    this.username = null;
    this.authService.logout();
  }
}
