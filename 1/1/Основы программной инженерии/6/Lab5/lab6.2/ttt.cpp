//ttt.cpp
#include "sixpointtwo.h"

int tentotwo(int a)
{
	int b = 0, n = 0;
	while (a != 0)
	{
		b += (a % 2) * pow(10, n);
		a /= 2;
		n++;
	}
	return b;
}