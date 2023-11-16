#include "User.h"

User::User(int Id)
{
	id = Id;
}

User::User(int Id, string Cellulare, string Email, string Indirizzo, string NumeroCivico)
{
	id = Id;
	cellulare = Cellulare;
	email = Email;
	indirizzo = Indirizzo;
	numeroCivico = NumeroCivico;
}

User::User(string Cf, string Cellulare, string Email, string Indirizzo, string NumeroCivico)
{
	codiceFiscale = Cf;
	cellulare = Cellulare;
	email = Email;
	indirizzo = Indirizzo;
	numeroCivico = NumeroCivico;
}

User::User(string Cf, string Nome, string Cognome, string Genere, string DataNascita, string Cellulare, string Email, string Indirizzo, string NumeroCivico)
{
	codiceFiscale = Cf;
	nome = Nome;
	cognome = Cognome;
	genere = Genere;
	dataNascita = DataNascita;
	cellulare = Cellulare;
	email = Email;
	indirizzo = Indirizzo;
	numeroCivico = NumeroCivico;
}

void User::addUser(crow::response& res, DatabaseManager& dbManager)
{
	string query = "INSERT INTO users(codice_fiscale, nome, cognome, genere, data_nascita, \
		cellulare, email, indirizzo, numero_civico) VALUES('" + codiceFiscale + "', '" \
		+ nome + "', '" + cognome + "','" + genere + "','" + dataNascita + "','" + cellulare + \
		"','" + email + "','" + indirizzo + "','" + numeroCivico + "');";
	dbManager.executeUpdate(query, res);

}

void User::updateUser(crow::response& res, DatabaseManager& dbManager)
{
	string query = "UPDATE users SET cellulare = '" + cellulare +
		"', email = '" + email + "' , indirizzo = '" + indirizzo +
		" ', numero_civico = '" + numeroCivico +
		"' WHERE id = '" + to_string(id) + "';";

	dbManager.executeUpdate(query, res);
}

void User::deleteUser(crow::response& res, DatabaseManager& dbManager)
{
	string query = "DELETE FROM users WHERE id = '" + to_string(id) + "';";
	dbManager.executeUpdate(query, res);
}

void User::getUsers(crow::response& res, DatabaseManager& dbManager)
{
	string query = "SELECT * FROM users;";
	sql::ResultSet* result = dbManager.executeQuery(query);

	if (result->next()) {

		nlohmann::json jsonArray;

		do {
			// Ottieni i valori delle colonne e gestiscili come desiderato
			nlohmann::json jsonData;

			jsonData["id"] = result->getInt("id");
			jsonData["codice_fiscale"] = result->getString("codice_fiscale");
			jsonData["nome"] = result->getString("nome");
			jsonData["cognome"] = result->getString("cognome");
			jsonData["genere"] = result->getString("genere");
			jsonData["data_nascita"] = result->getString("data_nascita");
			jsonData["cellulare"] = result->getString("cellulare");
			jsonData["email"] = result->getString("email");
			jsonData["indirizzo"] = result->getString("indirizzo");
			jsonData["numero_civico"] = result->getString("numero_civico");
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
