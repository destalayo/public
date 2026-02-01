export interface IReservation {
  id: string;
  userId: string;
  seatId: string;
  seasonId: string;
}

export class Reservation implements IReservation {
  constructor(public id: string, public userId: string, public seatId: string, public seasonId: string) { }
  

}
