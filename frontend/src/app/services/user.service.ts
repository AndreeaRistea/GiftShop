import { Observable } from 'rxjs';
import { HttpService } from './http.service';
import { UserDto } from '../models/userDto';
import { Injectable } from '@angular/core';
import { LoginDto } from '../models/loginDto';
import { SignupDto } from '../models/signupDto';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private httpService: HttpService) {}

  register(user: UserDto): Observable<SignupDto> {
    return this.httpService.post('User/Register', user);
  }

  login(credentials: {
    email: string;
    password: string;
  }): Observable<LoginDto> {
    return this.httpService.post('User/Login', credentials);
  }

  isLoggedIn() {
    return !!localStorage.getItem('token');
  }

  logout() {
    localStorage.removeItem('token');
  }

  getUserDetails() {
    const token = localStorage.getItem('token') as string;
    const decoded = jwtDecode(token) as any;

    console.log(decoded);
    const user = new UserDto();
    user.Id = decoded['id'];
    user.Email = decoded['Email'];
    user.Name = decoded['Name'];
    user.Address = decoded['Address'];
    return user;
  }

  getProfiePicture(): Observable<any> {
    return this.httpService.get('User/ProfilePicture');
  }

  addProfilePicture(profilePicture: string): Observable<any> {
    console.log('as');
    return this.httpService.post('User/ProfilePicture', {
      profilePhoto: profilePicture,
    });
  }
}
