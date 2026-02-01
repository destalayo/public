import { Component, Input } from '@angular/core';
import { VConditionComparatorType, VConditionFieldType, VConditionModel, VConditionNodeType, VConditionOperatorType, VConditionTableModel } from '../app.input-condition.model';
import { VSelectComponent } from '../../select/app.select';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { VIconButtonComponent } from '../../icon-button/app.icon-button';
import { VInputCheckComponent } from '../../input-check/app.input-check';
import { VDateTimePickerComponent } from '../../input-datetime/app.input-datetime';
import { VInputNumberComponent } from '../../input-number/app.input-number';
import { VInputTextComponent } from '../../input-text/app.input-text';
@Component({
  selector: 'app-input-condition-gridnode',
  imports: [CommonModule, VSelectComponent,VIconButtonComponent,VInputCheckComponent,VInputTextComponent, VInputNumberComponent, VDateTimePickerComponent, FormsModule],
  templateUrl: './app.input-condition-gridnode.html',
  styleUrls: ['./app.input-condition-gridnode.scss']
})
export class VInputConditionGridNodeComponent {
  @Input() nodeTypes: any[];
  @Input() operationTypes: any[];
  @Input() comparationTypes: any[];
  @Input() tableList: VConditionTableModel[];
  @Input() node: VConditionModel;
  @Input() parentNode: VConditionModel;
  NodeType = VConditionNodeType;
  FieldType = VConditionFieldType;

  fieldType:VConditionFieldType;

  addChild() {
    this.checkChildren();
    this.node.children.push({ nodeType: VConditionNodeType.Operator, operatorType: VConditionOperatorType.AND });
  }
  removeNode() {
    this.checkChildren();
    let index=this.parentNode.children.indexOf(this.node);
    if (index > -1) {
      this.parentNode.children.splice(index, 1);
    }
  }
  checkChildren() {
    if (this.node.children==null) {
      this.node.children = [];
    }
  }
  onChangeType() {
    if (this.node.nodeType == VConditionNodeType.Comparator) {
      this.node.operatorType = null;
      this.node.children = [];
    }
    else if (this.node.nodeType == VConditionNodeType.Operator) {
      this.node.comparatorType = null;
      this.node.tableId = null;
      this.node.fieldId = null;
      this.node.value = null;
      this.fieldType = null;
    }
  }
  getComparations() {
    let aux = [];
    if (this.node.tableId !=null && this.node.fieldId!=null) {
      this.fieldType = this.tableList.find(x => x.id == this.node.tableId)?.fields.find(x => x.id == this.node.fieldId)?.type;
      if (this.fieldType == VConditionFieldType.Text) {
        aux = [VConditionComparatorType.Equal, VConditionComparatorType.NotEqual, VConditionComparatorType.Contains, VConditionComparatorType.NotContains];
      }
      else if (this.fieldType == VConditionFieldType.Number) {
        aux = [VConditionComparatorType.Equal, VConditionComparatorType.NotEqual, VConditionComparatorType.GreaterThan, VConditionComparatorType.Greater, VConditionComparatorType.Less, VConditionComparatorType.LessThan];
      }
      else if (this.fieldType == VConditionFieldType.Date) {
        aux = [VConditionComparatorType.Greater, VConditionComparatorType.Less];
      }
      else if (this.fieldType == VConditionFieldType.Bool) {
        aux = [VConditionComparatorType.Equal, VConditionComparatorType.NotEqual];
      }
    }
    return this.comparationTypes.filter(x=>aux.some(y=>y==x.id));
  }
  getFields() {
    return this.tableList.find(x => x.id == this.node.tableId)?.fields;
  }
}
