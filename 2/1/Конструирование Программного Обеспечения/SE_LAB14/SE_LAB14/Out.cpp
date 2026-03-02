#include "stdafx.h"

#pragma warning(disable:4996)

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

	void Close(OUT out)
	{
		out.stream->close();
	}
}