"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ErrorHandler = void 0;
class ErrorHandler {
    OnConnection(err, result) {
        console.log("something went wrong while connecting");
    }
    OnInsert(err, result) {
        console.log("couldn't insert");
    }
}
exports.ErrorHandler = ErrorHandler;
//# sourceMappingURL=ErrorHandler.js.map