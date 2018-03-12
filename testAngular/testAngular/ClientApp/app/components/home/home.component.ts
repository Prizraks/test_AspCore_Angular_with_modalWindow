import { Component,ViewChild,ElementRef } from '@angular/core';
import { DetailServcies } from '../../Services/services';
import { Response } from '@angular/http';
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {
    public DetailList = [];
    public KeeperList = [];
    public formData: FormGroup;
    public mess = "";
    public constructor(private detService: DetailServcies) {
        this.detService.getDetailList()
            .subscribe(
            (data: Response) => (this.DetailList = data.json())
            );
        this.detService.getKeeperList()
            .subscribe(
            (data2: Response) => (this.KeeperList = data2.json())
        );
        this.formData = new FormGroup({
            'DetailName': new FormControl('', [Validators.required]),
            'Count': new FormControl('', Validators.nullValidator),
            'NomCode': new FormControl('', Validators.required),
            'DateCreate': new FormControl('', Validators.required),
            'Storekeeper': new FormControl('', [Validators.required])
        });
    }
    deleteRowOnDB(detId: number) {
        var status = confirm("Вы точно хотите удалить запись из БД?");
        if (status == true) {
            this.detService.removeDetailOnDB(detId)
                .subscribe((data: Response) => (this.mess=data.json()));
            alert(this.mess);

            //Get new list of details  
            this.detService.getDetailList()
                .subscribe(
                (data: Response) => (this.DetailList = data.json())
                );
        }

    }  
    deleteDetail(detId: number) {
        var status = confirm("Вы точно переместить деталь в удаленные?");
        if (status == true) {
            this.detService.removeDetail(detId)
                .subscribe((data: Response) => (this.mess = data.json()));
            alert(this.mess);

            //Get new list of details  
            this.detService.getDetailList()
                .subscribe(
                (data: Response) => (this.DetailList = data.json())
                );
        }
    }

        submitData() {
            if (this.formData.valid) {
                var Obj = {
                    NomCode: this.formData.value.NomCode,
                    Name: this.formData.value.DetailName,
                    StorekeeperId: this.formData.value.Storekeeper,
                    Count: this.formData.value.Count,
                    DateCreate: this.formData.value.DateCreate,
                };
                this.detService.postData(Obj).subscribe();
                alert("Деталь успешно добавлена")
                this.formData.reset();
                this.detService.getDetailList()
                    .subscribe(
                    (data: Response) => (this.DetailList = data.json())
                    );
            }
        }
}