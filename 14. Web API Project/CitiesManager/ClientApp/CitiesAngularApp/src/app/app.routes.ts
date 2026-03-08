import { Routes } from '@angular/router';
import { CitiesComponent } from './cities/cities.component';

export const routes: Routes = [
  { path: '', redirectTo: 'cities', pathMatch: 'full' },
  { path: 'cities', component: CitiesComponent }
];
