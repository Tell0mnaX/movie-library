import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ShowCreateDto } from './ShowCreateDto.model';

export interface Show {
  id: number;
  title: string;
  creator: string;
  seasons: number;
  genre: string;
  startYear: number;
  rating: number;
  imageUrl: string;
  userId: number;
  username?: string;
}

@Injectable({
  providedIn: 'root'
})
export class ShowService {
  private apiUrl = 'http://localhost:5257/api/show';

  constructor(private http: HttpClient) {}

  getShows(): Observable<Show[]> {
    return this.http.get<Show[]>(this.apiUrl);
  }

  getShowsByUsername(userId: number): Observable<Show[]> {
    return this.http.get<Show[]>(`${this.apiUrl}/userShows/${userId}`);
  }

  addShow(newShow: ShowCreateDto): Observable<Show> {
    return this.http.post<Show>(this.apiUrl, newShow);
  }
  

  deleteShow(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  getShow(id: number): Observable<Show> {
    return this.http.get<Show>(`${this.apiUrl}/${id}`);
  }

  getSortedShowsByYear(): Observable<Show[]> {
    return this.http.get<Show[]>(`${this.apiUrl}/sort/year`);
  }

  getShowsByGenre(genre: string): Observable<Show[]> {
    return this.http.get<Show[]>(`${this.apiUrl}/genre/${genre}`);
  }

  getTopShows(): Observable<Show[]> {
    return this.http.get<Show[]>(`${this.apiUrl}/top5`);
  }
  

  searchShowsWithKeyword(keyword: string): Observable<Show[]> {
    return this.http.get<Show[]>(`${this.apiUrl}/search/${keyword}`);
  }
  
  updateShow(show: Show): Observable<any> {
    return this.http.put(`${this.apiUrl}/${show.id}`, show);
  }
  
  
}
