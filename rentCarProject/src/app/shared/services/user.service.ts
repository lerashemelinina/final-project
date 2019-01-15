import { UserList } from "../models/user-list.model";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import { Injectable } from "@angular/core";
import { User } from "../models/user.model";
import { Observable} from "rxjs";
import {catchError, tap} from 'rxjs/operators';

@Injectable()
export class UserService {
    private link="http://localhost:63174/api/user";
   
    allUsers:UserList=new UserList();

    constructor(private myHttpClient: HttpClient) { 
        this.getUsers();
    }

    getUsers(): void {
        this.myHttpClient.get(this.link)
            .subscribe((x: Array<User>) => { this.allUsers.userList = x; });
    }

    getUserForEdit(userName:string,callback:(user:User)=>void): void {
        this.myHttpClient.get(`${this.link}?userName=${userName}`)
            .subscribe((x:User) => { callback(x);});
    }


    // getUserByEmailAndPassword(email:string,password:string,callback:(user:User)=>void): Observable<User> {
    //     return this.myHttpClient.get<User>(`${this.link}?email=${email}&password=${password}`)
    //         .pipe(
    //          tap(data=>console.log(data),
    //          catchError(this.handleError)));      
    // }

    // getUserByEmailAndPassword(email:string,password:string,callback:(user:User)=>void): void {
    //     this.myHttpClient.get(`${this.link}?email=${email}&password=${password}`)
    //         .pipe(
    //             catchError(this.handleError)
    //         )
    //         .subscribe((x:User) => { callback(x);});    
    // }

    getUserByEmailAndPassword(email:string,password:string): Observable<User> {

        return this.myHttpClient.get<User>(`${this.link}?email=${email}&password=${password}`);
                                            // .pipe(catchError(this.handleError));
                                                
    }

    deleteUser(userName:string):Observable<boolean>{
        let apiUrl:string=`${this.link}?userName=${userName}`;
        return this.myHttpClient.delete<boolean>(apiUrl);
    }


    addUser(user:User,callback:(bool:boolean)=>void): void {
        this.myHttpClient.post<boolean>(this.link,JSON.stringify(user), { headers: {"content-type": "application/json" }}).subscribe(()=>{this.getUsers(); callback(true);},
        ()=>{callback(false)});
        
    }

    editUser(user:User,userName:string,callback:(bool:boolean)=>void): void {

        this.myHttpClient.put<boolean>(`${this.link}?userName=${userName}&`,JSON.stringify(user), { headers: {"content-type": "application/json" }}).subscribe(()=>{this.getUsers(); callback(true);},
        ()=>{callback(false)});

    }

    handleError(err:HttpErrorResponse){
      console.log("im in the errror");
      let errorMessage:string = '';

      if(err.error instanceof ErrorEvent)
      errorMessage=`An error occured: ${err.error.message}`;
      else
      errorMessage=`Server returned code:${err.status}, error message is: ${err.message}`;
    
      console.log(errorMessage);



      return new Error(errorMessage);
    //   return errorMessage;
    }


}