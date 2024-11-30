#include <iostream>
#include "Call.h"

int main() {
	setlocale(LC_ALL, "ru");

	int a = 1;

	int cde = Call::cdeml(1, 3, 5);
	int cst = Call::cstd(a, 3, 5);
	int cfs = Call::cfst(1, 3, 5, 7);
	return 0;
}