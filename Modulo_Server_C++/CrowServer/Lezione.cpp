#include "Lezione.h"

Lezione::Lezione()
{
}

Lezione::Lezione(int Id)
{
	id = Id; 
}

Lezione::Lezione(int IdCorso, string Giorno, string OraInizio, string OraFine, int MaxPartecipanti)
{
	idCorso = IdCorso;
	giorno = Giorno;
	oraInizio = OraInizio;
	oraFine = OraFine;
	maxPartecipanti = MaxPartecipanti;
}

Lezione::Lezione(int Id, int IdCorso, string Giorno, string OraInizio, string OraFine, int MaxPartecipanti)
{
	id = Id;
	idCorso = IdCorso;
	giorno = Giorno;
	oraInizio = OraInizio;
	oraFine = OraFine;
	maxPartecipanti = MaxPartecipanti;
}



void Lezione::setIdCorso(int IdCorso)
{
	idCorso = IdCorso;
}

void Lezione::addLezione(crow::response& res, DatabaseManager& dbManager)
{
	time_t addedOraInizio = TimeManager::timeStringToTime(oraInizio);
	time_t addedOraFine = TimeManager::timeStringToTime(oraFine);

	string queryAbbonamenti = "SELECT * FROM lezioni where giorno = '" + giorno + "' AND id_corso = " + to_string(idCorso);

	sql::ResultSet* result = dbManager.executeQuery(queryAbbonamenti);

	if (result->next()) {
		nlohmann::json jsonArray;
		do {
			nlohmann::json jsonData;
			string ora_inizio = result->getString("ora_inizio");
			string ora_fine = result->getString("ora_fine");
			int corsoIdLezioneCorrente = result->getInt("id_corso");

			// calcolo la scadenza dell'abbonamento trovato per l'utente e verifico se è scaduto o meno 
			time_t timeInizio = TimeManager::timeStringToTime(ora_inizio);
			time_t timeFine = TimeManager::timeStringToTime(ora_fine);

			// Controlla se la fascia oraria inserita è compresa tra l'ora di inizio e l'ora di fine
			bool match = addedOraInizio > timeInizio && addedOraInizio < timeFine;
			bool match2 = addedOraFine > timeInizio && addedOraFine < timeFine;
			bool match3 = addedOraInizio <= timeInizio && addedOraFine >= timeFine
				&& corsoIdLezioneCorrente == idCorso;

			if (match || match2)
			{
				// Sovrapposizione trovata
				res.body = "Non e' possibile creare una nuova lezione in quanto esiste gia' un'altra lezione per lo stesso giorno della settimana, nella stessa fascia oraria.";
				res.code = 409;
				res.end();
				return;
			}

			if (match3)
			{
				// Sovrapposizione trovata
				res.body = "Non e' possibile creare una nuova lezione per lo stesso corso in questa fascia oraria durante lo stesso giorno della settimana.";
				res.code = 409;
				res.end();
				return;
			}

		} while (result->next());
		delete result;
	}

	string query = "INSERT INTO lezioni(id_corso, giorno, ora_inizio, ora_fine, max_partecipanti) VALUES("
		+ to_string(idCorso) + ", '" + giorno + "', '" + oraInizio + "','" + oraFine + "'," + to_string(maxPartecipanti) + ");";

	dbManager.executeUpdate(query, res);
}

void Lezione::updateLezione(crow::response& res, DatabaseManager& dbManager)
{
	// Ottengo giorno della settimana, ora di inizio e ora di fine attuali della lezione che si sta cercando di aggiornare
	string queryLezioneAttuale = "SELECT * FROM lezioni WHERE id = " + to_string(id);
	sql::ResultSet* resultLezioneAttuale = dbManager.executeQuery(queryLezioneAttuale);

	if (resultLezioneAttuale->next()) {
		string giornoLezioneAttuale = resultLezioneAttuale->getString("giorno");
		string oraInizioAttuale = resultLezioneAttuale->getString("ora_inizio");
		string oraFineAttuale = resultLezioneAttuale->getString("ora_fine");
		int idCorso = resultLezioneAttuale->getInt("id_corso");

		// Ottengo le lezioni esistenti per lo stesso giorno, escludendo la lezione attuale
		string queryAltreLezioni = "SELECT * FROM lezioni WHERE giorno = '" + giornoLezioneAttuale
			+ "' AND id != " + to_string(id) + " AND id_corso = " + to_string(idCorso);
		sql::ResultSet* resultAltreLezioni = dbManager.executeQuery(queryAltreLezioni);

		time_t addedOraInizio = TimeManager::timeStringToTime(oraInizio);
		time_t addedOraFine = TimeManager::timeStringToTime(oraFine);

		// Controlla se le nuove ore di inizio e fine si sovrappongono con altre lezioni per lo stesso giorno della settimana
		while (resultAltreLezioni->next()) {
			string oraInizioLezioneEsistente = resultAltreLezioni->getString("ora_inizio");
			string oraFineLezioneEsistente = resultAltreLezioni->getString("ora_fine");
			int corsoIdLezioneCorrente = resultAltreLezioni->getInt("id_corso");

			time_t timeInizioEsistente = TimeManager::timeStringToTime(oraInizioLezioneEsistente);
			time_t timeFineEsistente = TimeManager::timeStringToTime(oraFineLezioneEsistente);
			bool match = addedOraInizio > timeInizioEsistente && addedOraInizio < timeFineEsistente;
			bool match2 = addedOraFine > timeInizioEsistente && addedOraFine < timeFineEsistente;
			bool match3 = addedOraInizio <= timeInizioEsistente && addedOraFine >= timeFineEsistente
				&& corsoIdLezioneCorrente == idCorso;

			if (match || match2) {
				// Sovrapposizione trovata
				res.body = "Le nuove ore di inizio e fine si sovrappongono con altre lezioni per lo stesso giorno.";
				res.code = 409;
				res.end();
				return;
			}

			if (match3)
			{
				// Sovrapposizione trovata
				res.body = "Non e' possibile creare una nuova lezione per lo stesso corso in questa fascia oraria durante lo stesso giorno della settimana.";
				res.code = 409;
				res.end();
				return;
			}

		}
		delete resultAltreLezioni;

		// Se non ci sono sovrapposizioni, procedi con l'aggiornamento
		string queryUpdate = "UPDATE lezioni SET ora_inizio = '" + oraInizio + "', ora_fine = '" + oraFine + "', max_partecipanti = " + to_string(maxPartecipanti)
			+ " WHERE id = " + to_string(id);
		dbManager.executeUpdate(queryUpdate, res);
	}

	delete resultLezioneAttuale;
}

void Lezione::getLezioni(crow::response& res, DatabaseManager& dbManager)
{
	string query = "SELECT * FROM lezioni where id_corso =" + to_string(idCorso) + ";";
	sql::ResultSet* result = dbManager.executeQuery(query);

	if (result->next()) {

		nlohmann::json jsonArray;

		do {
			// Ottieni i valori delle colonne e gestiscili come desiderato
			nlohmann::json jsonData;

			jsonData["id"] = result->getInt("id");
			jsonData["id_corso"] = result->getInt("id_corso");
			jsonData["giorno"] = result->getString("giorno");
			jsonData["ora_inizio"] = result->getString("ora_inizio");
			jsonData["ora_fine"] = result->getString("ora_fine");
			jsonData["max_partecipanti"] = result->getInt("max_partecipanti");

			jsonArray.push_back(jsonData);

		} while (result->next());

		res.body = jsonArray.dump();
		res.code = 200;
		res.end();
		delete result;
		return;
	}
	else {
		res.body = "Nessuna lezione trovata per il corso specificato.";
		res.code = 404;
		res.end();
		return;
	}
}

void Lezione::getLezione(crow::response& res, DatabaseManager& dbManager)
{
	string query = "SELECT * FROM lezioni where id =" + to_string(id) + ";";
	sql::ResultSet* result = dbManager.executeQuery(query);

	if (result->next()) {

		// Ottieni i valori delle colonne e gestiscili come desiderato
		nlohmann::json jsonData;

		jsonData["id"] = result->getInt("id");
		jsonData["id_corso"] = result->getInt("id_corso");
		jsonData["giorno"] = result->getString("giorno");
		jsonData["ora_inizio"] = result->getString("ora_inizio");
		jsonData["ora_fine"] = result->getString("ora_fine");
		jsonData["max_partecipanti"] = result->getInt("max_partecipanti");

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

void Lezione::deleteLezione(crow::response& res, DatabaseManager& dbManager)
{
	string query = "DELETE FROM lezioni WHERE id = '" + to_string(id) + "';";
	dbManager.executeUpdate(query, res);
}
