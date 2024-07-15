// login.component.ts
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html'
})
export class LoginComponent {
    username: string = "";
    password: string = "";
    error: string = "";

    constructor(private router: Router, private authService: AuthService) { }

    login() {
        this.authService.login(this.username, this.password)
            .subscribe({
                next: () => {
                    this.router.navigate(['/']);
                },
                error: err => {
                    this.error = 'Username or password is incorrect';
                }
            });
    }
}
