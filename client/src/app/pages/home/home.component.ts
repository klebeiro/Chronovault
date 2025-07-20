import { Component, OnInit } from '@angular/core';

import { AppPageComponent, ProductCardComponent } from '../../components';
import { WatchService } from '../../services';
import { SharedModule } from '../../shared';
import { MostViewedWatchOutputDTO } from '../../dto/watches';
import { NotificationService } from '../../shared/services';

@Component({
  selector: 'app-home',
  imports: [ProductCardComponent, AppPageComponent, SharedModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent implements OnInit {
  isLoading: boolean = true;
  mostViewedWaches: MostViewedWatchOutputDTO[] = [];

  constructor(
    private watchService: WatchService,
    private notificationService: NotificationService
  ) {}

  ngOnInit(): void {
    this.loadMostViewedProducts();
  }

  reload() {
    this.loadMostViewedProducts();
  }

  loadMostViewedProducts(): void {
    this.isLoading = true;

    this.watchService.getMostViewedWaches().subscribe({
      next: (data) => {
        this.mostViewedWaches = data;
      },
      error: (error) => {
        this.notificationService.showErrorNotification({
          title: 'Erro ao carregar os produtos mais populares',
          description: error.error,
        });
      },
      complete: () => {
        this.isLoading = false;
      },
    });
  }
}
