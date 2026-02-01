import { Component, Input } from '@angular/core';
import { VConditionModel, VConditionNodeType } from '../app.input-condition.model';
import { VIconButtonComponent } from '../../icon-button/app.icon-button';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-input-condition-treenode',
  imports: [CommonModule, VIconButtonComponent, FormsModule],
  templateUrl: './app.input-condition-treenode.html',
  styleUrls: ['./app.input-condition-treenode.scss']
})
export class VInputConditionTreeNodeComponent {
  @Input() nodeTypes: any[];
  @Input() operationTypes: any[];
  @Input() comparationTypes: any[];
  @Input() tableList: any[];
  @Input() node: VConditionModel;
  @Input() parentNode: VConditionModel;
  NodeType = VConditionNodeType;

  toggleCompress(node: VConditionModel) {
    node.compressed = !node.compressed;
  }
  getTreeText() {
    let result: string = "";
    if (this.node.nodeType == VConditionNodeType.Operator) {
      if (this.node.operatorType != null) {
        result = `${this.operationTypes.find(x => x.id == this.node.operatorType)?.name}`;
      }
    }
    else if (this.node.nodeType == VConditionNodeType.Comparator) {
      if (this.node.tableId != null) {
        result += `${this.tableList.find(x => x.id == this.node.tableId)?.name}`;
        if (this.node.fieldId != null) {
          result += ` ${this.tableList.find(x => x.id == this.node.tableId)?.fields.find(x => x.id == this.node.fieldId)?.name}`;
          if (this.node.comparatorType != null) {
            result += ` ${this.comparationTypes.find(x => x.id == this.node.comparatorType)?.name}`;
            if (this.node.value != null) {
              result += ` ${this.node.value}`;
            }
          }
        }
      }
    }
    return result;
  }
}
