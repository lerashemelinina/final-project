import { Injectable } from "@angular/core";
import { BranchList } from "../models/branch-list.model";
import {HttpClient} from "@angular/common/http";
import { Branch } from "../models/branch.model";
import { Observable } from "rxjs";

@Injectable()
export class BranchService {
    private link="http://localhost:63174/api/branch";
   
    allBranches:BranchList=new BranchList();

    constructor(private myHttpClient: HttpClient) { 
        this.getAllBranches();
    }


    getAllBranches(): void {
        this.myHttpClient.get(this.link)
            .subscribe((x: Array<Branch>) => { this.allBranches.branchList = x; });
    }

}