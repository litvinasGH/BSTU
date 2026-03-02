#include <iostream>
#include <windows.h>
using namespace std;

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
void main()
{
	setlocale(LC_ALL, "RUS");
	int a;
	cin >> a;
	cout << tentotwo(a);
}
