import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, Subject, Subscription } from 'rxjs';
import { tap } from 'rxjs/operators';
import { AuthorizeService } from '../../api-authorization/authorize.service';
import { book, booksdata } from '../books/books.module'


@Injectable({
  providedIn: 'root'
})
export class BooksService {

 

  
  //mybookss = new Subject<book[]>();
  //auth = new Subject<boolean>();
  //mybookslists = new Subject<Set<Number>>();
  //subb: Subscription;
  //subm: Subscription;
  //suba: Subscription;
  //subl: Subscription;
  
 // public books: book[] = [];
 // public mybooks: book[] = [];
  //public mybooksO: book[] = [];
 // public mybooksl = new Set<Number>();

  booksChanged = new Subject<booksdata>();

  //public isAuthenticatedObservable: Observable<boolean>;
  isAuthenticatedsub: Subscription; 
  public isAuthenticated: boolean;

  checked: boolean;

  private booksData = new booksdata([], [], new Set<Number>());

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private authorizeService: AuthorizeService) {
    this.isAuthenticated = true;
    this.authorizeService.isAuthenticated().subscribe(d => this.isAuthenticated = d);
  }


  async fetchsMy() {
    if (this.isAuthenticated && this.booksData.mybooks.length == 0) {
      console.log("fetching m")
      await this.http.get<book[]>(this.baseUrl + 'api/UsersToBooks').
        pipe(
          tap(r => {
            this.booksData.mybooks = r;
            //r.forEach(b => this.booksData.mybookslist.add(b.id));
             this.myList();
            this.booksChanged.next(this.booksData);
          }))
        .subscribe();
    }
      return this.booksData;
    }
      
  myList() {
    console.log(",ylisttt", !this.booksData.mybooks, !this.booksData.mybookslist, this.booksData.mybooks.length != 0)
    if (this.booksData.books.length != 0 && this.booksData.mybooks.length != 0) {
      this.booksData.mybooks.forEach(b => this.booksData.mybookslist.add(b.id))
      this.booksChanged.next(this.booksData);
    }
  }


  async fetchs() {

    this.http.get<book[]>(this.baseUrl + 'api/Books1')
      .pipe()
      .subscribe(r => { this.booksData.books = r; console.log(r); this.booksChanged.next(this.booksData); });
    //.pipe(tap(r => this.bookss.next(r.map(x => { x.isRead = true; return x }))))

    //this.isAuthenticatedsub = await this.authorizeService.isAuthenticated().subscribe(d => this.auth.next(d));

    console.log("fetch ", (this.booksData.books==[]), this.booksData.books)
    await this.fetchsMy();

    return this.booksData;
    

  }
  async save() {
    console.log("savee  ", Array.from(this.booksData.mybookslist))
    this.http.post(this.baseUrl + 'api/UsersToBooks', Array.from(this.booksData.mybookslist)).subscribe();
   }


  async getBooksData() {
    let b = (this.booksData.books.length == 0);
    console.log("data  ", b, "asdasd", this.booksData.books);
    return b ? await this.fetchs() : this.booksData  ;
  };


  async read(books: book[], mybooks: book[]) {
    let i: number;
    for (let book of mybooks) {
      i = book.id - 1;
      //books[i].isRead = true;
    }
    return books;
  }

  async ngOnInit() {
  }


  ngOnDestroy() {

 
  }
}
