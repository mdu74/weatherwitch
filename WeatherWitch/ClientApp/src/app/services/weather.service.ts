import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable, of } from "rxjs";
import { catchError, map } from "rxjs/operators";
import { environment } from "../../environments/environment";

@Injectable({
  providedIn: "root"
})
export class WeatherService {

  constructor(private http: HttpClient) { }

  getWeatherForecast(longitude: number, latitude: number){
    return this.http.put<any[]>("api/WeatherForecast/GetWeatherForecast", { Lon: longitude, Lat: latitude })
                    .pipe(
                          map(res => res), 
                          catchError(this.handleError<any[]>("Get Weather Forecast ", []))
                        );
  }

  getWeatherForecastHourly(longitude: number, latitude: number){
    return this.http.put<any[]>("api/WeatherForecast/GetWeatherForecastHourly", { Lon: longitude, Lat: latitude })
                    .pipe(
                          map(res => res), 
                          catchError(this.handleError<any[]>("Get Weather Forecast Hourly ", []))
                        );
  }

  getWeatherForecastDaily(longitude: number, latitude: number){
    return this.http.put<any[]>("api/WeatherForecast/GetWeatherForecastDaily", { Lon: longitude, Lat: latitude })
                    .pipe(
                          map(res => res), 
                          catchError(this.handleError<any[]>("Get Weather Forecast Daily ", []))
                        );
  }

  private handleError<T>(operation: string, result?: T) {
    return (error: any): Observable<T> => {
      console.error("Service error while ", operation, ": ", error);
      return of(result as T);
    };
  }
}
