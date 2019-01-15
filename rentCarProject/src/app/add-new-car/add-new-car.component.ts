import { Component, OnInit } from '@angular/core';
import { Car } from '../shared/models/car.model';
import { CarService } from '../shared/services/car.service';
import { BranchService } from '../shared/services/branch.service';
import { CarTypeService } from '../shared/services/carType.service';
import { CarTypeList } from '../shared/models/carType-list.model';
import { BranchList } from '../shared/models/branch-list.model';
import { CarType } from '../shared/models/carType.model';
import { UploadImageService } from '../shared/services/uploadImage.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-add-new-car',
  templateUrl: './add-new-car.component.html'
})
export class AddNewCarComponent implements OnInit{

  allCarTypes: CarTypeList=new CarTypeList();
  allBranches: BranchList;
  actionMsg :string;
  selectedMake:string = undefined;
  selectedModel: string = undefined;
  selectedYear: number = undefined;
  selectedBranch: string=undefined;
  fileToUpload: File = null;
  imageUrl:string="";
  makeList:Array<string>=[];
  yearList:Array<number>=[];
  modelList:Array<string>=[];
  isMakeError:boolean=false;
  isModelError:boolean=false;
  isYearError:boolean=false;
  isBranchError:boolean=false;
  
  
  newCar:Car={
  "CarType": undefined,
  "Mileage": undefined,
  "Image": undefined,
  "Branch": undefined,
  "IsForRent": undefined,
  "CarNumber": undefined
};
  
    constructor(private myCarService: CarService, private myBranchService: BranchService, 
                private myCarTypeService: CarTypeService, private UploadImageService: UploadImageService,
                private router:Router) {
                  this.newCar.IsForRent=true;
                 }
  

    ngOnInit() {
        this.myCarTypeService.getCarTypes2()
             .subscribe((x: Array<CarType>)=>{this.allCarTypes.carTypeList=x;
                                              this.makeList=Array.from(new Set(x.map((carType:CarType)=>carType.Make)))});

        this.allBranches=this.myBranchService.allBranches;

      }

    validateMake(value){
      this.isMakeError=false;
      
      if(value=='default')
      this.isMakeError=true;
    }

    validateModel(value){
      this.isModelError=false;

      if(value=='default')
      this.isModelError=true;
    }

    validateYear(value){
     this.isYearError=false;

     if(value=='default')
     this.isYearError=true;
    }

    validateBranch(value){
      this.isBranchError=false;
 
      if(value=='default')
      this.isBranchError=true;
     }


    initModelList(){
      if (this.selectedMake)
        this.modelList=Array.from(new Set(((this.allCarTypes.carTypeList.filter(x=>x.Make==this.selectedMake))
                                                                   .map((carType:CarType)=>carType.Model))));
    }

    initYearList(){
      if(this.selectedMake&&this.selectedModel)
      this.yearList=Array.from(new Set((this.allCarTypes.carTypeList.filter(x=>(x.Make==this.selectedMake)
                                                                             &&(x.Model==this.selectedModel)))
                                                                             .map((carType:CarType)=>carType.Year)));
    }

    handleFileInput(file: FileList) {
    this.fileToUpload = file.item(0);
    let reader = new FileReader();
    reader.onload = (event: any) => { this.imageUrl = event.target.result; }
    reader.readAsDataURL(this.fileToUpload);
    }
    
  

    saveChanges() {
      if(this.isMakeError)
      return;

      if(this.isModelError)
      return;
   
      if(this.isYearError)
      return;

      if(this.isBranchError)
      return;

      let callback=(bool:boolean)=>{this.actionMsg = (bool) ? "action success" : "action fail";}

      this.newCar.CarType=this.allCarTypes.carTypeList.find(x=>(x.Make==this.selectedMake)
                                                                &&(x.Model==this.selectedModel)
                                                                &&(x.Year==this.selectedYear));

      this.newCar.Branch=this.allBranches.branchList.find(x=>(x.BranchName==this.selectedBranch));


      this.UploadImageService.postFile("CarImages",this.fileToUpload)
      .subscribe(data => { this.newCar.Image=data.toString();
                           this.myCarService.addCar(this.newCar,callback) ;});

    }

    onBack(){
      this.router.navigate(['/manager/orderList']);
    }

}
