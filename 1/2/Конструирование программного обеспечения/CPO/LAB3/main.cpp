#include "stdafx.h"
using namespace std;

int main() {
    setlocale(LC_ALL, "RUS");
    cout << "Ââåäèòå äàòó ôîðìàòà ÄÄÌÌÃÃÃÃ: ";
    DATE mainDate = inputDate();

    cout << "Ââåäèòå äàòó ðîæäåíèÿ ôîðìàòà ÄÄÌÌÃÃÃÃ: ";
    DATE BDate = inputDate();

    bool definition = LeapYearDefinition(mainDate.year);
    bool definitionSec = LeapYearDefinition(BDate.year);


    int number_of_day = SequentialNumberCounting(mainDate.day, mainDate.month, definition);

    int days_to_B = OutputDiff(mainDate, BDate);

    cout << "ÂÈÑÎÊÎÑÒÍÎÑÒÜ: " << (definition ? "ÄÀ" : "ÍÅÒ") << endl;
    cout << "ÄÅÍÜ Â ÃÎÄÓ: " << number_of_day << endl;
    cout << "ÄÎ ÄÍß ÐÎÆÄÅÍÈß: " << days_to_B << endl;
    cout << "ÌÅÑßÖ: " << OutputNameOfTheMonth(mainDate.month) << endl << endl;

    char buffch[4];
    cout << "Ââåñòè íîìåð äíÿ: ";
    cin >> buffch;

    int buffi = atoi(buffch);
    if (!(buffi > 0 && buffi <= 366)) {
        errorMes(1);
    }

    cout << "ÏÐÀÇÄÍÈÊ: " << OutputNameOfTheEvent(buffi, definition) << endl << endl;

    cout << "Ââåäèòå äàòó ôîðìàòà ÄÄÌÌÃÃÃÃ: ";

    DATE secondDate = inputDate();

    int diff = OutputDiff(mainDate, secondDate);

    cout << "ÐÀÇÍÈÖÀ: " << diff << endl;

    cout << "ÐÀÇÍÈÖÀ Â ×ÀÑÀÕ: " << OutputDiffH(diff);

    return 0;
}


void errorMes(int code) {
    cout << "ÍÅ ÊÎÐÐÅÊÒÍÛÉ ÂÂÎÄ!!!";
    exit(code);
}