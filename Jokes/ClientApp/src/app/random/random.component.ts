import {Component, Inject, OnDestroy, OnInit} from '@angular/core';
import {interval, Subscription} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {DataService} from "../data.service";

@Component({
  selector: 'app-random-component',
  templateUrl: './random.component.html'
})
export class RandomComponent implements OnDestroy, OnInit {

  subscription: Subscription;
  joke: Joke;
  searching: boolean = false;

  constructor(
    public _dataService: DataService
  ) {

    const source = interval(10000);
    this.subscription = source.subscribe(() => this.getRandomJoke());
  }

  ngOnInit(): void {
    this.getRandomJoke();
  }

  public getRandomJoke() {
    this.searching = true;
    this._dataService.requestJoke('').subscribe(result => {
      this.searching = false;
      this.joke = result;
    });
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
