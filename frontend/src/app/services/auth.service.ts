import { Injectable } from "@angular/core";
import { HelperService } from "./helper.service";

@Injectable({ providedIn: "root" })
export class AuthService {

    constructor(
        private helper: HelperService
    ) { }

    /** Will be true, if we already checked in with the backend during this session */
    private haveSession = false;

    /**
     * Checks if we have a valid active session with the backend.
     * @returns True if we do, False if we need to log in.
     */
    public async isLoggedIn(): Promise<boolean> {
        // we already checked in, no need to make a request
        if (this.haveSession) return true;

        // check in with the backend. This will also renew the session cookie
        return await this.helper.makeApiGetRequest<boolean>(
            "auth/renew",
            () => {
                this.haveSession = true;
                return true;
            },
            () => { return false; }
        );
    }

    /**
     * Logs in to the backend. Should receive a session cookie.
     * @param password 
     * @returns True if the response was 200 OK, False otherwise.
     */
    public async login(password: string): Promise<boolean> {
        return await this.helper.makeApiPostRequest<boolean>(
            "auth/login",
            `"${password}"`,
            () => {
                this.haveSession = true;
                return true;
            },
            () => { return false; }
        )
    }

}
