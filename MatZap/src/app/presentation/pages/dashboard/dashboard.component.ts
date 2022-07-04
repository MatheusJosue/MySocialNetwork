import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/domain/services/authentication.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  public isMenuCollapsed = true;

  constructor(
    private authenticationService: AuthenticationService,
  ) { }

  ngOnInit(): void {
  }

  onLogout() {

    this.authenticationService.logout() ;
    {
      window.location.reload();
    }
  }
}
