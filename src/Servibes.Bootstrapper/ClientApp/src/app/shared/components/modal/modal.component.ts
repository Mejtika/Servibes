import {
  Component,
  OnInit,
  Input,
  Inject,
  ViewChild,
  ElementRef
} from "@angular/core";
import { JQUERY_TOKEN } from "../../services/jQuery.service";

@Component({
  selector: "modal",
  templateUrl: "./modal.component.html",
  styleUrls: ["./modal.component.css"]
})
export class ModalComponent implements OnInit {
  @Input() title: string;
  @Input() elementId: string;
  @Input() closeOnBodyClick: boolean;
  @ViewChild("modalcontainer") containerEl: ElementRef;

  constructor(@Inject(JQUERY_TOKEN) private $: any) {}

  ngOnInit() {}

  public closeModalOnBody() {
    if (this.closeOnBodyClick) {
      this.$(this.containerEl.nativeElement).modal("hide");
    }
  }
  public closeModalOnEscape() {
    this.$(this.containerEl.nativeElement).modal("hide");
  }
}
