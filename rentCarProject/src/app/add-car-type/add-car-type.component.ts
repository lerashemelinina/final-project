import { Component} from '@angular/core';
import { CarType } from '../shared/models/carType.model';
import { CarTypeService } from '../shared/services/carType.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-car-type',
  templateUrl: './add-car-type.component.html'
})
export class AddCarTypeComponent {
actionMsg :string;
years:Array<number>=[];
isYearError:boolean=false;


newCarType:CarType={
  Make:undefined,
  Model:undefined,
  Price:undefined,
  DelayCharge:undefined,
  Year:undefined,
  IsAutomatic:undefined
}
  
constructor(private carTypeService: CarTypeService, private router:Router) {
  this.years=[(new Date).getFullYear(),(new Date).getFullYear()-1,(new Date).getFullYear()-2,
                 (new Date).getFullYear()-3,(new Date).getFullYear()-4];
            
  this.newCarType.IsAutomatic=true;

 }
 
 
validateYear(value){
  this.isYearError=false;

  if(value=='default')
   this.isYearError=true;
 }

saveChanges() {

  if(this.isYearError)
  return;

  let callback=(bool:boolean)=>{this.actionMsg = (bool) ? "action success" : "action fail";}
                                
                               
  this.carTypeService.addCarType(this.newCarType,callback) ;
  }

onBack(){
  this.router.navigate(['/manager/orderList']);
  }
  
}
