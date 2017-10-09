import {Injectable} from "@angular/core";
import { Observable } from 'rxjs/Observable';
import { Http, Response } from '@angular/http'


import { API } from './api.endpoint'
import { IWeather } from "./weather.model";

@Injectable()
export class iAssetService {
    constructor(private http: Http) {}

    getCities(country:string):Observable<string[]> {

        var countryCityUrl = `${API.baseUrl}/${country}/cities`;

        return this.http.get(countryCityUrl).map((response: Response) => {
            return <string[]>response.json();
        }).catch(this.handleError);
    }

      getCityWeather(country:string, city:string):Observable<IWeather> {
        
        var cityWeatherUrl = `${API.baseUrl}/${country}/${city}/weather`;

        return this.http.get(cityWeatherUrl).map((response: Response) => {
            return <IWeather>response.json();
        }).catch(this.handleError);
     }


    private handleError(error: Response) {
        return Observable.throw(error.statusText);
      }
}