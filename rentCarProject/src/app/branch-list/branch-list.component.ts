import { Component, OnInit } from '@angular/core';
import { BranchList } from '../shared/models/branch-list.model';
import { BranchService } from '../shared/services/branch.service';

@Component({
  selector: 'app-branch-list',
  templateUrl: './branch-list.component.html'
})


export class BranchListComponent implements OnInit {

//   branchList: BranchList;
//   actionMsg: string;
//   constructor(private myBranchService: BranchService) { }

  ngOnInit() {
//     this.branchList = this.myBranchService.branchList;
  }


//   deleteBranch(branchName: string) {
//     this.myBranchService.deleteBranch(branchName).subscribe(
//       (res) => {
//         if (res) {
//           this.myBranchService.getAllBranches();
//         }
//         this.actionMsg = (res) ? "delete success" : "delete fail";

//       });

     
//   }

}
