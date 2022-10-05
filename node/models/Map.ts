import type { Building } from "./Building";
export interface Wep {
  xRange: Number;
  yRange: Number;

  grid: Cell[];
}

//child class
interface Cell {
  x: Number;
  y: Number;
  type: Number;
  building: Building;
}
