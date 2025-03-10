#include "stdafx.h"
using namespace std;

int main() {
    setlocale(LC_ALL, "RUS");
    cout << "������� ���� ������� ��������: ";
    DATE mainDate = inputDate();

    cout << "������� ���� �������� ������� ��������: ";
    DATE BDate = inputDate();

    bool definition = LeapYearDefinition(mainDate.year);
    bool definitionSec = LeapYearDefinition(BDate.year);


    int number_of_day = SequentialNumberCounting(mainDate.day, mainDate.month, definition);

    int days_to_B = OutputDiff(mainDate, BDate);

    cout << "�������������: " << (definition ? "��" : "���") << endl;
    cout << "���� � ����: " << number_of_day << endl;
    cout << "�� ��� ��������: " << days_to_B << endl;
    cout << "�����: " << OutputNameOfTheMonth(mainDate.month) << endl << endl;

    char buffch[4];
    cout << "������ ����� ���: ";
    cin >> buffch;

    int buffi = atoi(buffch);
    if (!(buffi > 0 && buffi <= 366)) {
        errorMes(1);
    }

    cout << "��������: " << OutputNameOfTheEvent(buffi, definition) << endl << endl;

    cout << "������� ���� ������� ��������: ";

    DATE secondDate = inputDate();

    int diff = OutputDiff(mainDate, secondDate);

    cout << "�������: " << diff << endl;

    cout << "������� � �����: " << OutputDiffH(diff);

    return 0;
}


void errorMes(int code) {
    cout << "�� ���������� ����!!!";
    exit(code);
}