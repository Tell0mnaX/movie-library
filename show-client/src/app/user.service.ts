import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';

export interface User {
  id: number;
  username: string;
}

@Injectable({
  providedIn: 'root'
})

export class UserService {
  private apiUrl = 'http://localhost:5257/api/User';
  
  private selectedUserSubject = new BehaviorSubject<number>(0); // 0 = tous les users
  // On expose un Observable public pour écouter les changements
  selectedUser$ = this.selectedUserSubject.asObservable();

  constructor(private http: HttpClient) {}

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.apiUrl);
  }

  // Pour mettre à jour l'utilisateur sélectionné
  setSelectedUser(userId: number): void {
    this.selectedUserSubject.next(userId);
  }

  // Pour récupérer la valeur courante directement
  getSelectedUser(): number {
    return this.selectedUserSubject.value;
  }

}