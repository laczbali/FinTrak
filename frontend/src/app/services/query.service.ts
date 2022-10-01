import { Injectable } from '@angular/core';
import { HelperService } from './helper.service';

@Injectable({
  providedIn: 'root'
})
export class QueryService {

  constructor(private helper: HelperService) { }

  private cachedCategories: string[] = [];

  /**
   * @returns A string array of all transaction categories that are in the database
   */
  public async allCategories() {
    if(this.cachedCategories.length != 0) return this.cachedCategories;

    var categories = await this.helper.makeApiGetRequest<string[]>(
      "categories",
      (items) => items as string[],
      (items) => items as string[]
    );

    this.cachedCategories = categories;

    return categories;
  }

}
