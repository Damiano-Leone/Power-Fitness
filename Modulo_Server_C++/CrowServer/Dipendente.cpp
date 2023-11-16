#include "Dipendente.h"

Dipendente::Dipendente()
{
}

void Dipendente::getDipendenti(crow::response& res, DatabaseManager& dbManager)
{
    string query = "SELECT * FROM dipendenti where mail != 'admin';";
    sql::ResultSet* result = dbManager.executeQuery(query);

    if (result->next()) {

        nlohmann::json jsonArray;

        do {
            // Ottieni i valori delle colonne e gestiscili come desiderato
            nlohmann::json jsonData;
            jsonData["id"] = result->getInt("id");
            jsonData["mail"] = result->getString("mail");
            jsonData["password"] = result->getString("password");
            jsonData["ruolo"] = result->getString("ruolo");
            jsonData["nome"] = result->getDouble("nome");
            jsonData["cognome"] = result->getString("cognome");
            jsonData["confirmed"] = result->getString("confirmed");

            jsonArray.push_back(jsonData);

        } while (result->next());

        delete result;
        res.body = jsonArray.dump();
        res.set_header("Content-Type", "application/json");
        res.code = 200;
        res.end();
    }
    else {
        res.body = "Nessuna riga presente in tabella";
        res.code = 404;
        res.end();
    }
}
