import { Injectable } from "@angular/core";
import { OrderList } from "../models/order-list.model";
import { HttpClient } from "@angular/common/http";
import { Order } from "../models/order.model";
import { Observable } from "rxjs";


@Injectable()
export class OrderService {
    private link="http://localhost:63174/api/order";
   
    allOrders:OrderList=new OrderList();

    constructor(private myHttpClient: HttpClient) { 
        this.getOrders();
    }

    getOrders(): void {
        this.myHttpClient.get(this.link)
            .subscribe((x: Array<Order>) => { this.allOrders.orderList = x; });
    }

    getOrderForEdit(startRent:Date,carNumber:number,callback:(order:Order)=>void): void {
        this.myHttpClient.get(`${this.link}?startRent=${startRent}&carNumber=${carNumber}`)
            .subscribe((x:Order) => { callback(x);});
    }

    deleteOrder(startRent:Date,carNumber:number):Observable<boolean>{
        let apiUrl:string=`${this.link}?startRent=${startRent}&carNumber=${carNumber}`;
        return this.myHttpClient.delete<boolean>(apiUrl);
    }


    addOrder(order:Order,callback:(bool:boolean)=>void): void {
        this.myHttpClient.post<boolean>(this.link,JSON.stringify(order), { headers: {"content-type": "application/json" }}).subscribe(()=>{this.getOrders(); callback(true);},
        ()=>{callback(false)});    
    }

    editOrder(order:Order,startRent:Date,carNumber:number,callback:(bool:boolean)=>void): void {

        this.myHttpClient.put<boolean>(`${this.link}?startRent=${startRent}&carNumber=${carNumber}`,JSON.stringify(order), { headers: {"content-type": "application/json" }}).subscribe(()=>{this.getOrders(); callback(true);},
        ()=>{callback(false)});
    }

}