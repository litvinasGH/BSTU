//second.cpp
#include "second_task.h"

void doTheSecondTask()
{
	string input_string;
	char char_in_a_different_register;
	cout << "������� �������: ";
	getline(cin, input_string);
	cin.ignore(cin.rdbuf()->in_avail());
	for (int i = 0; i < input_string.length(); i++){
		if (input_string[0] >= '�' && input_string[0] <= '�'){
			do{
			cout << "������� ������ \"" << input_string[i] << "\" � ������ ��������: ";
			cin >> char_in_a_different_register;
			if (abs(int(input_string[i]) - int(char_in_a_different_register)) != 32) {
				cout << "�� ��������� ����!" << endl;
			}
		} while (abs(int(input_string[i]) - int(char_in_a_different_register)) != 32);
			cout << "�������: " << abs(int(input_string[i]) - int(char_in_a_different_register)) << endl;
		}
		else { 
			errorOutput(2);
			break;
		}
	}
}