//main.cpp
#include "main.h"


int main()
{
	SetConsoleOutputCP(1251);
	SetConsoleCP(1251);
	//setlocale(LC_ALL, "RUS");
	char switch_on;
	do {
		cout << "�������� �������������:\n\t1 � ����������� ������� �������� ����� � ASCII ����� � ��������� � �������� ���������, ���� ������ ������ ���������� ��������, ����� ����� ��������� �� ������;\n\t2 � ����������� ������� �������� ����� � Windows-1251 ����� � ��������� � �������� ���������, ���� ������ ������ �������� ��������, ����� ����� ��������� �� ������;\n\t3 � ����� � ������� ���� �������, ���������������� ��������� �����, ����� ����� ��������� �� ������;\n\t4 � ����� �� ���������." << endl;
		cout << "��� ��� �������: ";
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