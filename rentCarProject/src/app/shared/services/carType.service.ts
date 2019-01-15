import { Injectable } from "@angular/core";
import { CarTypeList } from "../models/carType-list.model";
import {HttpClient} from "@angular/common/http";
import { CarType } from "../models/carType.model";
import { Observable } from "rxjs";

@Injectable()
export class CarTypeService {

    private link="http://localhost:63174/api/carType";
   
    allCarTypes:CarTypeList=new CarTypeList();

    constructor(private myHttpClient: HttpClient) { 
        this.getCarTypes();
    }

    getCarTypes(): void {
        this.myHttpClient.get(this.link)
            .subscribe((x: Array<CarType>) => { this.allCarTypes.carTypeList = x;});
                                       
    }

    getCarTypes2():Observable<Array<CarType>>{
        return this.myHttpClient.get<Array<CarType>>(this.link);
    }


    getCarTypeForEdit(make:string, model:string, year:number, callback:(carType:CarType)=>void): void {
        this.myHttpClient.get(`${this.link}?make=${make}&model=${model}&year=${year}`)
            .subscribe((x:CarType) => { callback(x);});
    }

    deleteCarType(make:string, model:string, year:number):Observable<boolean>{
        let apiUrl:string=`${this.link}?make=${make}&model=${model}&year=${year}`;
        return this.myHttpClient.delete<boolean>(apiUrl);
    }


    addCarType(carType:CarType,callback:(bool:boolean)=>void): void {
        this.myHttpClient.post<boolean>(this.link,JSON.stringify(carType), { headers: {"content-type": "application/json" }}).subscribe(()=>{this.getCarTypes(); callback(true);},
        ()=>{callback(false)});
    }

    editCarType(carType:CarType,make:string, model:string, year:number, callback:(bool:boolean)=>void): void {
        this.myHttpClient.put<boolean>(`${this.link}?make=${make}&model=${model}&year=${year}`,JSON.stringify(carType), { headers: {"content-type": "application/json" }}).subscribe(()=>{this.getCarTypes(); callback(true);},
        ()=>{callback(false)});
    }


}