
#include <iostream>
#include <windows.h>
using namespace std;

void error(int i)
{
	switch (i)
	{
	case 1:	MessageBox(NULL, TEXT("�������� �������. ���� ������ �� 1 �� 4"), TEXT("�������� ������� =["), MB_OK | MB_ICONERROR); break;
	case 2: MessageBox(NULL, TEXT("�������� ������. ������� �������"), TEXT("�������� ������ =[."), MB_OK | MB_ICONERROR); break;
	default: MessageBox(NULL, TEXT("����������� ������"), TEXT("����������� ������ =["), MB_OK | MB_ICONERROR);
		break;
	}
}
//������� ��� ������� �������
void fir(char c)
{
	char C;
	if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
	{
		cout << "������� ���� �� ������ � ������ ��������: ";
		cin >> C;
		cout << "�������: " << abs(int(c) - int(C))<<endl;
	}
	else error(2);
}
//������� ��� ������� �������
void sec(char c)
{
	char C;
	if ( c >= '�' && c <= '�')
	{
		cout << "������� ���� �� ������ � ������ ��������: ";
		cin >> C;
		cout << "�������: " << abs(int(c) - int(C)) << endl;
	}
	else error(2);
}
//������� ��� �������� �������
void thr(char c)
{
	if (c >= '0' && c <= '9')
	{
		printf("��� ������� ������� - %X\n", c);
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
		cout << "�������� �������������:\n\t1 � ����������� ������� �������� ����� � ASCII ����� � ��������� � �������� ���������, ���� ������ ������ ���������� ��������, ����� ����� ��������� �� ������;\n\t2 � ����������� ������� �������� ����� � Windows-1251 ����� � ��������� � �������� ���������, ���� ������ ������ �������� ��������, ����� ����� ��������� �� ������;\n\t3 � ����� � ������� ���� �������, ���������������� ��������� �����, ����� ����� ��������� �� ������;\n\t4 � ����� �� ���������." << endl;
		cout << "��� ��� � ���� �������: ";
		do {
			cin >> swc;
			if (swc >= '0' && swc <= '9') sw = swc - '0';
		} while (!(swc >= '0' && swc <= '9'));
		switch (sw)
		{
		case 1:		cout << "������� ������: ";
			cin >> c; fir(c); break;
		case 2:		cout << "������� ������: ";
			cin >> c; sec(c); break;
		case 3:		cout << "������� ������: ";
			cin >> c; thr(c); break;
		case 4: break;
		default: error(1); break;
		}
	} while (sw!=4);
}