#include "Login.h"

Login::Login(string Mail, string Password)
{
	mail = Mail; 
	password = Password; 
}

void Login::effettuaLogin(crow::response& res, DatabaseManager& dbManager)
{
	string query1 = "SELECT * FROM dipendenti where mail = '" + mail + "';";
	sql::ResultSet* result1 = dbManager.executeQuery(query1);

	if (result1->next()) {
		string query = "SELECT * FROM dipendenti where mail = '" + mail + "' && password = '" + password + "';";
		sql::ResultSet* result = dbManager.executeQuery(query);

		if (result->next()) {
			int id = result->getInt("id");

			bool confirmed = result->getInt("confirmed");
			if (confirmed) {
				auto token = jwt::create()
					.set_issuer("auth0")
					.set_type("JWS")
					.set_payload_claim("user", jwt::claim(to_string(id)))
					.sign(jwt::algorithm::hs256{ password });
				cout << token << endl;
				delete result;
				res.body = "Token:" + token;
				res.code = 200;
				res.end();
			}
			else {
				delete result;
				res.body = "Spiacente, devi attendere l'approvazione da parte dell'amministratore prima di poter effettuare l'accesso.";
				res.code = 500;
				res.end();
			}
		}
		else {
			delete result;
			res.body = "La password inserita non e' corretta.";
			res.code = 500;
			res.end();
		}
	}

	else {
		res.body = "Non e' stato possibile trovare un account associato a queste informazioni.";
		res.code = 500;
		res.end();
	}
}

