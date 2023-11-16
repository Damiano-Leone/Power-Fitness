#include "Prenotazione.h"

Prenotazione::Prenotazione()
{
}

Prenotazione::Prenotazione(int IdPrenotazione)
{
	id = IdPrenotazione;
}
Prenotazione::Prenotazione(int IdLezione, int IdUser, string Data)
{
	idLezione = IdLezione;
	idUser = IdUser;
	data = Data;
}
Prenotazione::Prenotazione(int Id, int IdLezione, int IdUser, string Data)
{
	id = Id;
	idLezione = IdLezione;
	idUser = IdUser;
	data = Data;
}

void Prenotazione::setIdUser(int IdUser)
{
	idUser = IdUser;
}

void Prenotazione::addPrenotazione(crow::response& res, DatabaseManager& dbManager)
{
	// controllo che giorno e' la data 
	time_t now = time(NULL);
	now = TimeManager::getMidnightCurrentDate(now);
	time_t data_t = TimeManager::dateStringToTime(data);
	char str_time[26];
	ctime_s(str_time, sizeof str_time, &data_t);
	string str(str_time, 3);  //(ex 13/09/23 ->Wes)
	int max = 0;

	//cerco l'abbonamento attivo 
	string query4 = "SELECT * FROM abbonamenti where id_utente =" + to_string(idUser) + ";";
	sql::ResultSet* result4 = dbManager.executeQuery(query4);

	bool abbonamentoAttivoTrovato = false;
	time_t scadenzaAbbAttivo;

	if (result4->next()) {
		do {

			time_t start = TimeManager::dateStringToTime(result4->getString("data_inizio"));
			time_t scadenza = TimeManager::calcolaScadenza(start, result4->getString("durata_abbonamento"));

			if (start <= now && now <= scadenza) {
				abbonamentoAttivoTrovato = true;
				scadenzaAbbAttivo = scadenza;
			}
		} while (result4->next());

		delete result4;
	}

	if (abbonamentoAttivoTrovato) {

		int idCorso = -1;
		if (data_t <= scadenzaAbbAttivo) {

			if (data_t >= now) {

				string query = "SELECT * FROM lezioni where id =" + to_string(idLezione) + ";";
				sql::ResultSet* result = dbManager.executeQuery(query);

				if (result->next()) {
					// Ottieni i valori delle colonne e gestiscili come desiderato
					int idCorso = result->getInt("id_corso");
					string giorno = result->getString("giorno");
					string oraInizio = result->getString("ora_inizio");
					string oraFine = result->getString("ora_fine");
					max = result->getInt("max_partecipanti");

					if (giorno.find(str) == std::string::npos) {
						delete result;
						res.body = "Per la data inserita non sono previste lezioni";
						res.code = 409;
						res.end();
						return;
					}

					// Controllo se l'utente è prenotato già in questa data a questo corso
					string query3 = "SELECT * FROM prenotazioni P JOIN lezioni L ON L.id = P.id_lezione WHERE P.id_user = " 
						+ to_string(idUser) + " && P.data = '" + data + "' && L.id_corso = " + to_string(idCorso) + ";";
					sql::ResultSet* result3 = dbManager.executeQuery(query3);

					if (result3->next()) {
						delete result3;
						res.body = "L'utente ha gia' una prenotazione per questo corso in tale data.";
						res.code = 409;
						res.end();
						delete result;
						return;
					}
					delete result3;

					// Controllo se l'utente è prenotato in questa data per altri corsi 
					// durante la stessa fascia oraria
					string query4 = "SELECT * FROM prenotazioni P JOIN lezioni L ON L.id = P.id_lezione WHERE P.id_user = "
						+ to_string(idUser) + " && P.data = '" + data + "' && L.id_corso != "
						+ to_string(idCorso) + " && (('" + oraInizio + "' >= L.ora_inizio AND '"
						+ oraInizio + "' < L.ora_fine) OR ('" + oraFine + "' > L.ora_inizio AND '"
						+ oraFine + "' <= L.ora_fine) OR ('" + oraInizio + "' <= L.ora_inizio AND '"
						+ oraFine + "' >= L.ora_fine));";
					sql::ResultSet* result4 = dbManager.executeQuery(query4);

					if (result4->next()) {
						delete result4;
						res.body = "L'utente e' gia' prenotato per un'altra lezione con orario sovrapposto.";
						res.code = 409;
						res.end();
						delete result;
						return;
					}
					delete result4;

					delete result;
				}
				else {
					res.body = "Lezione non trovata";
					res.code = 404;
					res.end();
					return;
				}
			}
			else {
				res.body = "L'utente non puo' essere prenotato in date antecedenti a quella odierna.";
				res.code = 409;
				res.end();
				return;
			}


			// vedo se il numero massimo di partecipanti per tale data sta per essere superato 

			string query1 = "SELECT COUNT(*) from prenotazioni where id_lezione="
				+ to_string(idLezione) + " && data ='" + data + "';";
			sql::ResultSet* result1 = dbManager.executeQuery(query1);
			if (result1->next()) {
				int rowCount = result1->getInt(1);
				if (rowCount > max) {
					delete result1;
					res.body = "Posti esauriti per tale lezione.";
					res.code = 409;
					res.end();
					return;
				}
				delete result1;
			}

			string query5 = "SELECT data_inizio_corso, data_fine_corso FROM corsi WHERE id=" + to_string(idCorso);
			sql::ResultSet* result5 = dbManager.executeQuery(query5);

			if (result5->next()) {
				time_t dataInizioCorso = TimeManager::dateStringToTime(result5->getString("data_inizio_corso"));
				time_t dataFineCorso = TimeManager::dateStringToTime(result5->getString("data_fine_corso"));

				if (data_t < dataInizioCorso || data_t > dataFineCorso) {
					delete result5;
					res.body = "Il corso ha raggiunto la data di scadenza";
					res.code = 409;
					res.end();
					return;
				}
				delete result5;
			}

			string query2 = "insert into prenotazioni (id_lezione,id_user, data) values ("
				+ to_string(idLezione) + "," + to_string(idUser) + ",'" + data + "');";
			dbManager.executeUpdate(query2, res);
		}
		else {
			res.body = "L'utente non possiede un abbonamento valido per questa data.";
			res.code = 409;
			res.end();
			return;
		}

	}
	else {
		res.body = "L'utente non e' idoneo per la prenotazione in quanto non dispone di un abbonamento attivo.";
		res.code = 409;
		res.end();
		return;
	}
}
void Prenotazione::updatePrenotazione(crow::response& res, DatabaseManager& dbManager)
{
	// controllo che giorno e' la data 
	time_t now = time(NULL);
	now = TimeManager::getMidnightCurrentDate(now);
	time_t data_t = TimeManager::dateStringToTime(data);
	char str_time[26];
	ctime_s(str_time, sizeof str_time, &data_t);
	string str(str_time, 3);  //(ex 13/09/23 ->Wed)
	int max = 0;

	//cerco l'abbonamento attivo 
	string query4 = "SELECT * FROM abbonamenti where id_utente =" + to_string(idUser) + ";";
	sql::ResultSet* result4 = dbManager.executeQuery(query4);

	bool abbonamentoAttivoTrovato = false;
	time_t scadenzaAbbAttivo;

	if (result4->next()) {
		do {

			time_t start = TimeManager::dateStringToTime(result4->getString("data_inizio"));
			time_t scadenza = TimeManager::calcolaScadenza(start, result4->getString("durata_abbonamento"));

			if (start <= now && now <= scadenza) {
				abbonamentoAttivoTrovato = true;
				scadenzaAbbAttivo = scadenza;
			}
		} while (result4->next());

		delete result4;
	}

	if (abbonamentoAttivoTrovato) {

		int idCorso = -1;
		if (data_t <= scadenzaAbbAttivo) {

			if (data_t >= now) {

				string query = "SELECT * FROM lezioni where id =" + to_string(idLezione) + ";";
				sql::ResultSet* result = dbManager.executeQuery(query);

				if (result->next()) {
					// Ottieni i valori delle colonne e gestiscili come desiderato
					int id = result->getInt("id");
					int idCorso = result->getInt("id_corso");
					string giorno = result->getString("giorno");
					string oraInizio = result->getString("ora_inizio");
					string oraFine = result->getString("ora_fine");
					max = result->getInt("max_partecipanti");

					if (giorno.find(str) == std::string::npos) {
						delete result;
						res.body = "Per la data inserita non sono previste lezioni";
						res.code = 409;
						res.end();
						return;
					}

					// Controllo se l'utente è prenotato già in questa data a questo corso, escludendo
					// la lezione che stiamo modificando
					string query3 = "SELECT * FROM prenotazioni P JOIN lezioni L ON L.id = P.id_lezione WHERE P.id_user = " + to_string(idUser) + " && P.data = '" + data + "' && L.id_corso = " + to_string(idCorso) + " && P.id_lezione = " + to_string(idLezione) + ";";
					sql::ResultSet* result3 = dbManager.executeQuery(query3);

					if (result3->next()) {
						delete result3;
						res.body = "L'utente ha gia' una prenotazione per questo corso in tale data e fascia oraria.";
						res.code = 200;
						res.end();
						delete result;
						return;
					}
					delete result3;

					// Controllo se l'utente è prenotato in questa data per altri corsi 
					// durante la stessa fascia oraria, escludendo la lezione che stiamo modificando
					string query4 = "SELECT * FROM prenotazioni P JOIN lezioni L ON L.id = P.id_lezione WHERE P.id_user = "
						+ to_string(idUser) + " && P.data = '" + data + "' && L.id_corso != "
						+ to_string(idCorso) + " && P.id_lezione != " + to_string(idLezione)
						+ " && (('" + oraInizio + "' >= L.ora_inizio AND '"
						+ oraInizio + "' < L.ora_fine) OR ('" + oraFine + "' > L.ora_inizio AND '"
						+ oraFine + "' <= L.ora_fine) OR ('" + oraInizio + "' <= L.ora_inizio AND '"
						+ oraFine + "' >= L.ora_fine));";
					sql::ResultSet* result4 = dbManager.executeQuery(query4);

					if (result4->next()) {
						delete result4;
						res.body = "L'utente e' gia' prenotato per un'altra lezione con orario sovrapposto.";
						res.code = 409;
						res.end();
						delete result;
						return;
					}
					delete result4;

					delete result;
				}
				else {
					res.body = "Lezione non trovata";
					res.code = 404;
					res.end();
					return;
				}
			}
			else {
				res.body = "L'utente non puo' essere prenotato in date antecedenti a quella odierna.";
				res.code = 409;
				res.end();
				return;
			}


			// vedo se il numero massimo di partecipanti per tale data sta per essere superato
			// escludendo la pranotazione che stiamo modificando

			string query1 = "SELECT COUNT(*) from prenotazioni where id_lezione="
				+ to_string(idLezione) + " && data ='" + data + "' && id != " + to_string(id) + ";";
			sql::ResultSet* result1 = dbManager.executeQuery(query1);
			if (result1->next()) {
				int rowCount = result1->getInt(1);
				if (rowCount >= max) {
					delete result1;
					res.body = "Posti esauriti per tale lezione.";
					res.code = 409;
					res.end();
					return;
				}
				delete result1;
			}

			string query5 = "SELECT data_inizio_corso, data_fine_corso FROM corsi WHERE id=" + to_string(idCorso);
			sql::ResultSet* result5 = dbManager.executeQuery(query5);

			if (result5->next()) {
				time_t dataInizioCorso = TimeManager::dateStringToTime(result5->getString("data_inizio_corso"));
				time_t dataFineCorso = TimeManager::dateStringToTime(result5->getString("data_fine_corso"));

				if (data_t < dataInizioCorso || data_t > dataFineCorso) {
					delete result5;
					res.body = "Il corso ha raggiunto la data di scadenza";
					res.code = 409;
					res.end();
					return;
				}
				delete result5;
			}

			string query2 = "UPDATE prenotazioni SET id_lezione = " + to_string(idLezione)
				+ ", id_user = " + to_string(idUser) + ", data = '" + data + "' WHERE id = "
				+ to_string(id) + ";";
			dbManager.executeUpdate(query2, res);
		}
		else {
			res.body = "L'utente non possiede un abbonamento valido per questa data.";
			res.code = 409;
			res.end();
			return;
		}

	}
	else {
		res.body = "L'utente non e' idoneo per la prenotazione in quanto non dispone di un abbonamento attivo.";
		res.code = 409;
		res.end();
		return;
	}
}
void Prenotazione::getPrenotazioni(crow::response& res, DatabaseManager& dbManager)
{
	string query = "SELECT * FROM prenotazioni;";
	sql::ResultSet* result = dbManager.executeQuery(query);

	if (result->next()) {

		nlohmann::json jsonArray;

		do {
			// Ottieni i valori delle colonne e gestiscili come desiderato
			nlohmann::json jsonData;
			jsonData["id"] = result->getInt("id");
			jsonData["id_lezione"] = result->getInt("id_lezione");
			jsonData["id_user"] = result->getInt("id_user");
			jsonData["data"] = result->getString("data");

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
void Prenotazione::getPrenotazioniUser(crow::response& res, DatabaseManager& dbManager)
{
	string query = "SELECT * FROM prenotazioni where id_user = " + to_string(idUser) + ";";
	sql::ResultSet* result = dbManager.executeQuery(query);

	if (result->next()) {

		nlohmann::json jsonArray;

		do {
			// Ottieni i valori delle colonne e gestiscili come desiderato
			nlohmann::json jsonData;
			jsonData["id"] = result->getInt("id");
			jsonData["id_lezione"] = result->getInt("id_lezione");
			jsonData["id_user"] = result->getInt("id_user");
			jsonData["data"] = result->getString("data");

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
void Prenotazione::deletePrenotazione(crow::response& res, DatabaseManager& dbManager)
{
	string query = "DELETE FROM prenotazioni WHERE id = '" + to_string(id) + "';";
	dbManager.executeUpdate(query, res);
}
