import { CarType } from "./carType.model";
import { Branch } from "./branch.model";

export interface Car{

    CarType:CarType,
    Mileage:number,
    Image:string,
    Branch:Branch,
    IsForRent:boolean,
    CarNumber:number   
}