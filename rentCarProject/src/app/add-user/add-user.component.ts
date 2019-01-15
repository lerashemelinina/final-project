import { Component, OnInit } from '@angular/core';
import { User } from '../shared/models/user.model';
import { UserService } from '../shared/services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UploadImageService } from '../shared/services/uploadImage.service';


@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html'
})
export class AddUserComponent implements OnInit  {

  actionMsg :string;
  userRole:string=null;
  userRoleOptions:Array<string>=['employee','customer','manager'];
  isUserRoleError:boolean=false;
  fileToUpload: File = null;
  imageUrl:string="";
  notGuest: boolean=true;

  newUser:User={
    FullName:undefined,
    IdentityNumber:undefined,
    UserName:undefined,
    BirthDate:undefined,
    IsMale:undefined,
    Email:undefined,
    Password:undefined,
    UserRole:undefined,
    Image:undefined
  }
    
  constructor(private myUserService: UserService, private myActivatedRoute: ActivatedRoute,
              private router:Router, private UploadImageService: UploadImageService,) { 
                this.newUser.IsMale=true;

                if(this.userRole=="guest")
                {
                this.newUser.UserRole="customer";
                this.notGuest=false;
                }
              }

  ngOnInit() {
    this.myActivatedRoute.params.subscribe(params => {
    this.userRole=params.userRole;
    });
  }

  handleFileInput(file: FileList) {
    this.fileToUpload = file.item(0);
    let reader = new FileReader();
    reader.onload = (event: any) => { this.imageUrl = event.target.result; }
    reader.readAsDataURL(this.fileToUpload);
    }


  validateUserRole(value){
    if (value=='default')
        this.isUserRoleError=true;
    else
        this.isUserRoleError=false;
  }
               
    
      saveChanges() {
        let callback=(bool:boolean)=>{this.actionMsg = (bool) ? "action success" : "action fail";}

        if(this.fileToUpload!=null)
        {
        this.UploadImageService.postFile("UserImages",this.fileToUpload)
        .subscribe(data => { this.newUser.Image=data.toString();
                             this.myUserService.addUser(this.newUser,callback) ;});
        }
        else
        this.myUserService.addUser(this.newUser,callback) ;

      }

      onBack(){
        if(this.userRole=="manager")
        this.router.navigate(['/manager/orderList']);

        if(this.userRole=="customer")
        this.router.navigate(['/home']);
      }

}
