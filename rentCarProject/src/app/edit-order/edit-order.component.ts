import { Component, OnInit } from '@angular/core';
import { Order } from '../shared/models/order.model';
import { OrderService } from '../shared/services/order.service';
import { CarService } from '../shared/services/car.service';
import { UserService } from '../shared/services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserList } from '../shared/models/user-list.model';
import { CarList } from '../shared/models/car-list.model';


@Component({
  selector: 'app-edit-order',
  templateUrl: './edit-order.component.html' 
})
export class EditOrderComponent implements OnInit {

  actionMsg :string;
  allUsers: UserList;
  allCars: CarList;
  localCarNumber:number;
  localStartRent:Date =new Date();
  userRole:string;
  totalCost:number;
  totalDays:number;
  oneDay:number = 1000 * 60 * 60 * 24;
  startDateArray:Array<string>=[];
  returnDateArray:Array<string>=[];
  tempStartDate:Date;
  tempReturnDate:Date;

  testDate:Date=new Date();
  
  localOrder:Order={
    "StartRent": new Date(),
    "EndRent" : new Date(),
    "ReturnDate": new Date(),
    "User":undefined,
    "Car":undefined
  };
  
    constructor(private myOrderService: OrderService, private myCarService: CarService, 
                private myUserService: UserService, private myActivatedRoute: ActivatedRoute,
                private router:Router) { }
  
    ngOnInit() {
      this.allUsers=this.myUserService.allUsers;
      this.allCars=this.myCarService.allCars;

      this.myActivatedRoute.params.subscribe(params => {
        this.localStartRent=params.startRent;
        this.localCarNumber=params.carNumber;
        this.userRole=params.userRole;

        if(params.carNumber&&params.startRent){
          this.myOrderService.getOrderForEdit(params.startRent,params.carNumber, (order:Order)=>{this.localOrder=order;});
        }
      });
    }
  
    saveChanges(){
      let callback=(bool:boolean)=>{this.actionMsg = (bool) ? "action success" : "action fail";} 
      this.myOrderService.editOrder(this.localOrder, this.localStartRent, this.localCarNumber, callback);
    }

    onBack(){
      if (this.userRole=="manager")
      this.router.navigate(['/manager/orderList']);

      if(this.userRole=="employee")
      this.router.navigate(['employee']);
    }

    calculateTotalCost(){

      this.startDateArray=(this.localOrder.StartRent).toString().slice(0,10).split('-');
      this.returnDateArray=(this.localOrder.ReturnDate).toString().slice(0,10).split('-');

      this.tempStartDate=new Date(parseInt(this.startDateArray[0]),parseInt(this.startDateArray[1])-1,parseInt(this.startDateArray[2]));
      this.tempReturnDate=new Date(parseInt(this.returnDateArray[0]),parseInt(this.returnDateArray[1])-1,parseInt(this.returnDateArray[2]));
      
      this.totalDays=Math.floor((this.tempReturnDate.getTime()-this.tempStartDate.getTime())/(1000*3600*24));
      
      this.totalCost=this.totalDays*this.localOrder.Car.CarType.Price;

    };

}
