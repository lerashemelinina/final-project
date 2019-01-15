import { Component, OnInit } from '@angular/core';
import { CarService } from '../shared/services/car.service';
import { CarList } from '../shared/models/car-list.model';
import { OrderDetailsService } from '../shared/services/orderDetails.service';
import { Car } from '../shared/models/car.model';

@Component({
  selector: 'app-car-list',
  templateUrl: './car-list.component.html'
})
export class CarListComponent implements OnInit {

  allCars: CarList = new CarList();
  actionMsg: string;
  user:string;

  constructor(private myCarService: CarService, private orderDetailsService: OrderDetailsService){
    this.user=orderDetailsService.userRole;
   }


  ngOnInit() {

    if (this.orderDetailsService.availableCars.carList[0])
        this.allCars.carList=this.orderDetailsService.availableCars.carList;
    
    else
        this.allCars = this.myCarService.allCars;

  }

  deleteCar(carNumber: number) {
    this.myCarService.deleteCar(carNumber).subscribe(
      (res) => {
        if (res) {
          this.myCarService.getCars();
        }
        this.actionMsg = (res) ? "delete success" : "delete fail";
      });   
  }

  saveCar(car:Car){

    this.orderDetailsService.car=car;
    
  }

}
