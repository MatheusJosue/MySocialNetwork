import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-side-mobile',
  templateUrl: './side-mobile.component.html',
  styleUrls: ['./side-mobile.component.css']
})
export class SideMobileComponent implements OnInit {

  public isMenuCollapsed = true;

  constructor() { }

  ngOnInit(): void {
  }

}
