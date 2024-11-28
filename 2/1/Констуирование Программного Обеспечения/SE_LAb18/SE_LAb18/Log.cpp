#include "stdafx.h"



namespace Log
{
	LOG getlog(wchar_t logfile[])
	{
		LOG log = Log::INITLOG;

		log.stream = new std::ofstream(logfile);

		if (!log.stream)
			throw ERROR_THROW(112);

		return log;
	};


	void WriteLine(LOG log, char* c, ...)
	{
		char** p = &c;

		while (*p != "")
		{
			(*log.stream) << *p;
			p += 1;
		}
	};


	void WriteLine(LOG log, wchar_t* c, ...)
	{
		wchar_t** p = &c;
		char buffer[64];

		while (*p != L"")
		{
			wcstombs_s(0, buffer, *p, sizeof(buffer));
			(*log.stream) << buffer;
			p += 1;
		}
	};


	void WriteLog(LOG log)
	{
		char buffer[PARM_MAX_SIZE];

		time_t rawtime;
		tm timeinfo;

		time(&rawtime);
		localtime_s(&timeinfo, &rawtime);

		strftime(buffer, PARM_MAX_SIZE, "Дата: %d.%m.%y %H:%M:%S", &timeinfo);
		(*log.stream) << "\n--—- Протокол ——--\n" << buffer << "\n";
	};


	void WriteParm(LOG log, Parm::PARM parm)
	{
		char inInfo[PARM_MAX_SIZE];
		char outInfo[PARM_MAX_SIZE];
		char logInfo[PARM_MAX_SIZE];

		wcstombs_s(0, inInfo, parm.in, sizeof(inInfo));
		wcstombs_s(0, outInfo, parm.out, sizeof(outInfo));
		wcstombs_s(0, logInfo, parm.log, sizeof(logInfo));

		(*log.stream) << "--—- Параметры ——--\n"
			<< " -in: \t" << inInfo << "\n"
			<< " -out:\t" << outInfo << "\n"
			<< " -log:\t" << logInfo << "\n";
	};


	void WriteIn(LOG log, In::IN in)
	{
		(*log.stream) << "--—- Исходные данные ——-- \n"
			<< "Кол-во символов:\t" << in.size << "\n"
			<< "Кол-во строк:   \t" << in.lines << "\n"
			<< "Пропущенно:     \t" << in.ignor << "\n";
	};

	void WriteLex(LOG log, LT::LexTable lextable, IT::IdTable idtable) {
		int str = 0;
		int size = [](int number) -> int {
			if (number == 0) return 1;

			int count = 0;
			while (number != 0) {
				number /= 10;
				count++;
			}
			return count;
			}(LT::GetEntry(lextable, lextable.size - 1).sn);
		(*log.stream) << "\n--—- Представления кода в виде лексем ——-- " << endl;
		for (int i = 0; i < lextable.size; i++) {
			if (lextable.table[i].sn + 1 != str) {
				*log.stream << endl;
				cout << endl;
				str = lextable.table[i].sn + 1;
				*log.stream << std::setfill('0') << std::setw(size) << str << ' ';
				cout << std::setfill('0') << std::setw(size) << str << ' ';
			}
			if (lextable.table[i].lexema == 'i') {
				*log.stream << lextable.table[i].lexema << '|' << lextable.table[i].idxTI  + 1 << '|';
				cout << lextable.table[i].lexema << '|' << lextable.table[i].idxTI + 1 << '|';
			}
			else {
				*log.stream << lextable.table[i].lexema;
				cout << lextable.table[i].lexema;
			}
		}
		(*log.stream) << "\n--—------------------------------------------- Идентификаторы ---------------------------------------——-- " << endl;
		cout << "\n---------------------------------------------------------------------------------------------------------" << endl;
		cout << "|  Номер  |    id    | Тип данных |    Тип    | Связь (Номер Лексема Строка) | Значение?" << endl;
		cout << "---------------------------------------------------------------------------------------------------------" << endl;
		(*log.stream) << "|  Номер  |    id    | Тип данных |    Тип    | Связь (Номер Лексема Строка) | Значение?" << endl;
		(*log.stream) << "---------------------------------------------------------------------------------------------------------" << endl;

		for (int i = 0; i < idtable.size; i++) {
			cout << "| " << std::setfill('0') << std::setw(7) << i + 1 << " | ";
			(*log.stream) << "| " << std::setfill('0') << std::setw(7) << i + 1 << " | ";

			cout << std::setfill(' ') <<std::setw(8) << idtable.table[i].id << " | ";
			(*log.stream) << std::setfill(' ') << std::setw(8) << idtable.table[i].id << " | ";

			switch (idtable.table[i].iddatatype) {
			case IT::INT:
				cout << std::setw(10) << "int" << " | ";
				(*log.stream) << std::setw(10) << "int" << " | ";
				break;
			case IT::STR:
				cout << std::setw(10) << "str" << " | ";
				(*log.stream) << std::setw(10) << "str" << " | ";
				break;
			}

			switch (idtable.table[i].idtype) {
			case IT::F:
				cout << std::setw(9) << "func" << " | ";
				(*log.stream) << std::setw(9) << "func" << " | ";
				break;
			case IT::L:
				cout << std::setw(9) << "lit" << " | ";
				(*log.stream) << std::setw(9) << "lit" << " | ";
				break;
			case IT::P:
				cout << std::setw(9) << "param" << " | ";
				(*log.stream) << std::setw(9) << "param" << " | ";
				break;
			case IT::V:
				cout << std::setw(9) << "variable" << " | ";
				(*log.stream) << std::setw(9) << "variable" << " | ";
				break;
			}

			LT::Entry en = LT::GetEntry(lextable, idtable.table[i].idxfirstLE);
			cout << std::setw(9) <<idtable.table[i].idxfirstLE + 1 << std::setw(9) << en.lexema << std::setw(9) << en.sn << "  | ";
			(*log.stream) << std::setw(9) << idtable.table[i].idxfirstLE + 1 << std::setw(9) << en.lexema << std::setw(9) << en.sn << "  | ";

			if (idtable.table[i].idtype == IT::L && idtable.table[i].iddatatype == IT::INT) {
				cout << idtable.table[i].value.vint << " ";
				(*log.stream) << idtable.table[i].value.vint << " ";
			}
			else if (idtable.table[i].idtype == IT::L && idtable.table[i].iddatatype == IT::STR) {
				cout << idtable.table[i].value.vstr.len << " " << idtable.table[i].value.vstr.str << " ";
				(*log.stream) << idtable.table[i].value.vstr.len << " " << idtable.table[i].value.vstr.str << " ";
			}
			cout << endl;
			(*log.stream) << endl;
		}
		cout << "---------------------------------------------------------------------------------------------------------" << endl;
		(*log.stream) << "---------------------------------------------------------------------------------------------------------" << endl;
		

		
	}

	void WriteError(LOG log, Error::ERROR error)
	{
		
			if (error.id == 118) {
				(*log.stream) << "\n--—- Представления кода в виде лексем ——-- " << endl;
				(*log.stream) << error.inext.rtext << endl;
				(*log.stream) << "Ошибка " << error.id << ": " << error.message << endl;
				(*log.stream) << "Cтрока " << error.inext.line + 1 << std::endl << "Лексема \"" << error.inext.text << "\"" << "\n\n";
			}
			else if (error.id == 111)
			{
				(*log.stream) << "Ошибка " << error.id << ": " << error.message << " "
					<< "строка " << error.inext.line << ", позиция: " << error.inext.col << std::endl;
			}
			else
				(*log.stream) << "Ошибка " << error.id << ": " << error.message << "\n";
	};


	void Close(LOG log)
	{
		log.stream->close();
	}
};