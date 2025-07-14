import { Component } from '@angular/core';
import { ProductCardComponent } from '../../components/product-card/product-card.component';
import { Product } from '../../models/product.model';
import { AppPageComponent } from '../../components';

@Component({
  selector: 'app-home',
  imports: [ProductCardComponent, AppPageComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  products: Product[] = [];
}
