#include "TimeManager.h"


time_t TimeManager::calcolaScadenza(time_t start, string tipo)
{
	struct tm date;
	localtime_s(&date, &start); // Converti il timestamp in una struttura tm

	if (tipo == "1 Mese") {
		// Aggiungi un mese alla data corrente
		date.tm_mon += 1;
	}
	else if (tipo == "2 Mesi") {
		// Aggiungi due mesi alla data corrente
		date.tm_mon += 2;
	}
	else if (tipo == "3 Mesi") {
		// Aggiungi 3 mesi alla data corrente
		date.tm_mon += 3;
	}
	else if (tipo == "6 Mesi") {
		// Aggiungi 6 mesi alla data corrente
		date.tm_mon += 6;
	}
	else {
		// Aggiungi un anno alla data corrente
		date.tm_year += 1;
	}

	// Normalizza la data (gestisce il superamento dei limiti dei mesi) e
	// Converte la data aggiornata in un timestamp
	time_t expirationDate = mktime(&date);
	return expirationDate;
}

time_t TimeManager::dateStringToTime(const string& stringDate)
{
	tm date = {};
	string delimiter = "-";
	vector<string> sub_str;
	size_t start;
	size_t end = 0;

	// Fino a quando non trovo il carattere string::npos che indica la fine della ricerca
	// Memorizzo in start il pezzo della data usando come delimitatore il -
	while ((start = stringDate.find_first_not_of(delimiter, end)) != string::npos)
	{
		end = stringDate.find(delimiter, start);
		// Estraggo la sottostringa
		sub_str.push_back(stringDate.substr(start, end - start));
	}

	date.tm_mday = stoi(sub_str.at(2));
	date.tm_mon = stoi(sub_str.at(1)) - 1;
	date.tm_year = stoi(sub_str.at(0)) - 1900;

	time_t time1 = mktime(&date);
	return time1;
}

time_t TimeManager::timeStringToTime(const std::string& stringTime)
{
	tm timeStruct = {};
	std::string delimiter = ":";
	vector<string> sub_str;
	size_t start = 0;
	size_t end = 0;

	// Fino a quando non trovo il carattere string::npos che indica la fine della ricerca
	// Memorizzo in start il pezzo della data usando come delimitatore il :
	while ((start = stringTime.find_first_not_of(delimiter, end)) != string::npos)
	{
		end = stringTime.find(delimiter, start);
		// Estraggo la sottostringa
		sub_str.push_back(stringTime.substr(start, end - start));
	}

	timeStruct.tm_min = stoi(sub_str.at(1));
	timeStruct.tm_hour = stoi(sub_str.at(0));

	// Imposta gli altri campi a valori predefiniti
	timeStruct.tm_sec = 0;
	timeStruct.tm_mday = 1;
	timeStruct.tm_mon = 0;
	timeStruct.tm_year = 70;

	time_t time1 = mktime(&timeStruct);
	return time1;
}

time_t TimeManager::getMidnightCurrentDate(time_t day)
{
	// Ottenere la data corrente impostando l'orario a mezzanotte
	struct tm midnight;
	gmtime_s(&midnight, &day);
	midnight.tm_hour = 0;
	midnight.tm_min = 0;
	midnight.tm_sec = 0;

	time_t midnightTime = mktime(&midnight);
	return midnightTime;
}
