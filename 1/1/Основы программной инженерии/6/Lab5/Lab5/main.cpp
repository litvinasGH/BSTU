
#include <iostream>
#include <windows.h>
using namespace std;

void error(int i)
{
	switch (i)
	{
	case 1:	MessageBox(NULL, TEXT("Неверный вариант. Надо ввести от 1 до 4"), TEXT("Неверный вариант =["), MB_OK | MB_ICONERROR); break;
	case 2: MessageBox(NULL, TEXT("Неверный символ. Читайте условие"), TEXT("Неверный символ =[."), MB_OK | MB_ICONERROR); break;
	default: MessageBox(NULL, TEXT("Неизвестная ошибка"), TEXT("Неизвестная ошибка =["), MB_OK | MB_ICONERROR);
		break;
	}
}
//функция для первого задания
void fir(char c)
{
	char C;
	if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
	{
		cout << "Введите этот же символ в другом регистре: ";
		cin >> C;
		cout << "Разница: " << abs(int(c) - int(C))<<endl;
	}
	else error(2);
}
//функция для второго задания
void sec(char c)
{
	char C;
	if ( c >= 'А' && c <= 'я')
	{
		cout << "Введите этот же символ в другом регистре: ";
		cin >> C;
		cout << "Разница: " << abs(int(c) - int(C)) << endl;
	}
	else error(2);
}
//функция для третьего задания
void thr(char c)
{
	if (c >= '0' && c <= '9')
	{
		printf("Код данного символа - %X\n", c);
	}
	else error(2);
}

void main()
{
	SetConsoleOutputCP(1251);
	SetConsoleCP(1251);
	//setlocale(LC_ALL, "RUS");
	char c;
	int sw;
	char swc;
	do {
		cout << "Варианты использования:\n\t1 – определение разницы значений кодов в ASCII буквы в прописном и строчном написании, если введен символ латинского алфавита, иначе вывод сообщения об ошибке;\n\t2 – определение разницы значений кодов в Windows-1251 буквы в прописном и строчном написании, если введен символ русского алфавита, иначе вывод сообщения об ошибке;\n\t3 – вывод в консоль кода символа, соответствующего введенной цифре, иначе вывод сообщения об ошибке;\n\t4 – выход из программы." << endl;
		cout << "Что мне с этим сделать: ";
		do {
			cin >> swc;
			if (swc >= '0' && swc <= '9') sw = swc - '0';
		} while (!(swc >= '0' && swc <= '9'));
		switch (sw)
		{
		case 1:		cout << "Введите символ: ";
			cin >> c; fir(c); break;
		case 2:		cout << "Введите символ: ";
			cin >> c; sec(c); break;
		case 3:		cout << "Введите символ: ";
			cin >> c; thr(c); break;
		case 4: break;
		default: error(1); break;
		}
	} while (sw!=4);
}