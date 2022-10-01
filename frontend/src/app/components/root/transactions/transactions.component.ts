import { Component, OnInit } from '@angular/core';
import { QueryService } from 'src/app/services/query.service';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrls: ['./transactions.component.scss']
})
export class TransactionsComponent implements OnInit {

  constructor(private query: QueryService) { }

  public categories: string[] = [];

  ngOnInit() {
    this.getCategories();
  }

  private async getCategories() {
    this.categories = await this.query.allCategories();
    // ["none", "salary", "housing", "vacation", "something", "placeholder", "mycategory", "useful", "debug", "food", "hobby"]
  }

}
