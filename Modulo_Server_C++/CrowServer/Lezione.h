#pragma once
#include <string>
#include <crow.h>
#include <nlohmann/json.hpp>
#include "DatabaseManager.h"
#include "TimeManager.h"

using namespace std;

class Lezione
{
	int id;
	int idCorso;
	int maxPartecipanti;
	string giorno;
	string oraInizio;
	string oraFine;

public:
	Lezione();
	Lezione(int Id);
	Lezione(int IdCorso, string Giorno, string OraInizio, string OraFine, int MaxPartecipanti);
	Lezione(int Id, int IdCorso, string Giorno, string OraInizio, string OraFine, int MaxPartecipanti);

	void setIdCorso(int IdCorso);
	void addLezione(crow::response& res, DatabaseManager& dbManager);
	void updateLezione(crow::response& res, DatabaseManager& dbManager);
	void getLezioni(crow::response& res, DatabaseManager& dbManager);
	void getLezione(crow::response& res, DatabaseManager& dbManager);
	void deleteLezione(crow::response& res, DatabaseManager& dbManager);
};
