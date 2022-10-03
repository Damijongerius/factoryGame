"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Profile = void 0;
class Profile {
    constructor(_Name, _DateMade, _DateSeen, _TimePlayed) {
        if (_Name != null)
            this.Name = _Name;
        if (_DateMade != null)
            this.DateMade = _DateMade;
        if (_DateSeen != null)
            this.DateSeen = _DateSeen;
        if (_TimePlayed != null)
            this.TimePlayed = _TimePlayed;
    }
    ConstructStats(_networth, _money, _data, _xp, _level) {
        let statistics = new Statistics(_networth, _money, _data, _xp, _level);
    }
}
exports.Profile = Profile;
//# sourceMappingURL=profile.js.map