#include "DatabaseManager.h"

DatabaseManager::DatabaseManager(const std::string& host, const std::string& username,
    const std::string& password, const std::string& database) 
    : host(host), username(username), password(password), db(database)
{
}

DatabaseManager::~DatabaseManager()
{
    delete con;
}

bool DatabaseManager::connect() {
    try {
        sql::mysql::MySQL_Driver* driver;
        driver = sql::mysql::get_mysql_driver_instance(); // ottengo un puntatore al driver MySQL e con driver->connect mi connetto al db
        con=driver->connect(host, username, password); // mentre con.reset() dealloca  eventualmente in maniera automatica e riassegna la nuova con
        con->setSchema(db);
        return true;
    }
    catch (const sql::SQLException& e) {
        std::cerr << "Errore di connessione al database: " << e.what() << std::endl;
        return false;
    }
}

sql::ResultSet* DatabaseManager::executeQuery(const std::string& query) {
    sql::Statement* stmt= con->createStatement();
    sql::ResultSet* res = stmt->executeQuery(query);
    delete stmt;
    return res;
}

void DatabaseManager::executeUpdate(const std::string& query, crow::response& res) {
    try {
        sql::Statement* stmt = con->createStatement();
        int rs = stmt->executeUpdate(query);
        delete stmt;

        if (rs > 0) {
            res.body = "Operazione completata correttamente";
        }
        else {
            res.body = "Nessuna riga modificata nel database";
        }
    }
    catch (const sql::SQLException& e) {
        std::cerr << "Errore nell'esecuzione della query: " << e.what() << std::endl;
        res.body = "Errore durante l'esecuzione della query: " + std::string(e.what());
        res.code = 500;
        res.end();
    }
    res.code = 200;
    res.end();
}

