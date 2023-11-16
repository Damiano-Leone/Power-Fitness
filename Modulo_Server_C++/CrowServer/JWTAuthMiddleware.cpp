#include "JWTAuthMiddleware.h"

void JWTAuthMiddleware::setDb(DatabaseManager* pDb) {
    db = pDb;
}

void JWTAuthMiddleware::before_handle(crow::request& req, crow::response& res, context& ctx) {

    if (req.url == "/Login" || req.url == "/Sigin") {
        // Questa route non richiede un token valido
        return;
    }

    // Estrai il token dall'header delle richieste
    string token = req.get_header_value("Authorization");
    if (!token.empty()) {
        // Token presente, procedi
        if (!verifyToken(token)) {
            res = crow::response(401, "Unauthorized");
            res.end();
        }
    }
    else {
        res = crow::response(401, "Unauthorized");
        res.end();
    }
}

void JWTAuthMiddleware::after_handle(crow::request& req, crow::response& res, context& ctx) {}

bool JWTAuthMiddleware::verifyToken(string token) {
    auto decoded = jwt::decode(token);
    nlohmann::json jsonData = nlohmann::json::parse(decoded.get_payload());
    string id_utente = jsonData["user"];

    string query = "SELECT password FROM dipendenti where id =" + id_utente + ";";
    sql::ResultSet* result = db->executeQuery(query);

    if (result->next()) {
        string segreto = result->getString("password");
    
        try {
            auto verifier = jwt::verify()
                .allow_algorithm(jwt::algorithm::hs256{ segreto })
                .with_issuer("auth0");

            verifier.verify(decoded);
            return true;
        }
        catch (const exception&) {
            return false;
        }
    }
    else {
        cout << "utente non registrato " << endl;
        return false;
    }
}


