#pragma once
#include <string>
#include <crow.h>
#include <nlohmann/json.hpp>
#include "DatabaseManager.h"

using namespace std;
class Dipendente
{
public:
	Dipendente();
	static void getDipendenti(crow::response& res, DatabaseManager& dbManager);
};

