import { Component, Input, Output } from '@angular/core';
import { ParkingSpot } from '../parking-spot';
import { EventEmitter } from '@angular/core';

@Component({
  selector: 'app-parking-spot',
  standalone: true,
  imports: [],
  templateUrl: './parking-spot.component.html',
  styleUrl: './parking-spot.component.css'
})
export class ParkingSpotComponent {
  @Input() spot!: ParkingSpot;
  @Input() selected: boolean = false;
  @Output() select = new EventEmitter<ParkingSpot>();

  onSelect() {
    this.select.emit(this.spot);
  }
}
