import { User } from "./user.model";
import { Car } from "./car.model";

export interface Order{

    StartRent:Date,
    EndRent:Date,
    ReturnDate:Date,
    User:User,
    Car:Car
}