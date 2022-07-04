import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/domain/services/authentication.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  formUser : FormGroup;
  error : any;

  constructor(
    private formbuilder: FormBuilder,
    private authenticationService: AuthenticationService,
    private route: ActivatedRoute,
    private router: Router
  )
  {
    this.formUser = this.formbuilder.group({
      username: [null, Validators.required],
      email: [null, Validators.required],
      password: [null, Validators.required],
      confirmPassword: [null, Validators.required]
    });
  }

  ngOnInit(): void {
  }

  onSubmit()
  {
    this.authenticationService.register(this.formUser.value)
    .subscribe({
      next: () => {
      const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
      this.router.navigate(['login']);
    },
    error: (error: any) => {
      this.error = error.error;}
    });
  }

  onCancel() {
    this.router.navigate(['']);
  }
}
