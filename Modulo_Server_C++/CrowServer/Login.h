#pragma once
#include <string>
#include <crow.h>
#include <jwt-cpp/jwt.h>
#include <nlohmann/json.hpp>
#include "DatabaseManager.h"

using namespace std;
class Login
{
	string mail;
	string password;
public : 
	Login(string Mail, string Password);
	void effettuaLogin(crow::response& res, DatabaseManager& dbManager);
};

