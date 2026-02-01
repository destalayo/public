export interface ISeat {
  id: string;
  column: number;
  row: number;
  roomId: string;
}

export class Seat implements ISeat {
  constructor(public id: string, public column: number, public row: number, public roomId:string) { }
  

}
