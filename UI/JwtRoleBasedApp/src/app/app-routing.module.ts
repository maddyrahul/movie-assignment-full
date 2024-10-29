import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuard } from './guards/auth.guard';
import { MovieListComponent } from './movie-list/movie-list.component';
import { ViewShowComponent } from './view-show/view-show.component';
import { AddmovieComponent } from './addmovie/addmovie.component';
import { AddShowComponent } from './add-show/add-show.component';
import { DashboardComponent } from './dashboard/dashboard.component';


const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'movie-list', component: MovieListComponent ,canActivate: [AuthGuard]},
  { path: 'view-show/:movieId', component: ViewShowComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  {path:'add-movie', component:AddmovieComponent },
  { path: 'add-show/:movieId', component: AddShowComponent},
  { path: 'dashboard', component: DashboardComponent,canActivate: [AuthGuard] },
  { path: '**', redirectTo: '/login', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
