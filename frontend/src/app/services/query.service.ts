import { Injectable } from '@angular/core';
import { HelperService } from './helper.service';

@Injectable({
  providedIn: 'root'
})
export class QueryService {

  constructor(private helper: HelperService) { }

  public allCategories() {
    
  }

}
