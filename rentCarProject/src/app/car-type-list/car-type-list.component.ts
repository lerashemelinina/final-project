import { Component, OnInit } from '@angular/core';
import { CarTypeService } from '../shared/services/carType.service';
import { CarTypeList } from '../shared/models/carType-list.model';
import { CarType } from '../shared/models/carType.model';

@Component({
  selector: 'app-car-type-list',
  templateUrl: './car-type-list.component.html'
})

export class CarTypeListComponent implements OnInit {

  allCarTypes: CarTypeList;
  actionMsg: string;

  constructor(private myCarTypeService: CarTypeService) { }


  ngOnInit() {
    this.allCarTypes = this.myCarTypeService.allCarTypes; 
  }

  deleteCarType(make: string, model:string, year:number) {
    this.myCarTypeService.deleteCarType(make, model, year ).subscribe(
      (res) => {
        if (res) {
          this.myCarTypeService.getCarTypes();
        }
        this.actionMsg = (res) ? "delete success" : "delete fail";
      });
     
  }

}
