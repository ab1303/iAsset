import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from "./app.component";
import {TOASTR_TOKEN, Toastr} from "./common/toastr.service";
import { iAssetService } from './common/iasset.service';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms'

declare let toastr: Toastr;

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule
    ],
    declarations : [AppComponent], 
    providers:[
        iAssetService,
        { provide: TOASTR_TOKEN, useValue: toastr }
    ], 
    bootstrap : [AppComponent],  
})
export class AppModule {

}