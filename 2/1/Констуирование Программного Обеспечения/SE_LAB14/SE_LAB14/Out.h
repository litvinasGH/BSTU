#pragma once
#include "stdafx.h"
#include "In.h"

namespace Out {

	struct OUT
	{
		wchar_t outFile[PARM_MAX_SIZE];
		std::ofstream* stream;
	};

	static const OUT INITOUT{ L"", NULL };					// начальные данные для выходного потока
	OUT getOut(wchar_t outFile[]);							// создание выходного потока
	void WriteOut(OUT out, In::IN in);						// вывод обработанного текста в выходной файл
	void Close(OUT out);									// закрытие выходного потока
}