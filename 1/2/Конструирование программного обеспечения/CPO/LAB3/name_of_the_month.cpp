#include "stdafx.h"

std::string OutputNameOfTheMonth(int month) {
	std::string names_of_month[] = {
		"Январь", "Февраль", "Март", "Апрель",
		"Май", "Июнь", "Июль", "Август",
		"Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
	};
	return names_of_month[month - 1];
}