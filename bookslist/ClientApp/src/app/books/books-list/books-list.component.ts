import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subject, Subscription } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { AuthorizeService } from '../../../api-authorization/authorize.service';
import { book, booksdata } from '../../books/books.module'

import { BooksService} from '../../books/books.service';


@Component({
  selector: 'app-books-list',
  templateUrl: './books-list.component.html',
  styleUrls: ['./books-list.component.css']
})


export class BooksListComponent implements OnInit, OnDestroy {
  sub: Subscription;

  private booksData = new booksdata([], [],);

  //public isAuthenticatedObservable: Observable<boolean>;

  isAuthenticatedsub: Subscription;
  public isAuthenticated: boolean;


  constructor(private authorizeService: AuthorizeService, private BooksService: BooksService) {
    this.isAuthenticated = true;
    this.authorizeService.isAuthenticated().subscribe(d => this.isAuthenticated = d);
    
  }


  async readToggle(book: book) {
    console.log(book);
   /* this.booksData.mybookslist.has(book.id) ?
      this.booksData.mybookslist.delete(book.id) : this.booksData.mybookslist.add(book.id);*/
     this.booksData.toggle(book);
    this.BooksService.booksChanged.next(this.booksData);
    //await this.BooksService.save();
  }
 

  async ngOnInit() {
    console.log("onoit2");
    this.sub = this.BooksService.booksChanged.subscribe(
      (books: booksdata) => {
        this.booksData = books;
      });
    if (this.isAuthenticated) { await this.BooksService.fetchsMy() }
    this.booksData = await this.BooksService.getBooksData();
    console.log("on it",this.booksData);
 
  }
  
  async ngOnDestroy() {
    await this.BooksService.save();
    console.log("destroo");
    this.sub.unsubscribe();
  }
}


