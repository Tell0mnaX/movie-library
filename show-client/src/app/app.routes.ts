import { Routes } from '@angular/router';
import { ShowListComponent } from './show-list/show-list.component';
import { ShowFormComponent } from './show-form/show-form.component';
import { ShowEditComponent } from './show-edit/show-edit.component';

export const routes: Routes = [
  { path: '', component: ShowListComponent },
  { path: 'add', component: ShowFormComponent },
  { path: 'edit/:id', component: ShowEditComponent }
  
];