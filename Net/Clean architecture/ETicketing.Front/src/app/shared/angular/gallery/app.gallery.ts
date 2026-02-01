import { CommonModule } from '@angular/common';
import { Component, ElementRef, Input, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { VIconButtonComponent } from '../icon-button/app.icon-button';
import { VModalComponent } from '../modal/app.modal';
import { VButtonComponent } from '../button/app.button';

@Component({
  selector: 'app-gallery',
  imports: [CommonModule, VIconButtonComponent,VModalComponent, FormsModule],
  templateUrl: './app.gallery.html',
  styleUrls: ['./app.gallery.scss']
})
export class VGalleryComponent {
  @Input() pics: string[];
  @ViewChildren('imgRef') imgElements!: QueryList<ElementRef<HTMLDivElement>>;
  @ViewChild(VModalComponent) modal!: VModalComponent;
  selected = 0;
 
  left() {
    this.selected--;
    if(this.selected<0){
      this.selected = this.pics.length - 1;
    }
    this.scrollToSelected();
  }
  right() {
    this.selected++;
    if (this.selected >= this.pics.length) {
      this.selected = 0;
    }
    this.scrollToSelected();
  }
  scrollToSelected() {
    const el = this.imgElements.toArray()[this.selected]?.nativeElement;
    if (el) {
      el.scrollIntoView({ behavior: 'smooth', inline: 'center', block: 'nearest' });
    }
  }

  openPic(index:number) {
    this.selected = index;
    this.modal.show=true;
  }

}
