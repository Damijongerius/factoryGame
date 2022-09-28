class SaveFile {
  Profile: Profile;
  Map: Wep;

  ConstructProfile
  (
    _Name: string,
    _DateMade: Date,
    _DateSeen: Date,
    _TimePlayed: string,
    _Statistics: Statistics
  )
  {
    let profile = new Profile(_Name,_DateMade,_DateSeen,_TimePlayed,_Statistics);
    this.Profile = profile;
  }

  ConstructMap
  (
    _x: Number,
    _y: Number,
  )
  {
    let map = new Wep(_x,_y);
    this.Map = map;
  }
}

export const savefFile = new SaveFile();
