import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Observable } from 'rxjs';
import { CategoryDto } from '../models/categoryDto';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  constructor(private httpService: HttpService) {}

  getCategories(): Observable<CategoryDto[]> {
    return this.httpService.get<CategoryDto[]>('Category');
  }
}
