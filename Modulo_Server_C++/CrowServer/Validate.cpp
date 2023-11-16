#include "Validate.h"

Validate::Validate(int Id)
{
	id = Id;
}

void Validate::validateDip(crow::response& res, DatabaseManager& dbManager, string token)
{
	auto decoded = jwt::decode(token);
	nlohmann::json jsonData = nlohmann::json::parse(decoded.get_payload());
	string id_utente = jsonData["user"];
	cout << id_utente << endl; 

	string query = "SELECT ruolo FROM dipendenti where id =" + id_utente + ";";
	sql::ResultSet* result = dbManager.executeQuery(query);

	if (result->next()) {
		string ruolo = result->getString("ruolo");
		if (ruolo == "admin") {
			string query = "UPDATE dipendenti SET confirmed = TRUE WHERE id = " + to_string(id) + ";";
			dbManager.executeUpdate(query, res);
			return;
		}
		else
		{
			res.body = "Effettua l'accesso come admin per effettuare la validazione di un dipendente ";
			res.code = 500;
			res.end();
		}

	}

}
