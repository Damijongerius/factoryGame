class Building {
  x: number;
  y: number;
  objType: number;
  info: BuildingInfo;

  constructor(_x: number, _y: number, _objType: number) {
    if (_x != null) this.x = _x;

    if (_y != null) this.y = _y;

    if (_objType != null) this.objType = _objType;
  }

  ConstructInfo(
    _dataStored: number,
    _powerStored: number,
    _level: number,
    _age: number,
    _upkeepCost: number,
    _dataMined: number,
    _dataSold: number,
    _dataTransferd: number
  ) {
    let bi = new BuildingInfo(
      _dataStored,
      _powerStored,
      _level,
      _age,
      _upkeepCost,
      _dataMined,
      _dataSold,
      _dataTransferd
    );

    this.info = bi;
  }
}

class BuildingInfo {
  dataStored: number;
  powerStored: number;
  level: number;
  age: number;
  upkeepCost: number;
  dataMined: number;
  dataSold: number;
  dataTransferd: number;

  constructor(
    _dataStored: number,
    _powerStored: number,
    _level: number,
    _age: number,
    _upkeepCost: number,
    _dataMined: number,
    _dataSold: number,
    _dataTransferd: number
  ) {
    if (_dataStored != null) this.dataStored = _dataStored;

    if (_powerStored != null) this.powerStored = _powerStored;

    if (_level != null) this.level = _level;

    if (_age != null) this.age = _age;

    if (_upkeepCost != null) this.upkeepCost = _upkeepCost;

    if (_dataMined != null) this.dataMined = _dataMined;

    if (_dataSold != null) this.dataSold = _dataSold;

    if (_dataTransferd != null) this.dataTransferd = _dataTransferd;
  }
}
