import { Component, Input, OnInit } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";

@Component({
  selector: "app-alertModal",
  templateUrl: "./alertModal.component.html",
})
export class AlertModalComponent implements OnInit {
  constructor(public activeModal: NgbActiveModal) {}
  @Input() title;
  @Input() message;
  ngOnInit(): void {}
}
