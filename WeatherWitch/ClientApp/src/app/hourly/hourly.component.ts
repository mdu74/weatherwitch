import { Component, OnInit } from '@angular/core';
import { ChartDataSets, ChartOptions } from 'chart.js';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { WeatherService } from '../services/weather.service';

@Component({
  selector: 'hourly',
  templateUrl: './hourly.component.html',
  styleUrls: ['./hourly.component.scss']
})
export class HourlyComponent implements OnInit {
  destroy$: Subject<boolean> = new Subject<boolean>();
  hourlyWeatherData: any;
  hourlyLineChartLegend = true;
  hourlyLineChartType = "line";

  hourlyLineChartData: ChartDataSets[] = [
    { data: [], label: "Series A" },
  ];

  hourlyLineChartLabels: any[] = [];
  hourlyLineChartOptions: (ChartOptions) = {
    responsive: true,
  };

  hourlyLineChartColors: any[] = [
    {
      borderColor: "blue",
      backgroundColor: "rgba(0,0,0,0)",
    },
  ];

  hourlyLineChartPlugins = [];
  colorScheme = {
    domain: ["#5AA454", "#E44D25", "#CFC0BB", "#7aa3e5", "#a8385d", "#aae3f5"]
  };
  longitude: number;
  latitude: number;

  constructor(private weatherService: WeatherService) { }

  ngOnInit(): void {
    if ("geolocation" in navigator) {
      navigator.geolocation.getCurrentPosition((position) => {
        let latitude = position.coords.latitude;
        let longitude = position.coords.longitude;
        
        this.weatherService
        .getWeatherForecastHourly(longitude, latitude )
        .pipe(takeUntil(this.destroy$))
        .subscribe((weatherHourly: any[]) => {
          this.hourlyWeatherData = [];
          if(weatherHourly){
            weatherHourly.forEach((data)=>{
              let forecastData = { data: data["hourTemperature"], label: data["hour"] };
              this.hourlyLineChartLabels.push(data["hour"]);  
              this.hourlyLineChartData[0].data.push(data["hourTemperature"]);
              this.hourlyWeatherData.push(forecastData);
            });
          }
        });

      })      
    }    
  }

  onSelect(data): void {
    console.log('Item clicked', JSON.parse(JSON.stringify(data)));
  }

  onActivate(data): void {
    console.log('Activate', JSON.parse(JSON.stringify(data)));
  }

  onDeactivate(data): void {
    console.log('Deactivate', JSON.parse(JSON.stringify(data)));
  }

  ngOnDestroy() {
    this.destroy$.next(true);
    this.destroy$.unsubscribe();
  }
}
