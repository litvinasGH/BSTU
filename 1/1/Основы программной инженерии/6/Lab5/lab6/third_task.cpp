//three.cpp
#include "third_task.h"

void doTheThirdTask()
{
	string input_string;
	cout << "������� �������: ";
	getline(cin, input_string);

	for (int i = 0; i < input_string.length(); i++){
		if (input_string[0] >= '0' && input_string[0] <= '9') {
			printf("��� ������� ������� %c - %X\n", input_string[i], input_string[i]);
		}
		else { 
			errorOutput(2);
			break;
		}
	}
}