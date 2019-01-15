import { Component, OnInit } from '@angular/core';
import { CarType } from '../shared/models/carType.model';
import { CarTypeService } from '../shared/services/carType.service';
import { ActivatedRoute, Router } from '@angular/router';


@Component({
  selector: 'app-edit-car-type',
  templateUrl: './edit-car-type.component.html'
})
export class EditCarTypeComponent implements OnInit {

actionMsg :string;
localMake:string;
localModel:string;
localYear:number;

localCarType:CarType={
  "Make":undefined,
  "Model":undefined,
  "Price":undefined,
  "DelayCharge":undefined,
  "Year":undefined,
  "IsAutomatic":undefined
};
  

constructor(private myCarTypeService: CarTypeService, private myActivatedRoute: ActivatedRoute,
            private router:Router) { }
  
    ngOnInit() {
      this.myActivatedRoute.params.subscribe(params => {
        this.localMake=params.make;
        this.localModel=params.model;
        this.localYear=params.year;
   
        if(params.make&&params.model&&params.year){
          this.myCarTypeService.getCarTypeForEdit(params.make, params.model,params.year,(carType:CarType)=>{this.localCarType=carType;})  
        }

      });
    }
  
    saveChanges() {
      let callback=(bool:boolean)=>{this.actionMsg = (bool) ? "action success" : "action fail";}
      this.myCarTypeService.editCarType(this.localCarType, this.localMake, this.localModel, this.localYear, callback);
    }


    onBack(){
      this.router.navigate(['/manager/orderList']);
    }
  
}


  

