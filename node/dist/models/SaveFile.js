"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.savefFile = void 0;
class SaveFile {
    ConstructProfile(_Name, _DateMade, _DateSeen, _TimePlayed) {
        let profile = new Profile(_Name, _DateMade, _DateSeen, _TimePlayed);
        this.Profile = profile;
    }
    ConstructMap(_x, _y) {
        let map = new Wep(_x, _y);
        this.Map = map;
    }
}
exports.savefFile = new SaveFile();
//# sourceMappingURL=SaveFile.js.map