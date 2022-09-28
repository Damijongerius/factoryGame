class Building {
    constructor(_x, _y, _objType) {
        if (_x != null)
            this.x = _x;
        if (_y != null)
            this.y = _y;
        if (_objType != null)
            this.objType = _objType;
    }
    ConstructInfo(_dataStored, _powerStored, _level, _age, _upkeepCost, _dataMined, _dataSold, _dataTransferd) {
        let bi = new BuildingInfo(_dataStored, _powerStored, _level, _age, _upkeepCost, _dataMined, _dataSold, _dataTransferd);
        this.info = bi;
    }
}
class BuildingInfo {
    constructor(_dataStored, _powerStored, _level, _age, _upkeepCost, _dataMined, _dataSold, _dataTransferd) {
        if (_dataStored != null)
            this.dataStored = _dataStored;
        if (_powerStored != null)
            this.powerStored = _powerStored;
        if (_level != null)
            this.level = _level;
        if (_age != null)
            this.age = _age;
        if (_upkeepCost != null)
            this.upkeepCost = _upkeepCost;
        if (_dataMined != null)
            this.dataMined = _dataMined;
        if (_dataSold != null)
            this.dataSold = _dataSold;
        if (_dataTransferd != null)
            this.dataTransferd = _dataTransferd;
    }
}
//# sourceMappingURL=Building.js.map