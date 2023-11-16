#pragma once
#include <string>
#include <crow.h> 
#include <nlohmann/json.hpp>
#include "DatabaseManager.h"

using namespace std;

class Corso {
    int id;
    string nome;
    string dataInizioCorso;
    string dataFineCorso;
    string descrizione;
public:
    Corso(int Id); 
    Corso(string nomeCorso, string DataInizioCorso, string DataFineCorso, string Descrizione);
    Corso(int Id, string nomeCorso, string DataInizioCorso, string DataFineCorso, string Descrizione);

    void addCorso(crow::response& res, DatabaseManager& dbManager);
    void getCorso(crow::response& res, DatabaseManager& dbManager);
    void updateCorso(crow::response& res, DatabaseManager& dbManager);
    void deleteCorso(crow::response& res, DatabaseManager& dbManager);
    static void getCorsi(crow::response& res, DatabaseManager& dbManager);
};

