import { Component, OnInit } from '@angular/core';
import { Car } from '../shared/models/car.model';
import { CarService } from '../shared/services/car.service';
import { ActivatedRoute, Router } from '@angular/router';
import { BranchService } from '../shared/services/branch.service';
import { BranchList } from '../shared/models/branch-list.model';


@Component({
  selector: 'app-edit-car',
  templateUrl: './edit-car.component.html'
})
export class EditCarComponent implements OnInit {

actionMsg :string;
localCarNumber:number;
allBranches: BranchList;
selectedBranch:string;


localCar:Car={
  "CarType": undefined,
  "Mileage": undefined,
  "Image": undefined,
  "Branch": undefined,
  "IsForRent": undefined,
  "CarNumber": undefined
};

  constructor(private myCarService: CarService, private myBranchService: BranchService, 
              private myActivatedRoute: ActivatedRoute, private router:Router) { }

  ngOnInit() {
    this.allBranches=this.myBranchService.allBranches;
    this.myActivatedRoute.params.subscribe(params => {
      this.localCarNumber=params.carNumber;
 
      if(params.carNumber){
        this.myCarService.getCarForEdit(params.carNumber,(car:Car)=>{this.localCar=car;});
      }
      
    });

  }

  saveChanges(){
    this.localCar.Branch=this.allBranches.branchList.find(x=>(x.BranchName==this.selectedBranch));
    let callback=(bool:boolean)=>{this.actionMsg = (bool) ? "action success" : "action fail";} 
    this.myCarService.editCar(this.localCar, this.localCarNumber, callback)
  }

  onBack(){
    this.router.navigate(['/manager/orderList']);
  }

}
