import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {map, catchError} from "rxjs/operators";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class DataService {
  baseURL: string;

  constructor(
    public http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.baseURL = baseUrl;
  }

  requestJoke(searchParam: any): Observable<any> {
    return this.http.get(this.baseURL + 'api/jokes/' + searchParam).pipe(
      map(response => {
        return response;
      }),
      catchError(err => {
        throw new Error('error');
      })
    );
  }
}
