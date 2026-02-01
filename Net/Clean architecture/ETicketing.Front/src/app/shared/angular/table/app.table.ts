import { CommonModule } from '@angular/common';
import { Component, ContentChild, EventEmitter, Input, OnChanges, Output, TemplateRef } from '@angular/core';
import { VButtonComponent } from '../button/app.button';
import { FormsModule } from '@angular/forms';
import { VInputTextComponent } from '../input-text/app.input-text';


@Component({
  selector: 'app-table',
  imports: [CommonModule, VButtonComponent, VInputTextComponent, FormsModule],
  templateUrl: './app.table.html',
  styleUrls: ['./app.table.scss']
})
export class VTableComponent implements OnChanges {
  @Input() source: any[]=[];
  @Input() columns: { id: string, name: string }[] = [];
  @Input() selectedByColumnId: any[]=[];
  @Input() columnId: string = "";
  @Input() showSelectColumn: boolean = false;
  @Output() selectedChanged: EventEmitter<any[]> = new EventEmitter<any[]>();
  @ContentChild('headerTemplate', { static: true }) headerTemplate!: TemplateRef<any>;
  @ContentChild('bodyTemplate', { static: true }) bodyTemplate!: TemplateRef<any>;

  viewSource: any[] = [];
  viewColumns: { id: string, name: string }[] = [];

  currentPage: number = 0;
  pageSize: number = 30;
  allSelected: boolean = false;
  filters: any = {};

  ngOnChanges() {
    this.currentPage = this.currentPage == 0 ? 1 : this.currentPage;
    this.viewSource = this.source.map(x => ({ ...x, selected$$$: this.selectedByColumnId.some(y => y == x[this.columnId]) }));
    this.viewColumns = this.columns.map(x => x);
    if (Object.keys(this.filters).length==0) {
      this.columns.forEach(x => this.filters[x.id] = "");
    }
  }

  
  get paginatedSource() {
    return this.filteredSource.slice((this.currentPage - 1) * this.pageSize, ((this.currentPage - 1) * this.pageSize) + this.pageSize);
  }
  get filteredSource() {
    let aux = this.viewSource;
    for (var prop in this.filters) {
      if (this.filters[prop] != "") {
        aux = aux.filter(x => x[prop].toString().toLowerCase().includes(this.filters[prop].toLowerCase()));
      }
    }
    return aux;
  }

  selectPage(page: string) {
    this.currentPage = parseInt(page, 10) || 1;
  }
  formatInput(input: HTMLInputElement) {
    input.value = input.value.replace(/[^0-9]/g, '');
  }
  onSelect(row: any) {
    row.selected$$$ = !row.selected$$$;
    this.updateSelected();
  }
  updateSelected() {
    this.selectedByColumnId = this.viewSource.filter(x => x.selected$$$).map(x => x[this.columnId]);
    this.selectedChanged.emit(this.selectedByColumnId);
    this.allSelected = this.paginatedSource.every(x => x.selected$$$);
  }
  onSelectAll() {
    this.filteredSource.forEach(x => x.selected$$$ = this.allSelected);
    this.updateSelected();
  }
  btnFirst() {
    this.currentPage = 1;
  }
  btnLast() {
    this.currentPage = Math.ceil( this.source.length / this.pageSize);
  }
  btnNext() {
    this.currentPage++;
  }
  btnPrevious() {
    this.currentPage--;
  }
}
