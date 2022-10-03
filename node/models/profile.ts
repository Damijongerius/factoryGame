export class Profile {
  Name: string;
  DateMade: Date;
  DateSeen: Date;
  TimePlayed: string;
  Statistics: Statistics;

  ConstructStats(
    _networth: number,
    _money: number,
    _data: number,
    _xp: number,
    _level: number
  ) {
    this.Statistics = new Statistics(_networth, _money, _data, _xp, _level);
  }

  constructor(
    _Name: string,
    _DateMade: Date,
    _DateSeen: Date,
    _TimePlayed: string
  ) {
    if (_Name != null) this.Name = _Name;

    if (_DateMade != null) this.DateMade = _DateMade;

    if (_DateSeen != null) this.DateSeen = _DateSeen;

    if (_TimePlayed != null) this.TimePlayed = _TimePlayed;
  }
}
