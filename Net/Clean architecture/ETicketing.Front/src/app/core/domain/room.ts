export interface IRoom {
  id: string;
  name: string;
  rows: number;
  columns: number;
}

export class Room implements IRoom {
  constructor(public id: string, public name: string, public rows: number, public columns:number) { }
  

}
