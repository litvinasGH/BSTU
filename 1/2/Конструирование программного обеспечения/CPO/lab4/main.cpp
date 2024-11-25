#include <iostream>
#include <atlstr.h>

typedef unsigned char day;
typedef unsigned char month;
typedef unsigned int year;

struct Date
{
	day dd;
	month mm;
	year yyyy;
    bool operator<(const Date& other) const {
		return yyyy < other.yyyy || mm < other.mm || dd < other.dd;
    }

    bool operator>(const Date& other) const {
		return yyyy > other.yyyy || mm > other.mm || dd > other.dd;
    }

    bool operator==(const Date& other) const {
		return yyyy == other.yyyy && mm == other.mm && dd == other.dd;
    }
};

int _tmain(int argc, _TCHAR* argv[])
{

	setlocale(LC_ALL, "rus");

	Date date1 = { 7,1,1980 };
	Date date2 = { 7,2,1993 };
	Date date3 = { 7,1,1980 };

	if (date1 < date2) std::cout << "истина" << std::endl;
	else std::cout << "ложь" << std::endl;

	if (date1 > date2) std::cout << "истина" << std::endl;
	else std::cout << "ложь" << std::endl;

	if (date1 == date2) std::cout << "истина" << std::endl;
	else std::cout << "ложь" << std::endl;

	if (date1 == date3) std::cout << "истина" << std::endl;
	else std::cout << "ложь" << std::endl;
}