import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { BranchService } from './shared/services/branch.service';
import { BranchListComponent } from './branch-list/branch-list.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { CarListComponent } from './car-list/car-list.component';
import { UserListComponent } from './user-list/user-list.component';
import { CarTypeListComponent } from './car-type-list/car-type-list.component';
import { OrderListComponent } from './order-list/order-list.component';
import { CarTypeService } from './shared/services/carType.service';
import { HttpClientModule } from '@angular/common/http';
import { EditCarTypeComponent } from './edit-car-type/edit-car-type.component';
import { RouterModule, Routes } from '@angular/router';
import { AddCarTypeComponent } from './add-car-type/add-car-type.component';
import { FormsModule } from '@angular/forms';
import { CarService } from './shared/services/car.service';
import { AddNewCarComponent } from './add-new-car/add-new-car.component';
import { UploadImageService } from './shared/services/uploadImage.service';
import { EditCarComponent } from './edit-car/edit-car.component';
import { UserService } from './shared/services/user.service';
import { AddUserComponent } from './add-user/add-user.component';
import { EditUserComponent } from './edit-user/edit-user.component';
import { OrderService } from './shared/services/order.service';
import { EditOrderComponent } from './edit-order/edit-order.component';
import { AddOrderComponent } from './add-order/add-order.component';
import { ManagerComponent } from './manager/manager.component';
import { EmployeeComponent } from './employee/employee.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { SearchCarComponent } from './search-car/search-car.component';
import { OrderDetailsService } from './shared/services/orderDetails.service';



const appRoutes: Routes = [
  
  {path: 'addCarType', component: AddCarTypeComponent},
  {path: 'editCarType/:make/:model/:year', component: EditCarTypeComponent },
  {path:  'carList', component: CarListComponent},
  {path: 'addCar', component: AddNewCarComponent},
  {path: 'editCar/:carNumber', component: EditCarComponent},
  {path: 'addUser/:userRole', component: AddUserComponent},
  {path: 'editUser/:userName', component: EditUserComponent},
  {path:'addOrder', component:AddOrderComponent}, 
  {path:'employee', component:EmployeeComponent},
  {path:'signIn', component:SignInComponent},
  {path: 'home', component: SearchCarComponent},  
  {path: 'editOrder/:carNumber/:startRent/:userRole', component: EditOrderComponent},
  {path:'manager', component: ManagerComponent,
         children: [
                     {path: '',redirectTo: 'orderList',pathMatch: 'full'},
                     {path:'orderList', component:OrderListComponent},
                     {path:'carList', component:CarListComponent},
                     {path:'carTypeList', component:CarTypeListComponent},
                     {path:'userList', component:UserListComponent}              
   ]},
  {path:'employee', component:EmployeeComponent},
  {path: '',redirectTo: 'home',pathMatch: 'full'},
  {path: '**', component: SearchCarComponent } 
];



@NgModule({
  declarations: [
    AppComponent,
    BranchListComponent,
    HeaderComponent,
    FooterComponent,
    CarListComponent,
    UserListComponent,
    CarTypeListComponent,
    OrderListComponent,
    EditCarTypeComponent,
    AddCarTypeComponent,
    AddNewCarComponent,
    EditCarComponent,
    AddUserComponent,
    EditUserComponent,
    EditOrderComponent,
    AddOrderComponent,
    ManagerComponent,
    EmployeeComponent,
    SignInComponent,
    SearchCarComponent   
  ],

  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes)
  ],

  providers: [
    BranchService,
    CarTypeService,
    CarService,
    UploadImageService,
    UserService,
    OrderService,
    OrderDetailsService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
