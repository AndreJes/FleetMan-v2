"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AuthToken = void 0;
var AuthToken = /** @class */ (function () {
    function AuthToken(token, expires_in) {
        this.token = token;
        this.expires_in = expires_in;
        this.created_on = new Date();
    }
    AuthToken.prototype.TokenExpired = function () {
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
            console.error("[tokenExpired] Erro ao obter informa\u00E7\u00E3o de token expirado:");
            console.error("" + e);
            throw e;
        }
    };
    return AuthToken;
}());
exports.AuthToken = AuthToken;
//# sourceMappingURL=auth-token.js.map