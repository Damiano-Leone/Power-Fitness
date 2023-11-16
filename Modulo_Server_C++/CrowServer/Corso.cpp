#include "Corso.h"

Corso::Corso(int Id)
{
    id = Id;
}

Corso::Corso(string nomeCorso, string DataInizioCorso, string DataFineCorso, string Descrizione)
{
    nome = nomeCorso;
    dataInizioCorso = DataInizioCorso;
    dataFineCorso = DataFineCorso;
    descrizione = Descrizione;
}

Corso::Corso(int Id, string nomeCorso, string DataInizioCorso, string DataFineCorso, string Descrizione)
{
    id = Id;
    nome = nomeCorso;
    dataInizioCorso = DataInizioCorso;
    dataFineCorso = DataFineCorso;
    descrizione = Descrizione;
}

void Corso::addCorso(crow::response& res, DatabaseManager& dbManager)
{
    string query = "INSERT INTO corsi(nome_corso, data_inizio_corso, data_fine_corso, descrizione) VALUES ('"
        + nome + "', '"
        + dataInizioCorso + "', '" + dataFineCorso + "', '" + descrizione + "');";
    dbManager.executeUpdate(query, res);
}

void Corso::getCorsi(crow::response& res, DatabaseManager& dbManager)
{
    string query = "SELECT * FROM corsi;";
    sql::ResultSet* result = dbManager.executeQuery(query);

    if (result->next()) {

        nlohmann::json jsonArray;

        do {
            // Ottieni i valori delle colonne e gestiscili come desiderato
            nlohmann::json jsonData;
            jsonData["id"] = result->getInt("id");
            jsonData["nome_corso"] = result->getString("nome_corso");
            jsonData["data_inizio_corso"] = result->getString("data_inizio_corso");
            jsonData["data_fine_corso"] = result->getString("data_fine_corso");
            jsonData["descrizione"] = result->getString("descrizione");

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

void Corso::getCorso(crow::response& res, DatabaseManager& dbManager) {
    string query = "SELECT * FROM corsi where id =" + to_string(id) + ";";
    sql::ResultSet* result = dbManager.executeQuery(query);

    if (result->next()) {

        // Ottieni i valori delle colonne e gestiscili come desiderato
        nlohmann::json jsonData;

        jsonData["id"] = result->getInt("id");
        jsonData["nome_corso"] = result->getString("nome_corso");
        jsonData["data_inizio_corso"] = result->getString("data_inizio_corso");
        jsonData["data_fine_corso"] = result->getString("data_fine_corso");
        jsonData["descrizione"] = result->getString("descrizione");

        res.body = jsonData.dump();
        res.code = 200;
        res.end();
        delete result;
        return;
    }
    else {
        res.body = "Nessuna riga presente in tabella";
        res.code = 404;
        res.end();
        return;
    }
}

void Corso::updateCorso(crow::response& res, DatabaseManager& dbManager)
{
    string query = "UPDATE corsi SET nome_corso = '" + nome + "', data_inizio_corso = '"
        + dataInizioCorso + "', data_fine_corso = '" + dataFineCorso
        + "', descrizione = '" + descrizione
        + "' WHERE id = " + to_string(id) + ";";

    dbManager.executeUpdate(query, res);
}

void Corso::deleteCorso(crow::response& res, DatabaseManager& dbManager)
{
    string query = "DELETE FROM corsi WHERE id = " + to_string(id) + ";";
    dbManager.executeUpdate(query, res);
}





