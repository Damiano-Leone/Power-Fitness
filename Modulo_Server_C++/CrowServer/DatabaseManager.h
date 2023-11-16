#pragma once
#include <string>
#include <crow.h> 
#include "mysql/jdbc.h"

//Ricorda di installare la libreria mysql Connector e specificare le directory di inclusione di mysql in vs
//Qualora lo si volesse fare da riga di comando : 
//g++ -I"C:\path\to\mysql-connector-c++\include" -L"C:\path\to\mysql-connector-c++\include\mysql" -lmysqlcppconn file.cpp -o output

class DatabaseManager
{
private:
    std::string host;
    std::string username;
    std::string password;
    std::string db;
    //std::unique_ptr<sql::Connection> con; // permette di definire un puntatore intelligente che viene deallocato automaticamente 
                                          //quando non serve più o quando viene chiamata la funzione reset.
    sql::Connection* con;
public:
    DatabaseManager(const std::string& host, const std::string& username,
        const std::string& password, const std::string& database);
    ~DatabaseManager();
    bool connect();
    sql::ResultSet* executeQuery(const std::string& query);
    void executeUpdate(const std::string& query, crow::response& res);
};

