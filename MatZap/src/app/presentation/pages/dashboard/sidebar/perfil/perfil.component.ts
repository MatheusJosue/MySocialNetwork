import { AuthenticationService } from 'src/app/domain/services/authentication.service';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/domain/models/user.model';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.css']
})
export class PerfilComponent implements OnInit {

  currentUser: User;

  constructor(
    private authenticationService: AuthenticationService
  )
  {
    this.currentUser = authenticationService.currentUserValue
  }

  ngOnInit(): void {
  }

}
