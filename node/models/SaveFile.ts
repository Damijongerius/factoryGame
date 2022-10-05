import type { Profile } from "./profile";
import type { Wep } from "./Map";

export interface SaveFile {
  Profile: Profile;
  Map: Wep;

  // ConstructProfile(
  //   _Name: string,
  //   _DateMade: Date,
  //   _DateSeen: Date,
  //   _TimePlayed: string
  // ) {
  //   //hij zegt dat dit niet bestaat probeer wat dingen uit zoals het ergens anders proberen maken van deze functie
  //   let profile = new Profile(_Name, _DateMade, _DateSeen, _TimePlayed);
  //   this.Profile = profile;
  // }

  // ConstructMap(_x: Number, _y: Number) {
  //   let map = new Wep(_x, _y);
  //   this.Map = map;
  // }
}

// export const savefFile = new SaveFile();
