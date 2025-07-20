import { Component, Input } from '@angular/core';

type Size = 'small' | 'medium' | 'large';

@Component({
  selector: 'app-loading-indicator',
  templateUrl: './app-loading-indicator.component.html',
  styleUrl: './app-loading-indicator.component.css',
  standalone: false
})
export class AppLoadingIndicatorComponent {
  @Input('size') size: Size = 'small';
  @Input('customClass') customClass: string | null = null;

  getNumericSize(): number {
    switch (this.size) {
      case 'small':
        return 24;
      case 'medium':
        return 36;
      case 'large':
        return 48;
      default:
        return 36;
    }
  }
}
