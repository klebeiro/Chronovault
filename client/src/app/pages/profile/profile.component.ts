import { Component } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { Router, RouterOutlet } from '@angular/router';

import { AppPageComponent } from '../../components';

@Component({
  selector: 'profile',
  imports: [AppPageComponent, MatIconModule, MatButtonModule, RouterOutlet],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent {
  selectedRoute: string = 'details';

  menuItems = [
    {
      label: 'Perfil',
      icon: 'person',
      route: 'details'
    },
    {
      label: 'Meus Pedidos',
      icon: 'shopping_bag',
      route: 'my-orders'
    }
  ];

  constructor(private router: Router){}

  navigateTo(route: string) {
    this.router.navigate(['profile', route]);
    this.selectedRoute = route;
  }

  getSelectedRoute() {
    return this.selectedRoute;
  }
}
