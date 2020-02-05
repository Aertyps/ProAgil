import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/_services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  titulo = 'Login';
  model: any = {};

  constructor(private authService: AuthService,
              public router: Router,
              private toastr: ToastrService) {
  }

  ngOnInit() {
    if (localStorage.getItem('token') !== null) {
      this.router.navigate(['/dashbord']);
    }
  }

  login() {
   this.authService.login(this.model)
   .subscribe(
     () => {
       this.router.navigate(['/dashbord']);
       this.toastr.success('Logado');
     }, error => {
       this.toastr.error('Falha ao Logar');
     }
   );
  }
}
