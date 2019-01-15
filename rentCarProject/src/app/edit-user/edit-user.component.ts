import { Component, OnInit } from '@angular/core';
import { User } from '../shared/models/user.model';
import { UserService } from '../shared/services/user.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html'
})
export class EditUserComponent implements OnInit {

  actionMsg :string;
  localUserName:string;

  
  localUser:User={

    FullName:undefined,
    IdentityNumber:undefined,
    UserName:undefined,
    BirthDate:undefined,
    IsMale:undefined,
    Email:undefined,
    Password:undefined,
    UserRole:undefined,
    Image:undefined
  };
    
  
  constructor(private myUserService: UserService, private myActivatedRoute: ActivatedRoute,
              private router:Router) { }
    
      ngOnInit() {
        this.myActivatedRoute.params.subscribe(params => {
          this.localUserName=params.userName;

          if(params.userName){
            this.myUserService.getUserForEdit(params.userName,(user:User)=>{this.localUser=user;})  
          }
  
        });
      }
    
      saveChanges() {
        let callback=(bool:boolean)=>{this.actionMsg = (bool) ? "action success" : "action fail";}
        this.myUserService.editUser(this.localUser, this.localUserName, callback);
      }

      onBack(){
        this.router.navigate(['/manager/orderList']);
      }

}
