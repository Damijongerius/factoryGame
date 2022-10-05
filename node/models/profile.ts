import type { Statistics } from "./Statistics";

export interface Profile {
  Name: string;
  DateMade: Date;
  DateSeen: Date;
  TimePlayed: string;
  statistics: Statistics;
}
