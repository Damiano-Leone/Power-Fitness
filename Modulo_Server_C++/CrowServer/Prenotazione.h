#pragma once
#include <string>
#include <crow.h>
#include <nlohmann/json.hpp>
#include "DatabaseManager.h"
#include "TimeManager.h"

using namespace std;
class Prenotazione
{
	int id;
	int idLezione;
	int idUser;
	string data;
public:
	Prenotazione();
	Prenotazione(int IdPrenotazione);
	Prenotazione(int IdLezione, int IdUser, string Data);
	Prenotazione(int Id, int IdLezione, int IdUser, string Data);

	void setIdUser(int IdUser);
	void addPrenotazione(crow::response& res, DatabaseManager& dbManager);
	void updatePrenotazione(crow::response& res, DatabaseManager& dbManager);
	static void getPrenotazioni(crow::response& res, DatabaseManager& dbManager);
	void getPrenotazioniUser(crow::response& res, DatabaseManager& dbManager);
	void deletePrenotazione(crow::response& res, DatabaseManager& dbManager);
};


