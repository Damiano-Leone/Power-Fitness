#pragma once
#include <string>
#include <crow.h> 
#include <nlohmann/json.hpp>
#include "DatabaseManager.h"

using namespace std;

class User
{
	int id;
	string codiceFiscale;
	string nome;
	string cognome;
	string genere;
	string dataNascita;
	string cellulare;
	string email;
	string indirizzo;
	string numeroCivico;

public:
	User(int Id);
	User(int Id, string Cellulare, string Email, string Indirizzo, string NumeroCivico);
	User(string Cf, string Cellulare, string Email, string Indirizzo, string NumeroCivico);
	User(string Cf, string Nome, string Cognome, string Genere, string DataNascita,
		string Cellulare, string Email, string Indirizzo, string NumeroCivico);

	void addUser(crow::response& res, DatabaseManager& dbManager);
	void updateUser(crow::response& res, DatabaseManager& dbManager);
	void deleteUser(crow::response& res, DatabaseManager& dbManager);
	static void getUsers(crow::response& res, DatabaseManager& dbManager);
};
