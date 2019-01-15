import { Component, OnInit } from '@angular/core';
import { UserList } from '../shared/models/user-list.model';
import { UserService } from '../shared/services/user.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  allUsers: UserList;
  actionMsg: string;
  userRole:string="manager";

  constructor(private myUserService: UserService) { }


  ngOnInit() {
    this.allUsers = this.myUserService.allUsers; 
  }

  deleteUser(userName:string) {
    this.myUserService.deleteUser(userName).subscribe(
      (res) => {
        if (res) {
          this.myUserService.getUsers();
        }
        this.actionMsg = (res) ? "delete success" : "delete fail";
      });
     
  }

}
