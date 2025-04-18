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

		strftime(buffer, PARM_MAX_SIZE, "����: %d.%m.%y %H:%M:%S", &timeinfo);
		(*log.stream) << "\n--�- �������� ��--\n" << buffer << "\n";
	};


	void WriteParm(LOG log, Parm::PARM parm)
	{
		char inInfo[PARM_MAX_SIZE];
		char outInfo[PARM_MAX_SIZE];
		char logInfo[PARM_MAX_SIZE];

		wcstombs_s(0, inInfo, parm.in, sizeof(inInfo));
		wcstombs_s(0, outInfo, parm.out, sizeof(outInfo));
		wcstombs_s(0, logInfo, parm.log, sizeof(logInfo));

		(*log.stream) << "--�- ��������� ��--\n"
			<< " -in: \t" << inInfo << "\n"
			<< " -out:\t" << outInfo << "\n"
			<< " -log:\t" << logInfo << "\n";
	};


	void WriteIn(LOG log, In::IN in)
	{
		(*log.stream) << "--�- �������� ������ ��-- \n"
			<< "���-�� ��������:\t" << in.size << "\n"
			<< "���-�� �����:   \t" << in.lines << "\n"
			<< "����������:     \t" << in.ignor << "\n";
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
		(*log.stream) << "\n--�- ������������� ���� � ���� ������ ��-- " << endl;
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
		cout << endl << endl;
		size = [](int number) -> int {
			if (number == 0) return 1;

			int count = 0;
			while (number != 0) {
				number /= 10;
				count++;
			}
			return count;
			}(idtable.size);
		(*log.stream) << "\n--�- �������������� ��-- " << endl;
		cout << "�����\tid\t��� ������\t���\t����� � ��������\t\t\t��������?" << endl;
		(*log.stream) << "�����\tid\t��� ������\t���\t����� � ��������\t\t\t��������?" << endl;
		for (int i = 0; i < idtable.size; i++) {
			*log.stream << std::setfill('0') << std::setw(size) << i + 1 << ' ';
			cout << std::setfill('0') << std::setw(size) << i + 1 << ' ';
			cout << idtable.table[i].id << " ";
			*log.stream << idtable.table[i].id << " ";
			switch (idtable.table[i].iddatatype)
			{
			case IT::INT:
				cout << "int ";
				*log.stream << "int ";
				break;
			case IT::STR:
				cout << "str ";
				*log.stream << "str ";
				break;
			}
			switch (idtable.table[i].idtype)
			{
			case IT::F:
				cout << "func ";
				*log.stream << "func ";
				break;
			case IT::L:
				cout << "lit ";
				*log.stream << "lit ";
				break;
			case IT::P:
				cout << "param ";
				*log.stream << "param ";
				break;
			case IT::V:
				cout << "variable ";
				*log.stream << "variable ";
				break;
			}
			LT::Entry en = LT::GetEntry(lextable, idtable.table[i].idxfirstLE);
			cout << idtable.table[i].idxfirstLE + 1 << " " << en.lexema << " " << en.sn << " ";
			*log.stream << idtable.table[i].idxfirstLE + 1 << " " << en.lexema << " " << en.sn << " ";
			if (idtable.table[i].idtype == IT::L && idtable.table[i].iddatatype == IT::INT) {
				cout << idtable.table[i].value.vint << " ";
				*log.stream << idtable.table[i].value.vint << " ";
			}
			else if (idtable.table[i].idtype == IT::L && idtable.table[i].iddatatype == IT::STR) {
				cout << idtable.table[i].value.vstr.len << " \"" << idtable.table[i].value.vstr.str << "\" ";
				*log.stream << idtable.table[i].value.vstr.len << " \"" << idtable.table[i].value.vstr.str << "\" ";
			}
			cout << endl;
			*log.stream << endl;
		}
	}

	void WriteError(LOG log, Error::ERROR error)
	{
		
			if (error.id == 118) {
				(*log.stream) << "\n--�- ������������� ���� � ���� ������ ��-- " << endl;
				(*log.stream) << error.inext.rtext << endl;
				(*log.stream) << "������ " << error.id << ": " << error.message << endl;
				(*log.stream) << "C����� " << error.inext.line + 1 << std::endl << "������� \"" << error.inext.text << "\"" << "\n\n";
			}
			else if (error.id == 111)
			{
				(*log.stream) << "������ " << error.id << ": " << error.message << " "
					<< "������ " << error.inext.line << ", �������: " << error.inext.col << std::endl;
			}
			else
				(*log.stream) << "������ " << error.id << ": " << error.message << "\n";
	};


	void Close(LOG log)
	{
		log.stream->close();
	}
};