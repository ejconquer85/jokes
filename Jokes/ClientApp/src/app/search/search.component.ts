import {Component} from '@angular/core';
import {DataService} from "../data.service";

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html'
})
export class SearchComponent {
  jokes: Jokes;
  searchTerm:string = '';
  searching: boolean = false;

  constructor(
    public _dataService: DataService
  ) {
  }

  public search() {
    this.searching = true;
    this._dataService.requestJoke(this.searchTerm).subscribe(result => {
      this.searching = false;
      this.jokes = result;
    });
  }

  public makeBold(joke: string) {
    return joke.replace(this.searchTerm, '<b><i>' + this.searchTerm + '</i></b>');
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
