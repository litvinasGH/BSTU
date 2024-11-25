#include "stdafx.h"
using namespace std;

DATE inputDate() {
    char date[9];
    cin >> date;
    //const int day_of_B = 59;
    //const int year_of_B = 2006;


    if (date[8] != '\0') {
        errorMes(1);
    }

    for (int i = 0; i < 8; i++) {
        if (!(date[i] >= '0' && date[i] <= '9')) {
            errorMes(1);
        }
        date[i] -= 48;
    }

    DATE idate;
    idate.day = (date[0] * 10) + date[1];
    idate.month = (date[2] * 10) + date[3];
    idate.year = ((date[4] * 10 + date[5]) * 10 + date[6]) * 10 + date[7];

    int days_in_month[12] =
    { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

    bool definition = LeapYearDefinition(idate.year);

    if (definition) {
        days_in_month[1]++;
    }

    if (!(idate.day >= 1 && idate.day <= days_in_month[idate.month - 1]) ||
        !(idate.month >= 1 && idate.month <= 12) || !(idate.year >= 0)) {
        errorMes(1);
    }

    return idate;
}
