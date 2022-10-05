export interface Building {
  x: number;
  y: number;
  objType: number;
  info: BuildingInfo;
}

interface BuildingInfo {
  dataStored: number;
  powerStored: number;
  level: number;
  age: number;
  upkeepCost: number;
  dataMined: number;
  dataSold: number;
  dataTransferd: number;
}
