import { Component, OnInit} from '@angular/core';
import { CarList } from '../shared/models/car-list.model';
import { CarService } from '../shared/services/car.service';
import { Car } from '../shared/models/car.model';
import { Router } from '@angular/router';
import { OrderDetailsService } from '../shared/services/orderDetails.service';



@Component({
  selector: 'app-search-car',
  templateUrl: './search-car.component.html'
})
export class SearchCarComponent implements OnInit {

searchOptions:Array<string>=new Array("Make","Year","Transmission");
selectedOption:string;
selectedParam:any;
allCars: CarList=new CarList();
filteredCarList:Array<Car>;
tempCarList:Array<Car>;
startRent:Date;
endRent:Date;
makeList:Array<string>=[];
yearsList:Array<number>=[];
currentDate:Date = new Date();
isSearchOptionError:boolean=false;



constructor(private myCarService: CarService, private orderDetailsService: OrderDetailsService, private router:Router) {}

ngOnInit() {
   this.myCarService.getAllCars().subscribe(
   
     allCars=>
     {
       this.allCars.carList=allCars;
       this.makeList=Array.from(new Set(this.allCars.carList.map((car:Car)=>car.CarType.Make)));
       this.yearsList=Array.from(new Set(this.allCars.carList.map((car:Car)=>car.CarType.Year)));
     }
    
   )
}

validateSearchOption(value){
  this.isSearchOptionError=false;

  if(value=='default'){
      this.isSearchOptionError=true;
      this.selectedOption=undefined;
  }
  else
      this.selectedOption=value;

}


onSubmit(){

  this.myCarService.searchAvailableCars(this.startRent, this.endRent,(cars:Car[])=>{
                                        this.tempCarList=cars;
                                        if(this.selectedOption!="Transmission"){
                                          this.orderDetailsService.availableCars.carList=this.tempCarList.filter((car:Car)=>(car.CarType.Make==this.selectedParam)
                                                                                                       ||(car.CarType.Year==this.selectedParam));
                                        }
                                      
                                        else if(this.selectedOption=="Transmission"){
                                          if(this.selectedParam=="Automatic")
                                          this.orderDetailsService.availableCars.carList=this.tempCarList.filter((car:Car)=>(car.CarType.IsAutomatic));
                                      
                                          else if (this.selectedParam=="Non-Automatic")
                                          this.orderDetailsService.availableCars.carList=this.tempCarList.filter((car:Car)=>(!car.CarType.IsAutomatic));
                                        }
                                        
                                        this.orderDetailsService.startRent=this.startRent;
                                        this.orderDetailsService.endRent=this.endRent;

                                        this.router.navigate(['/carList']);
                                      });


}

  onBack(){
    this.filteredCarList=null;
    this.selectedOption=null;
  }

}


  
  

  
  




