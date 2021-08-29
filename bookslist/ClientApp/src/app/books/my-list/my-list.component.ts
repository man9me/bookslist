/// <reference path="../books-list/books-list.component.ts" />
import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { book, booksdata } from '../../books/books.module'

import { BooksService } from '../../books/books.service';
@Component({
  selector: 'app-my-list',
  templateUrl: './my-list.component.html',
  styleUrls: ['./my-list.component.css']
})
export class MyListComponent implements OnInit, OnDestroy {
  sub: Subscription;
  private booksData = new booksdata([], [],);

  constructor(private BooksService: BooksService) {

    }  ;
  async ngOnInit() {
    this.sub = this.BooksService.booksChanged.subscribe(
      (books: booksdata) => {
        this.booksData = books;
      });
    await this.BooksService.fetchsMy()
    this.booksData = await this.BooksService.getBooksData();
    console.log("on it fetch", this.booksData);
  }
  ngOnDestroy() {
    this.sub.unsubscribe();
  }
}
