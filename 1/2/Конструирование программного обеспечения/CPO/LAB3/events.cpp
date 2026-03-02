#include "stdafx.h"

std::string OutputNameOfTheEvent(int number, bool definition) {
	struct Events
	{
		std::string name;
		int date;
	};
	Events arrayOfEvents[] =
	{
		{"День Програмиста", 256},
		{"Канун НГ", 365},
		{"НГ", 1},
		{"Первый день лета", 152}
	};
	if (definition) {
		arrayOfEvents[1].date++;
		arrayOfEvents[3].date++;
	}
	for (int i = 0; i < 4; i++)
		if (number == arrayOfEvents[i].date)
			return arrayOfEvents[i].name;
	return "НЕТ";
}