
import { Component, Inject } from "@angular/core";
import { Toastr, TOASTR_TOKEN } from "./common/toastr.service";
import { iAssetService } from "./common/iasset.service";

@Component({
    selector:'iasset-app',
    templateUrl:'./app.component.html'
})
export class AppComponent {
    countryName:string = '';
    cities:string[];

    constructor(@Inject(TOASTR_TOKEN) private toastr: Toastr,
    private iAssetService: iAssetService){

    }
    getCities(){
        if(!this.countryName)
        {
            this.toastr.info('Please input country name');
            return;
        }

        this.iAssetService.getCities(this.countryName).subscribe(() => {
            this.toastr.success(`Cities successfully retrieved for country ${this.countryName}`);
        });
        

    }

}