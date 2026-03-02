#include "stdafx.h"
//#include "sequential_number_counting.h"

int SequentialNumberCounting(int day, int month, bool definition) {
	int days_in_month[12] =
	{31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
	if (definition) {
		days_in_month[1]++;
	}
	int number_of_day = 0;
	for (int i = 1; i < month; i++) {
		number_of_day += days_in_month[i - 1];
	}
	number_of_day += day;
	return number_of_day;
}