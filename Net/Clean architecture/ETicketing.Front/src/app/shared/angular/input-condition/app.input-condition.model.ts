export class VConditionModel
{
  text?: string | undefined;
  compressed?: boolean | false;
  nodeType: VConditionNodeType;
  operatorType?: VConditionOperatorType;
  comparatorType?: VConditionComparatorType;
  children?: VConditionModel[] ;
  tableId?: number | undefined;
  fieldId?: number | undefined;
  value?: any | undefined;
  constructor() {}
}

export class VConditionTableModel {
  id: number | undefined;
  name: string | undefined;
  fields: VConditionFieldModel[];
  constructor() { }
}
export class VConditionFieldModel {
  id: number | undefined;
  name: string | undefined;
  type: VConditionFieldType
  constructor() { }
}
export enum VConditionNodeType {
  Operator,
  Comparator
}
export enum VConditionFieldType {
  Number,
  Text,
  Date,
  Bool
}
export enum VConditionOperatorType {
  AND,
  OR
}
export enum VConditionComparatorType {
  Equal,
  NotEqual,
  GreaterThan,
  Greater,
  LessThan,
  Less,
  Contains,
  NotContains
}
