#include <iostream>
#include <atlstr.h>

typedef unsigned char type;
typedef unsigned char name;
typedef unsigned int number;
typedef unsigned int cnt;
typedef unsigned int price;
typedef unsigned char manufacturer;
typedef unsigned int implementationPer;

struct Product
{
	type tp[10];
	name nam[10];
	number num;
	cnt cn;
	price prc;
	manufacturer manuf[10];
	implementationPer per;

	double operator+(double n) const {
		return (prc * cn) + (prc * cn) * (n / 100);
	}

	double operator-(double n) const {
		return (prc * cn) - (prc * cn) * (n / 100);
	}

	bool operator<(const Product& other) const {
		return cn * prc < other.cn * other.prc;
	}

	bool operator>(const Product& other) const {
		return cn * prc > other.cn * other.prc;
	}
};

int _tmain(int argc, _TCHAR* argv[])
{
	setlocale(LC_ALL, "rus");

	Product p1 = { "Type1", "Name1", 1234, 10, 5, "Manuf1", 365 };
	Product p2 = { "Type2", "Name2", 4352, 60, 1, "Manuf1", 40 };

	std::cout << p1 + 0 << "\t" << p1 + 5 << std::endl;
	std::cout << p2 - 0 << "\t" << p2 - 10 << std::endl;
	std::cout << (p1 < p2 ? "ÈÑÒÈÍÀ" : "ËÎÆÜ") << std::endl;
	std::cout << (p1 > p2 ? "ÈÑÒÈÍÀ" : "ËÎÆÜ") << std::endl;
}