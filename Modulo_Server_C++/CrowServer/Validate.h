#pragma once
#include <crow.h>
#include <string>
#include "DatabaseManager.h"
#include <nlohmann/json.hpp>
#include <jwt-cpp/jwt.h>

using namespace std; 
class Validate
{
	int id;
public:
	Validate(int Id);
	void validateDip(crow::response& res, DatabaseManager& dbManager, string token);
};

