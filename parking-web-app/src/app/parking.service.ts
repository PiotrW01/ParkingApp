import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Sector } from './sector';
import { ParkingSpot } from './parking-spot';
import { Observable } from 'rxjs';
import { Reservation } from './reservation';
import { Cancellation } from './cancellation';

@Injectable({
  providedIn: 'root'
})
export class ParkingService {
  private baseUrl: string = "http://localhost:5156"

  constructor(private http: HttpClient) {}

  getSectors(): Observable<Sector[]> {
    return this.http.get<Sector[]>(this.baseUrl + '/api/ParkingLot/AllSectors');
  }

  getPlaces(): Observable<ParkingSpot[]> {
    return this.http.get<ParkingSpot[]>(this.baseUrl + '/api/ParkingLot/AllParkingSpaces');
  }

  reserveSpot(data: Reservation): Observable<any> {
    return this.http.post(this.baseUrl + '/api/ParkingLot/AddNewReservation', data);
  }

  cancelReservation(data: Cancellation): Observable<any> {
    return this.http.delete(this.baseUrl + '/api/ParkingLot/CancelResevation', {body: data});
  }
}
