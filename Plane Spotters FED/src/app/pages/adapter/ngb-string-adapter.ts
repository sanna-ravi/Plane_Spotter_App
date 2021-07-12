import { NgbDateStruct, NgbDateAdapter } from "@ng-bootstrap/ng-bootstrap";

export class NgbStringAdapter extends NgbDateAdapter<Date> {

  fromModel(date: Date): NgbDateStruct {
    if (date && typeof date == "string") {
      date = new Date(date);
    }
    return date ? {
      year: date.getFullYear(),
      month: date.getMonth() + 1,
      day: date.getDate()
    } : null;
  }

  toModel(date: NgbDateStruct): Date {
    return date ? new Date(Date.UTC(date.year, date.month - 1, date.day)) : null;;
  }
}