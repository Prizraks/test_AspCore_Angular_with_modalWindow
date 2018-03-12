import { Component } from '@angular/core';
import { DetailServcies } from '../../Services/services';
import { Response } from '@angular/http';
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';

@Component({
    selector: 'keepers',
    templateUrl: './keepers.component.html'
})
export class keepersComponent {
    public KeepersList = [];
    public formData: FormGroup;
    public constructor(private detService: DetailServcies) {
        this.detService.getKeeperCountList()
            .subscribe(
            (data: Response) => (this.KeepersList = data.json())
        );
        this.formData = new FormGroup({
            'Name': new FormControl('', [Validators.required])
        });

    }
    deleteKeeper(detId: number) {
        var status = confirm("Вы точно хотите удалить запись из БД?");
        if (status == true) {
            this.detService.removeKeeper(detId)
                .subscribe((data: Response) => (alert(data.json())));
            //Get new list of keepers  
            this.detService.getKeeperCountList()
                .subscribe(
                (data: Response) => (this.KeepersList = data.json())
                );
        }

    }
    submitData() {
        if (this.formData.valid) {
            var Obj = {
                KeeperName: this.formData.value.Name,
            };
            this.detService.postDataKeeper(Obj).subscribe();
            alert("Кладовщик успешно добавлен!");
            this.formData.reset();
            this.detService.getKeeperCountList()
                .subscribe(
                (data: Response) => (this.KeepersList = data.json())
                );
        }

    }
}