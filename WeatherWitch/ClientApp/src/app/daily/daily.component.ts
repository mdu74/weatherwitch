import { Component, OnInit } from '@angular/core';
import { ChartDataSets, ChartOptions } from 'chart.js';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { WeatherService } from '../services/weather.service';

@Component({
  selector: 'daily',
  templateUrl: './daily.component.html',
  styleUrls: ['./daily.component.scss']
})
export class DailyComponent implements OnInit {
  destroy$: Subject<boolean> = new Subject<boolean>();
  dailyWeatherData: any;
  dailyLineChartLegend = true;
  dailyLineChartType = "line";

  dailyLineChartData: ChartDataSets[] = [
    { data: [], label: "Series A" },
  ];

  dailyLineChartLabels: any[] = [];
  dailyLineChartOptions: (ChartOptions) = {
    responsive: true,
  };

  dailyLineChartColors: any[] = [
    {
      borderColor: "blue",
      backgroundColor: "rgba(0,0,0,0)",
    },
  ];

  dailyLineChartPlugins = [];
  colorScheme = {
    domain: ["#5AA454", "#E44D25", "#CFC0BB", "#7aa3e5", "#a8385d", "#aae3f5"]
  };

  constructor(private weatherService: WeatherService) { }

  ngOnInit(): void {
    navigator.geolocation.getCurrentPosition((position) => {
      let latitude = position.coords.latitude;
      let longitude = position.coords.longitude;
      
      this.weatherService
      .getWeatherForecastDaily(longitude, latitude)
      .pipe(takeUntil(this.destroy$))
      .subscribe(weatherHourly => {
        this.dailyWeatherData = [];
        weatherHourly.forEach((data)=>{              
          let forecastData = { data: data["daysTemperature"], label: data["day"] };
          this.dailyLineChartLabels.push(data["day"]);  
          this.dailyLineChartData[0].data.push(data["daysTemperature"]);
          this.dailyWeatherData.push(forecastData);
        });
      });
    })
    
   
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
