import { Component, EventEmitter, Output, Input} from '@angular/core';

@Component({
  selector: 'app-popup-card',
  standalone: true,
  imports: [],
  templateUrl: './popup-card.component.html',
  styleUrl: './popup-card.component.css'
})
export class PopupCardComponent {
  @Output() closePopupEvent: EventEmitter<void> = new EventEmitter();
  @Input() description: string = "";

  closePopup(){
    this.closePopupEvent.emit();
  }
}
