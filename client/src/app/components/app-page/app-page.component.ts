import { Component } from '@angular/core';
import { NavbarComponent } from '../navbar/navbar.component';
import { FooterComponent } from '../footer/footer.component';

@Component({
  selector: 'app-page',
  imports: [NavbarComponent, FooterComponent],
  templateUrl: './app-page.component.html',
  styleUrl: './app-page.component.scss'
})
export class AppPageComponent {
}
