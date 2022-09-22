import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, firstValueFrom, map, Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HelperService {

  constructor(private http: HttpClient) { }

  /**
   * Sends a POST request to the API. Runs the provided callbacks on the result.
   * 
   * Use as: await makeApiPostRequest(..PARAMS..);
   * @param endpoint Base API URL is not needed. Use as "controller/action"
   * @param data POST request body
   * @param okCallback 
   * @param failCallback 
   * @returns The result of the provided callback, of the provided Type, as an awaitable Promise
   */
  public makeApiPostRequest<Type>(
    endpoint: string,
    data: any,
    okCallback: (response: any) => Type,
    failCallback: (response: any) => Type
  ) : Promise<Type> {

    let baseUrl = environment.apiUrl;
    let request = this.http.post(
      baseUrl + endpoint,
      data,
      {
        headers: { 'Content-Type': 'application/json' },
        withCredentials: true
      }
    ).pipe(
      map<any, Type>(
        resp => okCallback(resp)
      ),
      catchError<any, Observable<Type>>(
        resp => {
          var callbackResult = failCallback(resp);
          return of(callbackResult);
        }
      )
    );

    return firstValueFrom(request);
  }

    /**
   * Sends a GET request to the API. Runs the provided callbacks on the result.
   * 
   * Use as: await makeApiPostRequest(..PARAMS..);
   * @param endpoint Base API URL is not needed. Use as "controller/action"
   * @param okCallback 
   * @param failCallback 
   * @returns The result of the provided callback, of the provided Type, as an awaitable Promise
   */
     public makeApiGetRequest<Type>(
      endpoint: string,
      okCallback: (response: any) => Type,
      failCallback: (response: any) => Type
    ) : Promise<Type> {
  
      let baseUrl = environment.apiUrl;
      let request = this.http.get(
        baseUrl + endpoint,
        {
          withCredentials: true
        }
      ).pipe(
        map<any, Type>(
          resp => okCallback(resp)
        ),
        catchError<any, Observable<Type>>(
          resp => {
            var callbackResult = failCallback(resp);
            return of(callbackResult);
          }
        )
      );
  
      return firstValueFrom(request);
    }

}
