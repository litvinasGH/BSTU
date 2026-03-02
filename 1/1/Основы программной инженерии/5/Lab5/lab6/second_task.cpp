//second.cpp
#include "second_task.h"

void doTheSecondTask()
{
	string input_string;
	char char_in_a_different_register;
	cout << "Введите символы: ";
	getline(cin, input_string);
	cin.ignore(cin.rdbuf()->in_avail());
	for (int i = 0; i < input_string.length(); i++){
		if (input_string[0] >= 'А' && input_string[0] <= 'я'){
			do{
			cout << "Введите символ \"" << input_string[i] << "\" в другом регистре: ";
			cin >> char_in_a_different_register;
			if (abs(int(input_string[i]) - int(char_in_a_different_register)) != 32) {
				cout << "Не обманывай меня!" << endl;
			}
		} while (abs(int(input_string[i]) - int(char_in_a_different_register)) != 32);
			cout << "Разница: " << abs(int(input_string[i]) - int(char_in_a_different_register)) << endl;
		}
		else { 
			errorOutput(2);
			break;
		}
	}
}