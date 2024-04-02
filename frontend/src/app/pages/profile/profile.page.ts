import { Component, OnInit } from '@angular/core';
import { UserDto } from 'src/app/models/userDto';
import { PhotoService } from 'src/app/services/photo.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.page.html',
  styleUrls: ['./profile.page.scss'],
})
export class ProfilePage implements OnInit {
  imageSource?: any;
  user?: UserDto;
  constructor(
    private userService: UserService,
    private photoService: PhotoService
  ) {}

  ngOnInit() {
    this.user = this.userService.getUserDetails();
    this.userService.getProfiePicture().subscribe((photo) => {
      this.imageSource = photo.ProfilePhoto;
    });
  }

  async takeProfilePhoto() {
    this.imageSource = await this.photoService.takePicture();
    this.userService.addProfilePicture(this.imageSource as string).subscribe();
  }
}
