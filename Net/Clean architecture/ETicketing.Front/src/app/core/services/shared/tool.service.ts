import { Injectable } from "@angular/core";

@Injectable({ providedIn: 'root' })
export class ToolService {
  constructor() { }

  stringFormat(template: string, ...params: any[]): string {
    let result = template;
    for (let i: number = 0; i < params.length; i++) {
      result = result.replace("{" + i + "}", params[i].toString());
    }
    return result;
  }
  getFileName(path:string): string {
    const filename = path.split('/').pop();
    const result = filename.substring(0, filename.lastIndexOf('.')); 
    return result;
  }
  replaceOnArray(array:any[],id:any, newItem:any) {
    const index = array.findIndex(x => x.id === id);
    if (index !== -1) {
      array[index] = { ...array[index], ...newItem };
    }
  }
  removeOnArray(array: any[], id: any) {
    const index = array.findIndex(x => x.id === id);
    if (index !== -1) {
      array.splice(index, 1);
    }
  }
  async runAsync<T>(task: () => T | Promise<T>): Promise<T> {
    return await Promise.resolve().then(task);
  }
}
