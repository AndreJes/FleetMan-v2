"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AuthToken = void 0;
var AuthToken = /** @class */ (function () {
    function AuthToken(token, expires_in, create_on) {
        this.token = token;
        //converts given seconds to miliseconds
        this.expires_in = expires_in * 1000;
        this.created_on = create_on ? create_on : new Date();
    }
    AuthToken.prototype.Expired = function () {
        try {
            var current_time = new Date();
            var limit = this.created_on.getTime() + this.expires_in;
            if (current_time.getTime() > limit) {
                return true;
            }
            else {
                return false;
            }
        }
        catch (e) {
            console.error("Erro ao obter informa\u00E7\u00E3o de token expirado:");
            console.error("" + e);
            throw e;
        }
    };
    AuthToken.fromSession = function (session_token) {
        return new this(session_token.token, session_token.expires_in / 1000, new Date(session_token.created_on));
    };
    return AuthToken;
}());
exports.AuthToken = AuthToken;
//# sourceMappingURL=auth-token.js.map