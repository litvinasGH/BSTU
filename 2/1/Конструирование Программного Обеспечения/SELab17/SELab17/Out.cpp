#include "stdafx.h"

//#pragma warning(disable:4996)

namespace Out {

	OUT getOut(wchar_t outFile[])
	{
		OUT out = INITOUT;

		out.stream = new ofstream(outFile);

		if (!out.stream)
			throw ERROR_THROW(112);

		return out;
	};

	void WriteOut(OUT out, In::IN in) {

		if ((*out.stream).is_open()) {
			(*out.stream) << in.text;
		}
	}

	void WriteError(OUT out, Error::ERROR e) {
		if ((*out.stream).is_open()) {
			*out.stream << e.inext.text << endl;
			*out.stream << "Ошибка " << e.id << ": " << e.message << endl;
			*out.stream << "Cтрока " << e.inext.line + 1 << " позиция " << e.inext.col + 1 << "\n\n";
		}
	}

	void Close(OUT out)
	{
		out.stream->close();
	}
}