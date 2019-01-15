import { Component, OnInit } from '@angular/core';
import { OrderList } from '../shared/models/order-list.model';
import { OrderService } from '../shared/services/order.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html'
})
export class OrderListComponent implements OnInit {

  allOrders: OrderList;
  actionMsg: string;
  userRole:string="manager";

  constructor(private orderService: OrderService) { }


  ngOnInit() {
    this.allOrders = this.orderService.allOrders; 
  }

  deleteOrder(startRent:Date,carNumber:number) {
    this.orderService.deleteOrder(startRent, carNumber).subscribe(
      (res) => {
        if (res) {
          this.orderService.getOrders();
        }
        this.actionMsg = (res) ? "delete success" : "delete fail";
      });
     
  }

}
