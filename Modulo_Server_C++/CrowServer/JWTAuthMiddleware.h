#pragma once
#include <string>
#include <crow.h>
#include <jwt-cpp/jwt.h>
#include <nlohmann/json.hpp>
#include "DatabaseManager.h"

using namespace std;
struct JWTAuthMiddleware {
	struct context
	{};
	DatabaseManager* db;
	void setDb(DatabaseManager* pDb);
	void before_handle(crow::request& req, crow::response& res, context& ctx);
	void after_handle(crow::request& req, crow::response& res, context& ctx);
	bool verifyToken(string token);
};