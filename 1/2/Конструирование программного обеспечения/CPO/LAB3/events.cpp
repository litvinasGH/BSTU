#include "stdafx.h"

std::string OutputNameOfTheEvent(int number, bool definition) {
	struct Events
	{
		std::string name;
		int date;
	};
	Events arrayOfEvents[] =
	{
		{"���� �����������", 256},
		{"����� ��", 365},
		{"��", 1},
		{"������ ���� ����", 152}
	};
	if (definition) {
		arrayOfEvents[1].date++;
		arrayOfEvents[3].date++;
	}
	for (int i = 0; i < 4; i++)
		if (number == arrayOfEvents[i].date)
			return arrayOfEvents[i].name;
	return "���";
}