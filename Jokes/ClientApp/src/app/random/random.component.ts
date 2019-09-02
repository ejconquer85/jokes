import {Component, Inject, OnDestroy} from '@angular/core';
import { interval, Subscription } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'app-random-component',
    templateUrl: './random.component.html'
})
export class RandomComponent implements OnDestroy {

    public subscription: Subscription;
    public myHttp: HttpClient;
    public baseUrl: string;
    public joke: Joke;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.myHttp = http;
        this.baseUrl = baseUrl;
        this.getRandomJoke();
        const source = interval(10000);
        this.subscription = source.subscribe(() => this.getRandomJoke());
    }

    public getRandomJoke(){
         this.myHttp.get<Joke>(this.baseUrl + 'api/jokes/').subscribe(result => {
      this.joke = result;
      }, error => console.error(error));
    }
    ngOnDestroy() {
        this.subscription.unsubscribe();
    }
}

interface Joke {
  id: string;
  joke: string;
  status: number;
}
