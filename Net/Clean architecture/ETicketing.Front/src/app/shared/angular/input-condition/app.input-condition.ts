import { Component, Input } from '@angular/core';
import { VConditionComparatorType, VConditionModel, VConditionNodeType, VConditionOperatorType } from './app.input-condition.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { VInputConditionGridNodeComponent } from './input-condition-gridnode/app.input-condition-gridnode';
import { VInputConditionTreeNodeComponent } from './input-condition-treenode/app.input-condition-treenode';
@Component({
  selector: 'app-input-condition',
  imports: [CommonModule, VInputConditionGridNodeComponent, VInputConditionTreeNodeComponent, FormsModule],
  templateUrl: './app.input-condition.html',
  styleUrls: ['./app.input-condition.scss']
})
export class VInputConditionComponent {
  @Input() tableList: any[];
  @Input() node: VConditionModel;
  @Input() parentNode: VConditionModel;
  nodeTypes = [
    { id: VConditionNodeType.Comparator, name: "Comparación" },
    { id: VConditionNodeType.Operator, name: "Operación" }
  ];
  operationTypes = [
    { id: VConditionOperatorType.AND, name: "(AND) todas" },
    { id: VConditionOperatorType.OR, name: "(OR) alguna" }
  ];
  comparationTypes = [
    { id: VConditionComparatorType.Equal, name: "Igual" },
    { id: VConditionComparatorType.NotEqual, name: "Diferente" },
    { id: VConditionComparatorType.GreaterThan, name: "Mayor o igual" },
    { id: VConditionComparatorType.Greater, name: "Mayor" },
    { id: VConditionComparatorType.LessThan, name: "Menor o igual" },
    { id: VConditionComparatorType.Less, name: "Menor" },
    { id: VConditionComparatorType.Contains, name: "Contiene" },
    { id: VConditionComparatorType.NotContains, name: "No contiene" }
  ];
  


}
