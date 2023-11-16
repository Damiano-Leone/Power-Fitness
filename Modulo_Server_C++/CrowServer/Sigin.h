#pragma once
#include <string>
#include <crow.h>
#include "DatabaseManager.h"

using namespace std; 
class Sigin
{
    string mail;
    string password;
    string nome;
    string cognome;
    string ruolo;
public:
    Sigin(string Mail, string Password, string Nome, string Cognome, string Ruolo);
    void effettuaSigin(crow::response& res, DatabaseManager& dbManager);
};

