#include "stdafx.h"
//#include "leap_year_definition.h"

bool LeapYearDefinition(int year){
	return ((year % 4 == 0) && (year % 100 != 0)) || ((year % 100 == 0) && (year % 400 == 0));
}