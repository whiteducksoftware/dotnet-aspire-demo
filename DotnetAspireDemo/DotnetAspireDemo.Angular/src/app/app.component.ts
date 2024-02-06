import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, Injectable, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { WeatherForecasts } from '../types/weatherForecast';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Injectable()
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'weather';
  forecasts: WeatherForecasts = [];

  storeForecastResult = signal<string>('Nothing stored yet');

  constructor(private http: HttpClient) {
    http.get<WeatherForecasts>('api/weatherforecast').subscribe((result) => {
      next: this.forecasts = result;
      error: console.error;
    });
  }

  storeForecast(forecast: WeatherForecasts) {
    this.http
      .post<WeatherForecasts>('api/weatherforecast', forecast)
      .subscribe((result) => {
        next: this.storeForecastResult.set('Stored weather forecasts successfully');
        error: console.error;
      });
  }
}
