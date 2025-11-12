import { NgFor, NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Sector } from '../sector';
import { ParkingSpot } from '../parking-spot';
import { ParkingService } from '../parking.service';
import { Reservation } from '../reservation';
import { FormsModule } from '@angular/forms';
import { concatMap } from 'rxjs';
import { ParkingSpotComponent } from '../parking-spot/parking-spot.component';
import { Cancellation } from '../cancellation';
import { PopupCardComponent } from '../popup-card/popup-card.component';

@Component({
  selector: 'app-main-container',
  standalone: true,
  imports: [NgFor, NgIf, FormsModule, ParkingSpotComponent, PopupCardComponent],
  templateUrl: './main-container.component.html',
  styleUrl: './main-container.component.css',
})

export class MainContainerComponent implements OnInit {
  sectors: Sector[] = [];
  parkingSpots: ParkingSpot[] = []
  selectedSector?: Sector;
  selectedSpot?: ParkingSpot;
  formInput: {start_Date: string, end_Date: string, start_Time: string, end_Time: string, license_Plate: string, ClientEmail: string} = {
    start_Time: '',
    end_Time: '',
    start_Date: '',
    end_Date: '',
    license_Plate: '',
    ClientEmail: ''
  }
  cancelInput: {license_Plate: string, parking_lot_ID: string} = {
    license_Plate: '',
    parking_lot_ID: '',
  }
  showPopup: boolean = false;
  response: string = '';

  constructor(private parkingService: ParkingService) {}

  selectSector(sector: Sector) {
    this.selectedSector = sector;
    this.selectedSpot = undefined;
  }

  selectParkingSpot(spot: ParkingSpot) {
    this.selectedSpot = spot;
  }

  submitReservation(){
    if(!this.selectedSector || !this.selectedSpot){
      return;
    }
    const start_Time = `${this.formInput.start_Date}T${this.formInput.start_Time}:00.000Z`;
    const end_Time = `${this.formInput.end_Date}T${this.formInput.end_Time}:00.000Z`;
    if(start_Time === end_Time){
      return;
    }
    if(new Date(this.formInput.end_Date).getTime() < new Date(this.formInput.start_Date).getTime()){
      return;
    }

    const payload: Reservation = {
      start_Time: start_Time,
      end_Time: end_Time,
      license_Plate: this.formInput.license_Plate.toUpperCase(),
      parking_Lot_ID: this.selectedSector.sector_ID,
      place_Number: this.selectedSpot.place_Number,
      ClientEmail: this.formInput.ClientEmail,
    };
    console.log(payload);
    this.parkingService.reserveSpot(payload).subscribe((response) => {
      this.response = response.message;
      this.showPopup = true;
      console.log('Reservation status: ', response);
    });
  }

  sumbitCancellation(){
    const payload: Cancellation = {
      license_Plate: this.cancelInput.license_Plate.toUpperCase(),
      parking_Lot_ID: this.cancelInput.parking_lot_ID
    }

    console.log(payload);
    this.parkingService.cancelReservation(payload).subscribe((response) => {
      console.log('Cancellation status: ', response);
      this.response = response;
      this.showPopup = true;
    });
  }

  ngOnInit() {
    const time = new Date().toTimeString().slice(0,5).toString();
    const date = new Date().toISOString().split('T')[0];
    this.formInput.start_Date = date;
    this.formInput.end_Date = date;
    this.formInput.start_Time = time;
    this.formInput.end_Time = time;

    this.parkingService.getSectors().pipe(
      concatMap((sectors) => {
        this.sectors = sectors;
        return this.parkingService.getPlaces();
      })
    ).subscribe((spots) => {
      this.parkingSpots = spots;
      console.log(this.parkingSpots);
    })
  }
}
