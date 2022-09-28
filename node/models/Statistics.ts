class Statistics {
  networth: number;
  money: number;
  data: number;
  xp: number;
  level: number;

  constructor
  (
    _networth: number,
    _money: number,
    _data: number,
    _xp: number,
    _level: number
  )
  {
    if(_networth != null)this.networth = _networth;

    if(_money != null)this.money = _money;

    if(_data != null)this.data = _data;

    if(_xp != null)this.xp = _xp;

    if(_level != null)this.level = _level;
  }
}
