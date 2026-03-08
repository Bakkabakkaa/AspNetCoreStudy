import { Routes } from '@angular/router';
import { Cities } from './cities/cities';

export const routes: Routes = [
  { path: '', redirectTo: 'cities', pathMatch: 'full' },
  { path: 'cities', component: Cities }
];
