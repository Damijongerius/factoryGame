"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.savefFile = void 0;
const profile_1 = require("./profile");
class SaveFile {
    ConstructProfile(_Name, _DateMade, _DateSeen, _TimePlayed) {
        //hij zegt dat dit niet bestaat probeer wat dingen uit zoals het ergens anders proberen maken van deze functie
        let profile = new profile_1.Profile(_Name, _DateMade, _DateSeen, _TimePlayed);
        this.Profile = profile;
    }
    ConstructMap(_x, _y) {
        let map = new Wep(_x, _y);
        this.Map = map;
    }
}
exports.savefFile = new SaveFile();
//# sourceMappingURL=SaveFile.js.map