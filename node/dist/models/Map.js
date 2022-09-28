class Wep {
    //wep constructor
    constructor(_xRange, _yRange) {
        if (_xRange != null)
            this.xRange = _xRange;
        if (_yRange != null)
            this.yRange = _yRange;
    }
    //constructor for child
    ConstructCell(_x, _y, _type) {
        let cell = new Cell(_x, _y, _type);
        this.grid.push(cell);
    }
}
//child class
class Cell {
    constructor(_x, _y, _type) {
        if (_x != null)
            this.x = _x;
        if (_y != null)
            this.y = _y;
        if (_type != null)
            this.type = _type;
    }
    ConstructBuild(_x, _y, _objType) {
        let building = new Building(_x, _y, _objType);
    }
}
//# sourceMappingURL=Map.js.map