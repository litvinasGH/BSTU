//first.cpp
#include "first_task.h"
void doTheFirstTask()
{
	char char_in_a_different_register;
	string input_string;
	cout << "Введите символы: ";
	getline(cin, input_string);
	cin.ignore(cin.rdbuf()->in_avail());

	for (int i = 0; i < input_string.length(); i++){
		if ((input_string[i] >= 'a' && input_string[i] <= 'z') || (input_string[i] >= 'A' && input_string[i] <= 'Z')){
			do {
				cout << "Введите символ \"" << input_string[i] << "\" в другом регистре: ";
				cin >> char_in_a_different_register;
			} while (abs(int(input_string[i]) - int(char_in_a_different_register)) != 32);
			cout << "Разница: " << abs(int(input_string[i]) - int(char_in_a_different_register)) << endl;
		}
		else { 
			errorOutput(2); 
			break;
		}
	}
}