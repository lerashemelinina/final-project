import { Component, OnInit } from '@angular/core';
import { Order } from '../shared/models/order.model';
import { OrderService } from '../shared/services/order.service';
import { UserService } from '../shared/services/user.service';
import { CarService } from '../shared/services/car.service';
import { UserList } from '../shared/models/user-list.model';
import { Router } from '@angular/router';
import { Car } from '../shared/models/car.model';
import { OrderDetailsService } from '../shared/services/orderDetails.service';

@Component({
  selector: 'app-add-order',
  templateUrl: './add-order.component.html'
})
export class AddOrderComponent implements OnInit {

  availableCars: Car[];
  allUsers: UserList;
  actionMsg :string;
  selectedUser:string = undefined;
  selectedCar:Car;
  currentDate:Date = new Date();
  userNameError:boolean=false;
  selectedCarError:boolean=false;
  userRole:string;

  newOrder:Order={
    "StartRent": undefined,
    "EndRent":undefined,
    "ReturnDate":undefined,
    "User":undefined,
    "Car":undefined
};
  
constructor(private myOrderService: OrderService, 
            private myUserService: UserService, 
            private myCarService: CarService, 
            private orderDetailsService: OrderDetailsService,
            private router: Router) {

              if(this.orderDetailsService.startRent){
                this.newOrder.StartRent=this.orderDetailsService.startRent;
                this.newOrder.EndRent=this.orderDetailsService.endRent;
                this.selectedUser=this.orderDetailsService.userName;
              }

              if(this.orderDetailsService.userRole){
              this.userRole=this.orderDetailsService.userRole;
              this.selectedUser=this.orderDetailsService.userName;
              }

             console.log(this.userRole);
             }
  
                
    ngOnInit() {
      this.allUsers=this.myUserService.allUsers;
    }

    selectAvailableCars(){
      this.selectedCar=undefined;
      if(this.newOrder.StartRent!=undefined&&this.newOrder.EndRent!=undefined)
      this.myCarService.searchAvailableCars(this.newOrder.StartRent, this.newOrder.EndRent,
                                             (cars:Car[])=>{this.availableCars=cars;});
    }


    validateUserName(value){
      if (value=='default')
          this.userNameError=true;
      else
          this.userNameError=false;
    }

    selectCar(carNumber:number) {

      this.validateUserName(this.selectedUser);
      if(this.userNameError)
      return;

      let callback=(bool:boolean)=>{this.actionMsg = (bool) ? "action success" : "action fail";}                           
      this.newOrder.User=this.allUsers.userList.find(x=>(x.UserName==this.selectedUser));


      if(this.availableCars)
       this.newOrder.Car=this.availableCars.find(x=>(x.CarNumber==carNumber));
      else
       this.newOrder.Car=this.orderDetailsService.car;


      this.myOrderService.addOrder(this.newOrder,callback);
      
    }

    onBack(){
      if(this.userRole=='manager')
      this.router.navigate(['/manager/orderList']);
      else if (this.userRole=="customer")
      this.router.navigate(['/addOrder']);
    }

}


