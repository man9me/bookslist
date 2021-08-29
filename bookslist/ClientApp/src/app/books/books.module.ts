import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';



@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ]
})
export class BooksModule {

}

export class booksdata {
  public books: book[];
  public mybooks?: book[];
  public mybookslist?: Set<Number>;
  constructor(
    books: book[],
    mybooks?: book[],
    mybookslist?: Set<Number>
  ) {
    this.books = books;
    this.mybooks = mybooks;
    this.mybookslist = mybookslist;    
  }

  public del(book: book) {
    this.mybookslist.delete(book.id);
    this.mybooks.splice(this.mybooks.indexOf(book), 1);
    return this.mybookslist
  }

  public add(book: book) {
    this.mybookslist.add(book.id)
    this.mybooks.push(book)
    return this.mybookslist
  }

  public toggle(book:book)  {
    this.mybookslist.has(book.id) ?
      this.del(book) : this.add(book);
    return this.mybookslist
  }
}

export class book {
  public id: number;
  public title: string;
  public description: string;
  //isRead?: boolean;
  constructor(
    id: number,
    title: string,
    description: string,  
  ) {
    this.id = id;
    this.description = description;
    this.title = title;  
  }
}



/*export interface booksdata {
  books: book[];
  mybooks ?: book[];
  mybookslist ?: Set<Number>;
}

export interface book {
  id: number;
  title: string;
  description: string;
  //isRead?: boolean;
}
*/
