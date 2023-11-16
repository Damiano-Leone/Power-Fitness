#pragma once
#include <string>
#include <crow.h> 
#include <vector>
#include <nlohmann/json.hpp>
#include "DatabaseManager.h"
#include "TimeManager.h"

using namespace std;
class Abbonamento
{
	int id; 
	int idUtente; 
	string dataIscrizione; 
	string durataAbbonamento; 

public:
	Abbonamento();
	Abbonamento(int Id);
	Abbonamento(int IdUtente, string DurataAbbonamento);
	Abbonamento(int IdUtente, string DataIscrizione, string DurataAbbonamento);
	Abbonamento(int Id, int IdUtente, string DataIscrizione, string DurataAbbonamento);

	void setIdUtente(int IdUtente);
	void addAbbonamento(crow::response& res, DatabaseManager& dbManager);
	void rinnovaAbbonamento(crow::response& res, DatabaseManager& dbManager);
    void updateAbbonamento(crow::response& res, DatabaseManager& dbManager);
	void deleteAbbonamento(crow::response& res, DatabaseManager& dbManager);
	void getAbbonamentiUser(crow::response& res, DatabaseManager& dbManager);
	void getAbbonamentoAttivoUser(crow::response& res, DatabaseManager& dbManager);
};

