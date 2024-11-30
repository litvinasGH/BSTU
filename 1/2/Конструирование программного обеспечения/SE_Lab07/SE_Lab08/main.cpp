#include <iostream>

using namespace std;

int defaultparm(int a1, int a2, int a3, int a4, int a5, int a6 = 1, int a7 = 2)
{
	return (a1 + a2 + a3 + a4 + a5 + a6 + a7) / 7;
};


int main()
{
	int x1 = defaultparm(1, 2, 3, 4, 5);
	int x2 = defaultparm(1, 2, 3, 4, 5, 6, 7);
}