import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from "./app.component";
import {TOASTR_TOKEN, Toastr} from "./common/toastr.service";

declare let toastr: Toastr;

@NgModule({
    imports: [BrowserModule],
    declarations : [AppComponent], 
    providers:[
        { provide: TOASTR_TOKEN, useValue: toastr }
    ], 
    bootstrap : [AppComponent],  
})
export class AppModule {

}