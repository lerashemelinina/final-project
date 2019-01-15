import { Injectable } from "@angular/core";
import { Car } from "../models/car.model";
import { CarList } from "../models/car-list.model";

@Injectable()
export class OrderDetailsService{
 
    userRole:string="guest";
    availableCars: CarList = new CarList();
    car:Car;
    startRent:Date;
    endRent:Date;
    userName:string;

}