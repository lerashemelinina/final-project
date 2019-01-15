import { Component, OnInit } from '@angular/core';
import { OrderService } from '../shared/services/order.service';
import { OrderList } from '../shared/models/order-list.model';
import { Order } from '../shared/models/order.model';
import { CarService } from '../shared/services/car.service';
import { CarList } from '../shared/models/car-list.model';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html'
})
export class EmployeeComponent implements OnInit {

  allOrders: OrderList;
  carNumber:number=null;
  filteredOrders:Array<Order>;
  userRole:string="employee";
  allCars:CarList;


  constructor(private orderService: OrderService, private myCarService: CarService) { }

  ngOnInit() {
    this.allOrders=this.orderService.allOrders;
    this.allCars = this.myCarService.allCars;
    console.log(this.allCars);
  }

  onSubmit(){
    this.filteredOrders=this.allOrders.orderList.filter((order:Order)=>order.Car.CarNumber==this.carNumber);
  }

}
