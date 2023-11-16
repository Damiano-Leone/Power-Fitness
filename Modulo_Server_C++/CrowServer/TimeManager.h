#pragma once
#include <string>
#include <crow.h> 
#include <vector>

using namespace std; 

class TimeManager
{
public:
	static time_t calcolaScadenza(time_t start, string tipo);
	static time_t dateStringToTime(const string& stringDate);
	static time_t timeStringToTime(const string& stringTime);
	static time_t getMidnightCurrentDate(time_t day);
};

