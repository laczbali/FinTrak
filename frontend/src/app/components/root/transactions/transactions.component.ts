import { Component, OnInit } from '@angular/core';
import { Transaction } from 'src/app/models/Transaction';
import { QueryService } from 'src/app/services/query.service';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrls: ['./transactions.component.scss']
})
export class TransactionsComponent implements OnInit {

  constructor(
    private query: QueryService
  ) { }

  public categories: string[] = [];
  public transactions: Transaction[] = [{
    id: "ed6141ae-251e-4cc7-987c-a540e6c581cc",
    amount: 46,
    description: "Chips Potato Swt Chilli Sour",
    creationTime: new Date("4/24/2022"),
    category: "housing"
  }, {
    id: "4d0e554c-29c3-4850-b668-62bd3c86c3d9",
    amount: 61,
    description: "Cookie Dough - Oatmeal Rasin",
    creationTime: new Date("10/21/2021"),
    category: "vacation"
  }, {
    id: "9c10c00f-867f-44f3-b532-2b5f5b5746d5",
    amount: 70001.3,
    description: "Squash - Pepper",
    creationTime: new Date("5/12/2022"),
    category: "none"
  }, {
    id: "9ac316fb-6ba5-451d-96ed-ed4add92157a",
    amount: 2,
    description: "Vol Au Vents",
    creationTime: new Date("1/27/2022"),
    category: "housing"
  }, {
    id: "5fcd512c-ca6a-48f1-a5e0-60d9b856d713",
    amount: 30,
    description: "Chicken - Thigh, Bone In",
    creationTime: new Date("2/9/2022"),
    category: "salary"
  }, {
    id: "b4f10792-7642-4ec1-ae68-d92a5832f743",
    amount: 81,
    description: "Potatoes - Instant, Mashed",
    creationTime: new Date("3/12/2022"),
    category: "housing"
  }, {
    id: "206e576a-08b6-41ce-852b-a404339cabb7",
    amount: 17,
    description: "Wine - Two Oceans Sauvignon",
    creationTime: new Date("9/18/2022"),
    category: "vacation"
  }, {
    id: "f5055947-9d66-4be3-8dda-bc9588887f70",
    amount: 39,
    description: "Beef - Sushi Flat Iron Steak",
    creationTime: new Date("2/24/2022"),
    category: "salary"
  }, {
    id: "a0ad77a8-c235-4bc3-a05d-6492bad86fef",
    amount: 88,
    description: "Clementine",
    creationTime: new Date("9/7/2022"),
    category: "none"
  }, {
    id: "cc0b7b38-6d23-406a-93f4-a21738a9b366",
    amount: 77,
    description: "Relish",
    creationTime: new Date("3/31/2022"),
    category: "vacation"
  }].sort((a, b) => { if (a.creationTime > b.creationTime) return 1; return -1; });

  ngOnInit() {
    this.getCategories();
  }

  private async getCategories() {
    this.categories = await this.query.allCategories();
    // ["none", "salary", "housing", "vacation", "something", "placeholder", "mycategory", "useful", "debug", "food", "hobby"]
  }

}
