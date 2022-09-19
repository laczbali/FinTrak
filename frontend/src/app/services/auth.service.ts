import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError, map, Observable, of, tap } from "rxjs";

@Injectable({ providedIn: "root" })
export class AuthService {

    constructor(private http: HttpClient) { }

    public get isLoggedIn(): boolean {
        // TODO auth validation
        // if we don't have a session cookie, return false
        // if we have a session cookie, send a validation request to the backend (auth/renew)
        return true;
    }

    public login(password: string): Observable<boolean> {
        // TODO get URL from env
        return this.http.post("https://localhost:54966/auth/login", `"${password}"`, { headers: { 'Content-Type': 'application/json' } })
            .pipe(
                map(
                    resp => {
                        return true;
                    }
                ),
                catchError(
                    resp => {
                        return of(false);
                    }
                )
            );
    }

}
