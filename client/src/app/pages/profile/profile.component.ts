import { Component, OnInit } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';

import { AppPageComponent } from '../../components';

@Component({
  selector: 'profile',
  imports: [AppPageComponent, MatIconModule, MatButtonModule, RouterOutlet],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss',
})
export class ProfileComponent implements OnInit {
  selectedRoute: string = 'details';

  menuItems = [
    {
      label: 'Perfil',
      icon: 'person',
      route: 'details',
    },
    {
      label: 'Meus Pedidos',
      icon: 'shopping_bag',
      route: 'my-orders',
    },
  ];

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.addRouteChangeListener();

    this.selectedRoute = this.checkSelectedRoute(this.router.url);
  }

  addRouteChangeListener() {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        console.log(event.urlAfterRedirects);
        this.selectedRoute = this.checkSelectedRoute(event.urlAfterRedirects);
      }
    });
  }

  navigateTo(route: string) {
    this.router.navigate(['profile', route]);
    this.selectedRoute = route;
  }

  getSelectedRoute() {
    return this.selectedRoute;
  }

  checkSelectedRoute(currentUrl: string) {
    console.log(currentUrl);

    if (
      currentUrl.includes('my-orders') ||
      currentUrl.includes('order-details')
    ) {
      return 'my-orders';
    }

    return 'details';
  }
}
