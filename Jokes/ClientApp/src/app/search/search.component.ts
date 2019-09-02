import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Http } from '../../../node_modules/@angular/http';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html'
})
export class SearchComponent {
    public jokes: Jokes;
    public searchTerm: string;
    public myHttp: HttpClient;
    public baseUrl: string;


  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
      this.myHttp = http;
      this.baseUrl = baseUrl;
      this.searchTerm = '';
  }

  public search() {
    this.myHttp.get<Jokes>(this.baseUrl + 'api/jokes/' + this.searchTerm).subscribe(result => {
      this.jokes = result;
    }, error => console.error(error));
  }

    public makeBold(joke: string) {
      return joke.replace(this.searchTerm, '<b><i>'+this.searchTerm+'</i></b>');
    }

  public onKeydown(event) {
  if (event.key === "Enter") {
      this.search();
  }
}
}

interface Jokes {
  short: string[];
  medium: string[];
  long: string[];
}
