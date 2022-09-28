class Wep {
  xRange: Number;
  yRange: Number;

  grid: Cell[];

  //wep constructor
  constructor(_xRange: Number, _yRange: Number) {
    if (_xRange != null) this.xRange = _xRange;

    if (_yRange != null) this.yRange = _yRange;
  }

  //constructor for child
  ConstructCell(_x: Number, _y: Number, _type: Number) {
    let cell = new Cell(_x, _y, _type);

    this.grid.push(cell);
  }
}

//child class
class Cell {
  x: Number;
  y: Number;
  type: Number;
  building: Building;

  constructor(_x: Number, _y: Number, _type: Number) {
    if (_x != null) this.x = _x;

    if (_y != null) this.y = _y;

    if (_type != null) this.type = _type;
  }

  ConstructBuild(_x: number, _y: number, _objType: number) {
    let building = new Building(_x, _y, _objType);
  }
}
