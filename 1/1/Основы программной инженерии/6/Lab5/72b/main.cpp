#include "seven-two-b.h"

int wmain()
{
	_setmode(_fileno(stdout), _O_U16TEXT);
	_setmode(_fileno(stdin), _O_U16TEXT);
	_setmode(_fileno(stderr), _O_U16TEXT);
	wchar_t symbol; char symbol_code[33];
	wcout << L"������� �����:";
	wcin.get(symbol);
	_itoa_s(symbol, symbol_code, 2);
	convertUnicodeToUTF8(symbol_code);
	wprintf(L"� UTF-8(16-������ �������������): %X", convertingBinToTen(symbol_code));
	return 0;
}