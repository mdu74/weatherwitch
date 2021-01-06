import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { AuthenticationService } from '../services/authentication.service';
import { WeatherService } from '../services/weather.service';

@Component({
  selector: 'weather',
  templateUrl: './weather.component.html',
  styleUrls: ['./weather.component.scss']
})
export class WeatherComponent implements OnInit {
  destroy$: Subject<boolean> = new Subject<boolean>();
  title = "Weather Witch";
  todaysWeather: any;
  weatherData: any;

  constructor(private weatherService: WeatherService, 
              private router: Router, 
              private authenticationService: AuthenticationService) {}

  ngOnInit(): void {
    if ("geolocation" in navigator) {
      navigator.geolocation.getCurrentPosition((position) => {
        let latitude = position.coords.latitude;
        let longitude = position.coords.longitude;
        
        this.weatherService
        .getWeatherForecast(longitude, latitude)
        .pipe(takeUntil(this.destroy$))
        .subscribe(weather => {
          this.todaysWeather = weather;
        });
      })      
    } 
  }
  
  logOut(){
    this.authenticationService
        .logOut()
        .pipe(takeUntil(this.destroy$))
        .subscribe(res => {
          if(res == null) this.router.navigate(["login"]);
        })
  }

  ngOnDestroy() {
    this.destroy$.next(true);
    this.destroy$.unsubscribe();
  }
}
