import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { BehaviorSubject, Observable, of } from "rxjs";
import { catchError, map } from "rxjs/operators";
import { environment } from "../../environments/environment";
import Swal from "sweetalert2";

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private getCurrentUser$: BehaviorSubject<any>;
  public currentUser$: Observable<any>;

  constructor(private http: HttpClient) { 
    this.getCurrentUser$ = new BehaviorSubject<any>(JSON.parse(localStorage.getItem("currentUser")));
    this.currentUser$ = this.getCurrentUser$.asObservable();
  }
  
  public get currentUserValue(): any {
    return this.getCurrentUser$.value;
  }

  logIn(user: any){
    return this.http.post<any[]>("api/User/LogIn", user)
                    .pipe(
                          map(res => {
                            localStorage.setItem("currentUser", JSON.stringify(user));
                            this.getCurrentUser$.next(res);
                            return res;
                          }), 
                          
                          catchError(this.handleError<any[]>("Log In ", null))
                        );
  }

  logOut(){
    localStorage.removeItem("currentUser");
    this.getCurrentUser$.next(null);
    return this.getCurrentUser$;
  }

  private handleError<T>(operation: string, result?: T) {
    return (error: string): Observable<T> => {
      Swal.fire("Oops...", error + " please try Password123", "error");
      console.error("Service error while ", operation, ": ", error);
      return of(result as T);
    };
  }
}
