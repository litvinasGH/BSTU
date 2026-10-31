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
			wcstombs(buffer, *p, sizeof(buffer));
			(*log.stream) << buffer;
			p += 1;
		}
	};


	void WriteLog(LOG log)
	{
		char buffer[PARM_MAX_SIZE];

		time_t rawtime;
		struct tm* timeinfo;

		time(&rawtime);
		timeinfo = localtime(&rawtime);

		strftime(buffer, PARM_MAX_SIZE, "Дата: %d.%m.%y %H:%M:%S", timeinfo);
		(*log.stream) << "\n--—- Протокол ——--\n" << buffer << "\n";
	};


	void WriteParm(LOG log, Parm::PARM parm)
	{
		char inInfo[PARM_MAX_SIZE];
		char outInfo[PARM_MAX_SIZE];
		char logInfo[PARM_MAX_SIZE];

		wcstombs(inInfo, parm.in, sizeof(inInfo));
		wcstombs(outInfo, parm.out, sizeof(outInfo));
		wcstombs(logInfo, parm.log, sizeof(logInfo));

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


	void WriteError(LOG log, Error::ERROR error)
	{
		
			if (error.id != 100)
			{
				(*log.stream) << "Ошибка " << error.id << ": " << error.message << "\n";
			}
			else
			{
				(*log.stream) << "Ошибка " << error.id << ": " << error.message << " "
					<< "строка " << error.inext.line << ", позиция: " << error.inext.col << std::endl;
			}
		
	};


	void Close(LOG log)
	{
		log.stream->close();
	}
};