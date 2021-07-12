import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { SpotterService } from 'src/app/services/spotter.service';
import { environment } from 'src/environments/environment';
import { PlaneSpotterModel } from '../models/spotter.model';

@Component({
  selector: 'app-spotters-manage',
  templateUrl: './spotters-manage.component.html',
  styleUrls: ['./spotters-manage.component.scss']
})
export class SpottersManageComponent implements OnInit {

  public spotterForm: FormGroup;
  public data = [];
  checkForm = false;

  isSubmitted = false;
  public internalId: string;

  public model: PlaneSpotterModel = {};

  public image: any;
  public imageUrl = '';

  public dateandTime?: NgbDateStruct;
  protected readonly configuration: any = environment;

  yesterday = new Date(new Date().setDate(new Date().getDate() - 1))
  maxDate = {
    year: this.yesterday.getFullYear(),
    month: this.yesterday.getMonth() + 1,
    day: this.yesterday.getDate()
  };

  constructor(protected service: SpotterService, private router: Router, public formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute) {

    this.activatedRoute.params.subscribe(params => {
      if (params['id']) {
        this.internalId = params['id'];
        this.loadData();
      }
    });

    this.spotterForm = this.formBuilder.group({
      'Make': ['', Validators.compose([Validators.required, Validators.maxLength(128)])],
      'Model': ['', Validators.compose([Validators.required, Validators.maxLength(128)])],
      'Registration': ['', Validators.compose([Validators.required, Validators.pattern("[a-zA-Z0-9]{1,2}-[a-zA-Z0-9]{1,5}")])],
      'Location': ['', Validators.compose([Validators.required, Validators.maxLength(255)])],
      'DateandTime': ['', Validators.required],
    });
  }

  ngOnInit(): void {
  }

  protected loadData(): void {
    this.service.Get(this.internalId)
      .subscribe(
        (data: any[]) => this.model = this.manipulateData(data),
        (error: any) => console.log("something went wrong"));
  }

  protected manipulateData(data) {
    if (data) {
      this.imageUrl = `${this.configuration.image.imageBasePath}${data.spotterImageUrl}`;
      if (data.dateandTime && typeof data.dateandTime == "string") {
        var modelDate = new Date(data.dateandTime);
        this.dateandTime =  {
          year: modelDate.getFullYear(),
          month: modelDate.getMonth() + 1,
          day: modelDate.getDate()
        };
        console.log(this.dateandTime);
      }
    }
    return data;
  }

  fileChange(StockImage) {
    if (StockImage.files.length >= 1) {
      this.manipulateFile(StockImage.files[0]);
    }
  }

  manipulateFile(file) {
    const reader = new FileReader();
    if (file) {
      var mimeType = file.type;
      if (mimeType.match(/image\/*/) == null) {
        alert('Only images can be uploaded !');
        return;
      }
      this.model.spotterImage = file;
      reader.onload = () => {
        this.image = reader.result;
      }
      reader.readAsDataURL(file);
    }
  }

  public onSubmitForm() {
    this.checkForm = true;
    console.log(this.spotterForm);
    if (!this.spotterForm.invalid) {
      if (this.model.spotterImageUrl == null && this.image == null) {
        alert('There should be an item image');
      }
      else {
        this.isSubmitted = true;

        console.log(this.dateandTime);
        this.model.dateandTime = new Date(Date.UTC(this.dateandTime.year, this.dateandTime.month - 1, this.dateandTime.day)).toJSON();
        console.log(this.model);
        const formData = this.getFormData(this.model);
        console.log(formData);
        if (this.model.id > 0) {
          formData.delete('createdBy');
          formData.delete('updatedBy');
        }

        this.service.AddorUpdateFormData(formData)
          .subscribe(
            (data: PlaneSpotterModel) => {
              this.loadData();
              this.resetForm();
              alert('Spotter information saved successfully!');
              this.router.navigate(['spotter']);
            },
            (error: any) => {
              alert('Failed to update spotter info!');
              console.log(error);
              this.isSubmitted = false;
            }
          );
      }
    }
  }

  public getFormData(object) {
    const formData = new FormData();
    Object.keys(object).forEach(key => formData.append(key, object[key]));
    return formData;
  }

  public resetForm() {
    this.model = {};
    this.isSubmitted = false;
    this.checkForm = false;
    this.router.navigate(['spotter']);
  }

  filesDropped(files: any[]): void {
    if (files.length != 1) {
      alert('Only a image can be uploaded !');
    }
    else {
      this.manipulateFile(files[0]);
    }
  }

  removeImage(StockImage): void {
    this.image = null;
    StockImage.value = '';
    this.model.spotterImage = null;
  }

}
