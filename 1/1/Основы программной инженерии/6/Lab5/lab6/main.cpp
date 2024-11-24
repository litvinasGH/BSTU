//main.cpp
#include "main.h"


int main()
{
	SetConsoleOutputCP(1251);
	SetConsoleCP(1251);
	//setlocale(LC_ALL, "RUS");
	char switch_on;
	do {
		cout << "¬арианты использовани€:\n\t1 Ц определение разницы значений кодов в ASCII буквы в прописном и строчном написании, если введен символ латинского алфавита, иначе вывод сообщени€ об ошибке;\n\t2 Ц определение разницы значений кодов в Windows-1251 буквы в прописном и строчном написании, если введен символ русского алфавита, иначе вывод сообщени€ об ошибке;\n\t3 Ц вывод в консоль кода символа, соответствующего введенной цифре, иначе вывод сообщени€ об ошибке;\n\t4 Ц выход из программы." << endl;
		cout << "„то мне сделать: ";
		cin >> switch_on;
		switch_on -= '0';
		cin.ignore(cin.rdbuf()->in_avail());
		switch (switch_on) {
		case 1: doTheFirstTask(); system("pause"); system("cls"); break;
		case 2: doTheSecondTask(); system("pause"); system("cls"); break;
		case 3: doTheThirdTask(); system("pause"); system("cls"); break;
		case 4: break;
		default: errorOutput(1); system("pause"); system("cls"); break;
		}
	} while ((switch_on) != 4);
	return 0;
}