#include <iostream>
#include<Windows.h>


int main()
{

	char hello[15];
	int h,j;
	for (;;) {
		for (int g=0; g<=14; g++)
		{
			j = 48 + rand() % 60;
			if (j >= 58 && j <= 64) {  }
				
			else { hello[g] = j; }
		}
		std::cout << hello << std::endl;
		Sleep(500);
		if (rand() == 1000) { break; }

	}
	
}