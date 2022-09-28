class Wep {

  xRange: Number;
  yRange: Number;

  grid: Cell[];

  //wep constructor
  constructor
  (
    _xRange: Number,
    _yRange: Number
  )
  {
    if(_xRange != null)this.xRange = _xRange;

    if(_yRange != null)this.yRange = _yRange;
  }
  
  //constructor for child
  ConstructCell
  (
    _x: Number,
    _y: Number,
    _type: Number,
    _building: Building
  )
  {
    let cell = new Cell(_x,_y,_type,_building);

    this.grid.push(cell);
  }
}

//child class
class Cell{
  x: Number;
  y: Number;
  type: Number;
  building: Building;

  constructor
  (
    _x: Number,
    _y: Number,
    _type: Number,
    _building: Building
  )
  {
    if(_x != null)this.x = _x;

    if(_y != null)this.y = _y;

    if(_type != null)this.type = _type;

    if(_building != null)this.building = _building;
  }
}
