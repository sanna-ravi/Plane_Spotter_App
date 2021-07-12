import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { SpotterService } from 'src/app/services/spotter.service';

@Component({
  selector: 'app-spotters-list',
  templateUrl: './spotters-list.component.html',
  styleUrls: ['./spotters-list.component.scss']
})
export class SpottersListComponent implements OnInit {

  public selectedId: string;
  public data = [];

  public settings = {
    selectMode: 'single',
    hideHeader: false,
    hideSubHeader: false,
    mode: 'external',
    actions: {
      columnTitle: 'Actions',
      add: true,
      edit: true,
      delete: true,
      custom: [],
      position: 'right', // left|right
      width:'100px'
    },
    add: {
      addButtonContent: '<h4 class="mb-1"><i class="fa fa-plus ml-3 text-success"></i></h4>',
      createButtonContent: '<i class="fa fa-check mr-3 text-success"></i>',
      cancelButtonContent: '<i class="fa fa-times text-danger"></i>'
    },
    edit: {
      editButtonContent: '<i class="ni ni-curved-next mr-3 text-primary"></i>',
      saveButtonContent: '<i class="fa fa-check mr-3 text-success"></i>',
      cancelButtonContent: '<i class="fa fa-times text-danger"></i>'
    },
    delete: {
      deleteButtonContent: '<i class="ni ni-fat-delete text-danger"></i>',
      confirmDelete: true
    },
    noDataMessage: 'No data found',
    columns: {
      internalId: {
        title: 'Id',
        editable: true,
        type: 'html',
        filter: true,
        show: false,
        sort: true,
        sortDirection: 'asc',
        
        valuePrepareFunction: (cell, row) => {
          return `<a href="/#/spotter/${row.internalId}">${cell}</a>`;
        }
      },
      make: {
        title: 'Make',
        type: 'string',
        filter: true,
        editable: false
      },
      model: {
        title: 'Model',
        type: 'string',
        filter: true,
        editable: false
      },
      registration: {
        title: 'Registration',
        type: 'string',
        filter: true,
        editable: false
      },
      location: {
        title: 'Location',
        type: 'string',
        filter: true,
        editable: false
      },
      dateandTime: {
        title: 'Date Time',
        type: 'string',
        filter: true,
        editable: false
      }
    },
    pager: {
      display: true,
      perPage: 10,
      perPageSelect: [10, 20, 50, 75, 100, 200, 500]
    }
  };

  constructor(protected service: SpotterService, private router: Router) { }

  ngOnInit(): void {
    this.loadData();
  }

  protected loadData(): void {
    this.service.GetAll()
      .subscribe(
        (data: any[]) => this.data = this.manipulateData(data),
        (error: any) => alert( 'Failed to load data'));
  }
  protected manipulateData(data)
  {
    return data;
  }

  public onAdd(event) {
    this.router.navigate(['spotter', 'new']);
  }

  public onUserRowSelect(event) {
    //this.selectedIds = event.selected.map(x => x.id);
  }

  public onEdit(event) {
    this.router.navigate(['spotter', event.data.internalId]);
  }

  public onDelete(event) {
    console.log(event);
    this.selectedId = event.data.internalId;
    
    this.service.Delete(this.selectedId).subscribe(
      (data: any[]) => 
      { 
        alert( 'Spotter data deleted successfully!');
        this.loadData(); 
        this.selectedId = null; 
      },
      (error: any) => {alert( 'Failed to delete stock unit info!')});
  }

}
