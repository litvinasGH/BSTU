#include "stdafx.h"

std::string OutputNameOfTheMonth(int month) {
	std::string names_of_month[] = {
		"������", "�������", "����", "������",
		"���", "����", "����", "������",
		"��������", "�������", "������", "�������"
	};
	return names_of_month[month - 1];
}