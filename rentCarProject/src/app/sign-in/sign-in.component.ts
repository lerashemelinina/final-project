import { Component, OnInit } from '@angular/core';
import { UserService } from '../shared/services/user.service';
import { User } from '../shared/models/user.model';
import { Router } from '../../../node_modules/@angular/router';
import { OrderDetailsService } from '../shared/services/orderDetails.service';
import { Car } from '../shared/models/car.model';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html'
})
export class SignInComponent implements OnInit {

email:string;
password:string;
localUser:User={
  FullName:undefined,
  IdentityNumber:undefined,
  UserName:undefined,
  BirthDate:undefined,
  IsMale:undefined,
  Email:undefined,
  Password:undefined,
  UserRole:undefined,
  Image:undefined
};

car:Car;

  constructor(private myUserService: UserService, private router:Router,
              private orderDetailsService: OrderDetailsService) { }


  ngOnInit(){
  this.car=this.orderDetailsService.car;
  }



  onSubmit(){
  this.myUserService.getUserByEmailAndPassword(this.email,this.password).subscribe(
                                              (user:User)=>{this.localUser=user,
                                                            this.orderDetailsService.userName=this.localUser.UserName,
                                                            this.orderDetailsService.userRole=this.localUser.UserRole,
                                                            this.navigateUser()}
  );}                             
  

  navigateUser(){
  if(this.localUser.UserRole=="manager")
      this.router.navigate(['/manager']);
  else if(this.localUser.UserRole=="employee")
      this.router.navigate(['/employee']);
  else if(this.localUser.UserRole=="customer")
      this.router.navigate(['/addOrder']); 
  else 
      this.router.navigate(['/home']);                                 
  }
}
