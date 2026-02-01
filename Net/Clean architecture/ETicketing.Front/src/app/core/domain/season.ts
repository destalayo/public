export interface ISeason {
  id: string;
  name: string;
  roomId: string;
}

export class Season implements ISeason {
  constructor(public id: string, public name: string, public roomId: string) { }
  

}
