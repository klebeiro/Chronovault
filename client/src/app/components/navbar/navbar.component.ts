import { Component, Input, OnInit } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { NavigationEnd, Router, RouterModule } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { Cart } from '../../shared/types';

import { CartService } from '../../shared/services';
import { AuthService } from '../../services';

@Component({
  selector: 'app-navbar',
  imports: [MatIconModule, RouterModule, MatSidenavModule, CommonModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
})
export class NavbarComponent implements OnInit {
  @Input('fixedElementsMarginActive') fixedElementsMarginActive: boolean = true;
  showProfileButton: boolean = false;
  showLogoutButton: boolean = false;

  cart: Cart = {
    items: [],
  };

  constructor(
    private cartService: CartService,
    private router: Router,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.addCartSubscription();
    this.addRouterChangeSubscription();
    this.addAuthCredentialsChangeSubscription();
  }

  addCartSubscription() {
    this.cartService.getCart().subscribe((cart) => {
      this.cart = cart;
    });
  }

  addRouterChangeSubscription() {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.checkActiveAction(event.url);
      }
    });
  }

  checkActiveAction(currentUrl: string = '') {
    if (currentUrl.toLowerCase().includes('profile')) {
      this.showProfileButton = false;
    } else {
      if (this.authService.getIsAuthenticated()) {
        this.showLogoutButton = true;
      } else {
        this.showLogoutButton = false;
      }
    }
  }

  openCartDrawer() {
    this.cartService.openCartDrawer();
  }

  addAuthCredentialsChangeSubscription() {
    this.authService.getAuthCredentials$().subscribe((credentials) => {
      if (credentials === null) {
        this.showLogoutButton = false;
      } else {
        this.showLogoutButton = true;
      }
    });
  }

  logout() {
    this.authService.removeAuthCredentials();
    this.router.navigate(['/']);
  }

  clickProfile() {
    if (this.authService.getIsAuthenticated()) {
      this.router.navigate(['profile']);
      return;
    }

    this.router.navigate(['login']);
  }
}
