#include "stdafx.h"
using namespace std;
//Функция вывода ошибки "НЕ КОРРЕКТНЫЙ ВВОД!!!" и завершение програмы 
//code - код ошибки
void errorMes(int code);

int main(){
    setlocale(LC_ALL, "RUS");

    char date[9];
    cout << "Введите дату формата ДДММГГГГ: ";
    cin >> date;
    const int day_of_B = 59;
    const int year_of_B = 2006;
    

    if (date[8] != '\0') {
        errorMes(1);
    }

    for (int i = 0; i < 8; i++) {
        if (!(date[i] >= '0' && date[i] <= '9')) {
            errorMes(1);
        }
        date[i] -= 48;
    }

    int day = (date[0] * 10) + date[1],
        month = (date[2] * 10) + date[3],
        year = ((date[4] * 10 + date[5]) * 10 + date[6]) * 10 + date[7];

    int days_in_month[12] =
    { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

    bool definition = LeapYearDefinition(year);

    if (definition) {
        days_in_month[1]++;
    }

    if (!(day >= 1 && day <= days_in_month[month - 1]) || !(month >= 1 && month <= 12) || !(year >= 0)) {
        errorMes(1);
    }

    int number_of_day = SequentialNumberCounting(day, month, definition);

    int days_to_B;

    
    if (number_of_day <= day_of_B) {
        days_to_B = day_of_B - number_of_day;
    }
    else if (number_of_day > day_of_B) {
        days_to_B = ((day < 29 && month < 2 && definition ? 366 : 365) - number_of_day) + day_of_B;
    }
    if (year < year_of_B) {
        int buffer = 0;
        int difference = year_of_B - year;
        for (int i = (number_of_day <= day_of_B ? 0 : 1); i < difference; i++) {
            buffer += (LeapYearDefinition(year + i) ? 366 : 365);
        }
        days_to_B += buffer;
    }

    cout << "ВИСОКОСТНОСТЬ: " << (definition ? "ДА" : "НЕТ") << endl;
    cout << "ДЕНЬ В ГОДУ: " << number_of_day << endl;
    cout << "ДО ДНЯ РОЖДЕНИЯ: " << days_to_B << endl;

   
    return 0;
}


void errorMes(int code) {
    cout << "НЕ КОРРЕКТНЫЙ ВВОД!!!";
    exit(code);
}