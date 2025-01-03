#pragma once
#include "stdafx.h"
#include "LT.h"
#include "IT.h"

namespace Log								// ������ � ����������
{
	struct LOG								// ��������
	{
		wchar_t logfile[PARM_MAX_SIZE];		// ��� ����� ���������
		std::ofstream* stream;				// �������� ����� ���������
	}; 

	static const LOG INITLOG{ L"", NULL };				// ��������� ��� ��������� ������������� LOG
	LOG getlog(wchar_t logfile[]);						// ������������ ��������� LOG
	void WriteLine(LOG log, char* c, ...);				// ������� � �������� ������������ �����
	void WriteLine(LOG log, wchar_t* c, ...);			// ������� � �������� ������������ �����
	void WriteLog(LOG log);								// ������� � �������� ���������
	void WriteParm(LOG log, Parm::PARM parm);			// ������� � �������� ���������� � ������� ������
	void WriteIn(LOG log, In::IN in);					// ������� � �������� ���������� � ������� ���������
	void WriteError(LOG log, Error::ERROR error);		// ������� � �������� ���������� �� ������
	void Close(LOG log);
	void WriteLex(LOG log, LT::LexTable lextable, IT::IdTable idtable);
}