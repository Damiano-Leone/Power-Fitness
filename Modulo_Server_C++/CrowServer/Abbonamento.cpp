#include "Abbonamento.h"

Abbonamento::Abbonamento()
{
}

Abbonamento::Abbonamento(int Id)
{
	id = Id;
}
Abbonamento::Abbonamento(int IdUtente, string DurataAbbonamento)
{
	idUtente = IdUtente;
	durataAbbonamento = DurataAbbonamento;
}
Abbonamento::Abbonamento(int IdUtente, string DataIscrizione, string DurataAbbonamento)
{
	idUtente = IdUtente;
	dataIscrizione = DataIscrizione;
	durataAbbonamento = DurataAbbonamento;
}
Abbonamento::Abbonamento(int Id, int IdUtente, string DataIscrizione, string DurataAbbonamento)
{
	id = Id;
	idUtente = IdUtente;
	dataIscrizione = DataIscrizione;
	durataAbbonamento = DurataAbbonamento;
}

void Abbonamento::setIdUtente(int IdUtente)
{
	idUtente = IdUtente;
}

void Abbonamento::addAbbonamento(crow::response& res, DatabaseManager& dbManager)
{
	time_t dIscrizione = TimeManager::dateStringToTime(dataIscrizione);
	time_t now = time(NULL);

	now = TimeManager::getMidnightCurrentDate(now);

	if (dIscrizione >= now) {
		//Controllo se vi sono abbonamenti attivi per l'utente 
		string queryAbbonamenti = "SELECT * FROM abbonamenti where id_utente =" + to_string(idUtente) + ";";
		sql::ResultSet* result = dbManager.executeQuery(queryAbbonamenti);

		if (result->next()) {
			nlohmann::json jsonArray;
			do {
				nlohmann::json jsonData;
				string data_iscri = result->getString("data_inizio");
				string tipo_abb = result->getString("durata_abbonamento");

				// calcolo la scadenza dell'abbonamento trovato per l'utente e verifico se è scaduto o meno 
				time_t time1 = TimeManager::dateStringToTime(data_iscri);
				time1 = TimeManager::calcolaScadenza(time1, tipo_abb);
				if (time1 >= now) {
					// non è ancora scaduto
					res.body = "Non e' possibile creare un nuovo abbonamento quando ne e' gia' presente uno attivo. Si puo' procedere con il rinnovo dell'abbonamento esistente.";
					res.code = 409;
					res.end();
					return;
				}
			} while (result->next());
			delete result;
		}
		//Inserisco un nuovo abbonamento se non ho trovato abbonamenti attivi
		string query = "INSERT INTO abbonamenti(id_utente,data_inizio, durata_abbonamento) VALUES ("
			+ to_string(idUtente) + ", '" + dataIscrizione + "', '" + durataAbbonamento + "');";

		dbManager.executeUpdate(query, res);
	}

	else {
		res.body = "Non e' possibile creare abbonamenti con date precedenti a quella odierna";
		res.code = 409;
		res.end();
	}

}

void Abbonamento::rinnovaAbbonamento(crow::response& res, DatabaseManager& dbManager)
{
	time_t dRinnovo = TimeManager::dateStringToTime(dataIscrizione);
	time_t now = time(NULL);
	now = TimeManager::getMidnightCurrentDate(now);
	time_t scadenza = now;

	string queryAbbonamenti = "SELECT * FROM abbonamenti where id_utente=" + to_string(idUtente) +
		" && data_inizio=(select max(data_inizio) from abbonamenti where id_utente= "
		+ to_string(idUtente) + ");";

	sql::ResultSet* result = dbManager.executeQuery(queryAbbonamenti);
	bool AbbonamentoAttivo = false;

	string data_iscri;
	if (result->next()) {
		do {
			nlohmann::json jsonData;
			data_iscri = result->getString("data_inizio");
			string tipo_abb = result->getString("durata_abbonamento");
			time_t time1 = TimeManager::dateStringToTime(data_iscri);
			time1 = TimeManager::getMidnightCurrentDate(time1);
			// calcolo la scadenza dell'abbonamento trovato per l'utente e verifico se è scaduto o meno 
			time1 = TimeManager::calcolaScadenza(time1, tipo_abb);
			if (time1 >= scadenza) {
				scadenza = time1; // imposto la scadenza alla scadenza dell'abbonamento attivo
				AbbonamentoAttivo = true;
			}
		} while (result->next());
		delete result;
	}

	if (AbbonamentoAttivo)
	{
		time_t dataStart = TimeManager::dateStringToTime(data_iscri);
		dataStart = TimeManager::getMidnightCurrentDate(dataStart);

		// Rieffettuo il controllo sull'abbonamento attivo perché voglio avere al più un solo rinnovo presente
		// Ed in questo modo se esiste già un rinnovo dataStart e scadenza saranno relativi al rinnovo
		if (dataStart <= now && scadenza >= now && dRinnovo > scadenza) {
			string query = "INSERT INTO abbonamenti(id_utente,data_inizio, durata_abbonamento) VALUES ("
				+ to_string(idUtente) + ", '" + dataIscrizione + "', '" + durataAbbonamento + "');";
			dbManager.executeUpdate(query, res);
			return;
		}
		else {
			res.body = "E' impossibile procedere con un ulteriore rinnovo se ne e' gia' stato effettuato uno in precedenza";
		}
	}
	else {
		res.body = "Non e' possibile rinnovare un abbonamento in assenza di uno attualmente attivo. Si prega di creare un nuovo abbonamento utilizzando la route 'addAbbonamento'";
	}
	res.code = 404;
	res.end();
	return;
}

void Abbonamento::updateAbbonamento(crow::response& res, DatabaseManager& dbManager)
{
	time_t now = TimeManager::getMidnightCurrentDate(time(NULL));
	time_t newDataUpdate = TimeManager::dateStringToTime(dataIscrizione);

	if (newDataUpdate >= now) {

		//controllo se l'abbonamento che si vuole modificare non è già in corso
		string queryAbbonamenti = "SELECT * FROM abbonamenti where id=" + to_string(id) + ";";
		sql::ResultSet* result = dbManager.executeQuery(queryAbbonamenti);

		if (result->next()) {
			string data_iscri = result->getString("data_inizio");
			string tipo_abb = result->getString("durata_abbonamento");

			time_t dataStart = TimeManager::dateStringToTime(data_iscri);
			time_t scadenza = TimeManager::calcolaScadenza(dataStart, tipo_abb);

			if (dataStart <= now && now <= scadenza) {
				res.body = "Non e' possibile modificare un abbonamento in corso";
				res.code = 409;
				res.end();
				delete result;
				return;
			}
		}
		delete result;

		// Troviamo la data di inizio più recente tra tutti gli abbonamenti esistenti, quindi, quella del rinnovo
		// e confrontiamo la nuova data di inizio con la scadenza dell'abbonamento attivo affinché non si sovrappongano
		string queryMaxDataInizio = "SELECT MAX(data_inizio) AS max_data_inizio, durata_abbonamento \
			 FROM abbonamenti WHERE id_utente=" + to_string(idUtente) + ";";
		sql::ResultSet* result1 = dbManager.executeQuery(queryMaxDataInizio);

		time_t maxDataInizio = now;

		if (result1->next()) {
			string maxDataString = result1->getString("max_data_inizio");
			string tipo_abb = result1->getString("durata_abbonamento");
			maxDataInizio = TimeManager::dateStringToTime(maxDataString);
			time_t scadenza = TimeManager::calcolaScadenza(maxDataInizio, tipo_abb);
			if (scadenza <= now) {
				res.body = "Non e' possibile modificare un abbonamento gia' scaduto.";
				res.code = 409;
				res.end();
				delete result1;
				return;
			}
		}
		delete result1;


		string query1 = "SELECT * FROM abbonamenti where id_utente=" + to_string(idUtente) + ";";
		sql::ResultSet* result2 = dbManager.executeQuery(query1);

		time_t scadenzaAbbAttivo = now;

		if (result2->next()) {
			string data;
			string tipo;
			do {
				data = result2->getString("data_inizio");
				tipo = result2->getString("durata_abbonamento");

				time_t start = TimeManager::dateStringToTime(data);
				time_t scadenza = TimeManager::calcolaScadenza(start, tipo);

				if (start <= now && now <= scadenza) {
					scadenzaAbbAttivo = scadenza;
				}

			} while (result2->next());
			delete result2;
		}


		// Verifica se la nuova data di inizio è successiva alla scadenza dell'abbonamento attivo affinchè non si sovrappongano
		if (newDataUpdate < scadenzaAbbAttivo) {
			res.body = "Non e' possibile effettuare la modifica.\nPer la data di inizio inserita esiste gia' un abbonamento attualmente attivo.\nSeleziona una data di inizio successiva.";
			res.code = 409;
			res.end();
			return;
		}

		string query = "update abbonamenti set data_inizio='" + dataIscrizione + "', durata_abbonamento='"
			+ durataAbbonamento + "' where id=" + to_string(id) + ";";
		dbManager.executeUpdate(query, res);
	}
	res.body = "La data di iscrizione deve essere pari o successiva a oggi";
	res.code = 409;
	res.end();
	return;
}

void Abbonamento::deleteAbbonamento(crow::response& res, DatabaseManager& dbManager)
{
	time_t now = TimeManager::getMidnightCurrentDate(time(NULL));
	string query1 = "SELECT * FROM abbonamenti WHERE id = " + to_string(id) + ";";
	sql::ResultSet* result1 = dbManager.executeQuery(query1);
	if (result1->next()) {
		string dataInizio = result1->getString("data_inizio");

		time_t start = TimeManager::dateStringToTime(dataInizio);
		start = TimeManager::getMidnightCurrentDate(start);
		time_t scadenza = TimeManager::calcolaScadenza(start, result1->getString("durata_abbonamento"));
		// Se è stato attivato più di 3 giorni fa non lo faccio eliminare
		if (start <= now && now <= scadenza) {
			res.body = "Non e' possibile eliminare un abbonamento attivo.";
			res.code = 409;
			res.end();
			delete result1;
			return;
		}
	}
	delete result1;
	string query = "DELETE FROM abbonamenti WHERE id = " + to_string(id) + ";";
	dbManager.executeUpdate(query, res);
}

void Abbonamento::getAbbonamentiUser(crow::response& res, DatabaseManager& dbManager)
{
	string query = "SELECT * FROM abbonamenti where id_utente=" + to_string(idUtente) + ";";
	sql::ResultSet* result = dbManager.executeQuery(query);
	nlohmann::json jsonArray = nlohmann::json::array();

	if (result->next()) {

		do {
			// Ottieni i valori delle colonne e gestiscili come desiderato
			nlohmann::json jsonData;

			jsonData["id"] = result->getInt("id");
			jsonData["data_inizio"] = result->getString("data_inizio");
			jsonData["durata_abbonamento"] = result->getString("durata_abbonamento");

			jsonArray.push_back(jsonData);

		} while (result->next());

		delete result;

		res.body = jsonArray.dump();
		res.set_header("Content-Type", "application/json");
		res.code = 200;
		res.end();
	}

	else {
		res.body = "Nessun abbonamento per questo utente";
		res.code = 404;
		res.end();
	}
	return;
}

void Abbonamento::getAbbonamentoAttivoUser(crow::response& res, DatabaseManager& dbManager)
{
	time_t now = TimeManager::getMidnightCurrentDate(time(NULL));
	string query = "SELECT * FROM abbonamenti where id_utente=" + to_string(idUtente) + ";";
	sql::ResultSet* result = dbManager.executeQuery(query);
	nlohmann::json jsonData;

	bool abbonamentoAttivoTrovato = false;

	if (result->next()) {

		do {

			jsonData["id"] = result->getInt("id");
			jsonData["data_inizio"] = result->getString("data_inizio");
			jsonData["durata_abbonamento"] = result->getString("durata_abbonamento");

			time_t start = TimeManager::dateStringToTime(result->getString("data_inizio"));
			start = TimeManager::getMidnightCurrentDate(start);
			time_t scadenza = TimeManager::calcolaScadenza(start, result->getString("durata_abbonamento"));

			if (start <= now && now <= scadenza) {
				abbonamentoAttivoTrovato = true;
			}

		} while (result->next());

		delete result;
	}

	if (abbonamentoAttivoTrovato) {
		res.body = jsonData.dump();
		res.set_header("Content-Type", "application/json");
		res.code = 200;
		res.end();
	}

	else {
		res.body = "Nessun abbonamento attivo per questo utente";
		res.code = 404;
		res.end();
	}
	return;
}
