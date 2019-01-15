import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Car } from "../models/car.model";
import { CarList } from "../models/car-list.model";


@Injectable()
export class CarService {
    private link="http://localhost:63174/api/car";
   
    allCars:CarList=new CarList();
    // availableCars:CarList=new CarList();

    constructor(private myHttpClient: HttpClient) { 
        this.getCars();
    }

    getCars(): void {
        this.myHttpClient.get(this.link)
            .subscribe((x: Array<Car>) => { this.allCars.carList = x;});
    }

    getAllCars():Observable <Car[]>{
        return this.myHttpClient.get<Car[]>(this.link);
    }

    getCarForEdit(carNumber:number,callback:(car:Car)=>void): void {
        this.myHttpClient.get(`${this.link}?carNumber=${carNumber}`)
            .subscribe((x:Car) => { callback(x);});
    }

    deleteCar(carNumber:number):Observable<boolean>{
        let apiUrl:string=`${this.link}?carNumber=${carNumber}`;
        return this.myHttpClient.delete<boolean>(apiUrl);
    }


    addCar(car:Car,callback:(bool:boolean)=>void): void {
        this.myHttpClient.post<boolean>(this.link,JSON.stringify(car), { headers: {"content-type": "application/json" }}).subscribe(()=>{this.getCars(); callback(true);},
        ()=>{callback(false)});
        
    }

    editCar(car:Car,carNumber:number,callback:(bool:boolean)=>void): void {
        this.myHttpClient.put<boolean>(`${this.link}?carNumber=${carNumber}`,JSON.stringify(car), { headers: {"content-type": "application/json" }}).subscribe(()=>{this.getCars(); callback(true);},
        ()=>{callback(false)});

    }

    searchAvailableCars(startRent:Date, endRent:Date, callback:(car:Car[])=>void): void {
        this.myHttpClient.get(`${this.link}?startRent=${startRent}&endRent=${endRent}`)
                .subscribe((x:Car[]) => { callback(x);});

    }

}