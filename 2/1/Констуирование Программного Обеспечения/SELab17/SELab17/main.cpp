#include "stdafx.h"
#include <iostream>
#include <time.h>

#include <iomanip>

#include "Error.h"	
#include "Parm.h"		
#include "Log.h"		
#include "In.h"			




using namespace std;


int _tmain(int argc, _TCHAR* argv[])
{
	setlocale(LC_ALL, "RUS");

#ifdef DEBUG
	try {
		cout << "--------- тест geterror ---------\n\n";
		throw ERROR_THROW(100);
	}
	catch (Error::ERROR e)
	{
		cout << "Ошибка " << e.id << ": " << e.message << endl;
		if (e.id == 111) {
			cout << "Cтрока " << e.inext.line + 1 << " позиция " << e.inext.col + 1 << "\n\n";
		}
	}


	try {
		cout << "--------- тест geterrorin ---------\n\n";
		throw ERROR_THROW_IN(111, 7, 7);
	}
	catch (Error::ERROR e)
	{
		cout << "Ошибка " << e.id << ": " << e.message << endl;
		if (e.id == 111) {
			cout << "Cтрока " << e.inext.line + 1 << " позиция " << e.inext.col + 1 << "\n\n";
		}
	}
	try {
		cout << "--------- тест getparm ---------\n\n";
		Parm::PARM parm = Parm::getparm(argc, argv);
	}
	catch (Error::ERROR e)
	{
		cout << "Ошибка " << e.id << ": " << e.message << endl;
		if (e.id == 111) {
			cout << "Cтрока " << e.inext.line + 1 << " позиция " << e.inext.col + 1 << "\n\n";
		}
	}

	
	exit(0);
#endif // DEBUG



	Log::LOG log = Log::INITLOG;
	Out::OUT out;
	try
	{
		LT::Create(10);
		Parm::PARM parm = Parm::getparm(argc, argv);
		log = Log::getlog(parm.log);
		Log::WriteLine(log, (char*)"Тест", (char*)" без ошибок \n", "");
		Log::WriteLine(log, (wchar_t*)L"Тест", (wchar_t*)L" без ошибок \n", L"");
		Log::WriteLog(log);
		Log::WriteParm(log, parm);
		out = Out::getOut(parm.out);
		In::IN in = In::getin(parm.in);
		cout << in.text << endl;
		cout << "Всего символов: " << in.size << endl;
		cout << "Всего строк: " << in.lines << endl;
		cout << "Пропущено: " << in.ignor << endl;



		Log::WriteIn(log, in);
		Out::WriteOut(out, in);
		Out::Close(out);
		LT::LexTable lextable = LT::Create(in.count_words);
		IT::IdTable idtable = IT::Create(in.count_words);
		FST::GetLexOrID(in, lextable, idtable);
		Log::WriteLex(log, lextable, idtable);
	}

	catch (Error::ERROR e)
	{
		
		if (e.id == 111) {
			cout << e.inext.text << endl;
			cout << "Ошибка " << e.id << ": " << e.message << endl;
			cout << "Cтрока " << e.inext.line + 1 << " позиция " << e.inext.col + 1 << "\n\n";
			Out::WriteError(out, e);
		}
		else if (e.id == 118) {
			cout << e.inext.rtext << endl;
			cout << "Ошибка " << e.id << ": " << e.message << endl;
			cout << "Cтрока " << e.inext.line + 1 << std::endl << "Лексема \"" << e.inext.text << "\"\n\n";
		}
		else {
			cout << "Ошибка " << e.id << ": " << e.message << endl;
		}
		if (log.stream != NULL)
			Log::WriteError(log, e);
		exit(e.id);
	}
	Log::Close(log);
	system("pause");

	return 0;
}

