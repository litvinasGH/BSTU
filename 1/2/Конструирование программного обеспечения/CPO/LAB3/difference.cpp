#include "stdafx.h"
using namespace std;

int OutputDiff(DATE mainDate, DATE secondDate) {

    bool definition = LeapYearDefinition(mainDate.year);
    bool definitionSec = LeapYearDefinition(secondDate.year);

    int number_of_day = SequentialNumberCounting(mainDate.day, mainDate.month, definition);
    int day_of_B = SequentialNumberCounting(secondDate.day, secondDate.month, definitionSec);

    int days_to_B;


    if (number_of_day <= day_of_B) {
        days_to_B = day_of_B - number_of_day;
    }
    else if (number_of_day > day_of_B) {
        days_to_B = ((mainDate.day < 29 && mainDate.month < 2 && definition ? 366 : 365) - number_of_day) + day_of_B;
    }
    if (mainDate.year < secondDate.year) {
        int buffer = 0;
        int difference = secondDate.year - mainDate.year;
        for (int i = (number_of_day <= day_of_B ? 0 : 1); i < difference; i++) {
            buffer += (LeapYearDefinition(mainDate.year + i) ? 366 : 365);
        }
        days_to_B += buffer;
    }
    else if (mainDate.year > secondDate.year) {
        int buffer = 0;
        int difference = mainDate.year - secondDate.year;
        for (int i = (number_of_day <= day_of_B ? 0 : 1); i < difference; i++) {
            buffer += (LeapYearDefinition(secondDate.year + i) ? 366 : 365);
        }
        days_to_B += buffer;
    }

    return days_to_B;
}


int OutputDiffH(int difference) {
    time_t now = time(0);
    tm ltm; tm* ltmp = &ltm;
    localtime_s(ltmp, &now);
    return (difference * 24) +  ltmp->tm_hour;
}