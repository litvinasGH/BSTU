#include "FST.h"
#include <iostream>
#include <stdarg.h>
#include "stdafx.h"


#include <iomanip>



namespace FST
{
	RELATION::RELATION(char c, short ns)
	{
		symbol = c;
		nnode = ns;
	};


	NODE::NODE() {
		n_relation = 0;
		RELATION* relations = nullptr;
	}
	NODE::NODE(short n, RELATION rel, ...)
	{
		n_relation = n;
		RELATION* p = &rel;
		relations = new RELATION[n];
		for (int i = 0; i < n; i++)
		{
			relations[i] = p[i];
		}
	}

	FST::FST(const char* s, short ns, NODE n, ...) {
		string = s;
		nstates = ns;
		nodes = new NODE[ns];

		va_list args;
		va_start(args, n);
		nodes[0] = n;

		for (int k = 1; k < ns; k++) {
			nodes[k] = va_arg(args, NODE);
		}

		va_end(args);
		rstates = new short[nstates];
		memset(rstates, 0xff, sizeof(short) * nstates);
		rstates[0] = 0;
		position = -1;
	}


	bool execute(FST& fst)
	{
		short* rstates = new short[fst.nstates];
		memset(rstates, 0xff, sizeof(short) * fst.nstates);

		short lstring = strlen(fst.string);

		bool rc = true;

		for (short i = 0; i < lstring && rc; i++)
		{
			fst.position++;
			rc = [](FST& fst, short*& rstates) -> bool {
				bool rc = false;

				std::swap(rstates, fst.rstates);

				for (short i = 0; i < fst.nstates; i++)
				{
					if (rstates[i] == fst.position)
					{

						for (short j = 0; j < fst.nodes[i].n_relation; j++)
						{

							if (fst.nodes[i].relations[j].symbol == fst.string[fst.position])
							{

								fst.rstates[fst.nodes[i].relations[j].nnode] = fst.position + 1;
								rc = true;
							}
						}
					}
				}
				return rc;
				}(fst, rstates);;
		}

		delete[] rstates;


		return (rc ? (fst.rstates[fst.nstates - 1] == lstring) : rc);
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="word"></param>
	/// <returns>s = litstr; i = litint; I = ID</returns>
	int GetID(char* word) {
		FST strLit(
			word,
			4,
			NODE(1, RELATION('\'', 1)),
			NODE(258,
				RELATION('A', 1), RELATION('A', 2), RELATION('B', 1), RELATION('B', 2),
				RELATION('C', 1), RELATION('C', 2), RELATION('D', 1), RELATION('D', 2),
				RELATION('E', 1), RELATION('E', 2), RELATION('F', 1), RELATION('F', 2),
				RELATION('G', 1), RELATION('G', 2), RELATION('H', 1), RELATION('H', 2),
				RELATION('I', 1), RELATION('I', 2), RELATION('J', 1), RELATION('J', 2),
				RELATION('K', 1), RELATION('K', 2), RELATION('L', 1), RELATION('L', 2),
				RELATION('M', 1), RELATION('M', 2), RELATION('N', 1), RELATION('N', 2),
				RELATION('O', 1), RELATION('O', 2), RELATION('P', 1), RELATION('P', 2),
				RELATION('Q', 1), RELATION('Q', 2), RELATION('R', 1), RELATION('R', 2),
				RELATION('S', 1), RELATION('S', 2), RELATION('T', 1), RELATION('T', 2),
				RELATION('U', 1), RELATION('U', 2), RELATION('V', 1), RELATION('V', 2),
				RELATION('W', 1), RELATION('W', 2), RELATION('X', 1), RELATION('X', 2),
				RELATION('Y', 1), RELATION('Y', 2), RELATION('Z', 1), RELATION('Z', 2),
				RELATION('a', 1), RELATION('a', 2), RELATION('b', 1), RELATION('b', 2),
				RELATION('c', 1), RELATION('c', 2), RELATION('d', 1), RELATION('d', 2),
				RELATION('e', 1), RELATION('e', 2), RELATION('f', 1), RELATION('f', 2),
				RELATION('g', 1), RELATION('g', 2), RELATION('h', 1), RELATION('h', 2),
				RELATION('i', 1), RELATION('i', 2), RELATION('j', 1), RELATION('j', 2),
				RELATION('k', 1), RELATION('k', 2), RELATION('l', 1), RELATION('l', 2),
				RELATION('m', 1), RELATION('m', 2), RELATION('n', 1), RELATION('n', 2),
				RELATION('o', 1), RELATION('o', 2), RELATION('p', 1), RELATION('p', 2),
				RELATION('q', 1), RELATION('q', 2), RELATION('r', 1), RELATION('r', 2),
				RELATION('s', 1), RELATION('s', 2), RELATION('t', 1), RELATION('t', 2),
				RELATION('u', 1), RELATION('u', 2), RELATION('v', 1), RELATION('v', 2),
				RELATION('w', 1), RELATION('w', 2), RELATION('x', 1), RELATION('x', 2),
				RELATION('y', 1), RELATION('y', 2), RELATION('z', 1), RELATION('z', 2),
				RELATION('À', 1), RELATION('À', 2), RELATION('Á', 1), RELATION('Á', 2),
				RELATION('Â', 1), RELATION('Â', 2), RELATION('Ã', 1), RELATION('Ã', 2),
				RELATION('Ä', 1), RELATION('Ä', 2), RELATION('Å', 1), RELATION('Å', 2),
				RELATION('¨', 1), RELATION('¨', 2), RELATION('Æ', 1), RELATION('Æ', 2),
				RELATION('Ç', 1), RELATION('Ç', 2), RELATION('È', 1), RELATION('È', 2),
				RELATION('É', 1), RELATION('É', 2), RELATION('Ê', 1), RELATION('Ê', 2),
				RELATION('Ë', 1), RELATION('Ë', 2), RELATION('Ì', 1), RELATION('Ì', 2),
				RELATION('Í', 1), RELATION('Í', 2), RELATION('Î', 1), RELATION('Î', 2),
				RELATION('Ï', 1), RELATION('Ï', 2), RELATION('Ð', 1), RELATION('Ð', 2),
				RELATION('Ñ', 1), RELATION('Ñ', 2), RELATION('Ò', 1), RELATION('Ò', 2),
				RELATION('Ó', 1), RELATION('Ó', 2), RELATION('Ô', 1), RELATION('Ô', 2),
				RELATION('Õ', 1), RELATION('Õ', 2), RELATION('Ö', 1), RELATION('Ö', 2),
				RELATION('×', 1), RELATION('×', 2), RELATION('Ø', 1), RELATION('Ø', 2),
				RELATION('Ù', 1), RELATION('Ù', 2), RELATION('Ú', 1), RELATION('Ú', 2),
				RELATION('Û', 1), RELATION('Û', 2), RELATION('Ü', 1), RELATION('Ü', 2),
				RELATION('Ý', 1), RELATION('Ý', 2), RELATION('Þ', 1), RELATION('Þ', 2),
				RELATION('ß', 1), RELATION('ß', 2), RELATION('à', 1), RELATION('à', 2),
				RELATION('á', 1), RELATION('á', 2), RELATION('â', 1), RELATION('â', 2),
				RELATION('ã', 1), RELATION('ã', 2), RELATION('ä', 1), RELATION('ä', 2),
				RELATION('å', 1), RELATION('å', 2), RELATION('¸', 1), RELATION('¸', 2),
				RELATION('æ', 1), RELATION('æ', 2), RELATION('ç', 1), RELATION('ç', 2),
				RELATION('è', 1), RELATION('è', 2), RELATION('é', 1), RELATION('é', 2),
				RELATION('ê', 1), RELATION('ê', 2), RELATION('ë', 1), RELATION('ë', 2),
				RELATION('ì', 1), RELATION('ì', 2), RELATION('í', 1), RELATION('í', 2),
				RELATION('î', 1), RELATION('î', 2), RELATION('ï', 1), RELATION('ï', 2),
				RELATION('ð', 1), RELATION('ð', 2), RELATION('ñ', 1), RELATION('ñ', 2),
				RELATION('ò', 1), RELATION('ò', 2), RELATION('ó', 1), RELATION('ó', 2),
				RELATION('ô', 1), RELATION('ô', 2), RELATION('õ', 1), RELATION('õ', 2),
				RELATION('ö', 1), RELATION('ö', 2), RELATION('÷', 1), RELATION('÷', 2),
				RELATION('ø', 1), RELATION('ø', 2), RELATION('ù', 1), RELATION('ù', 2),
				RELATION('ú', 1), RELATION('ú', 2), RELATION('û', 1), RELATION('û', 2),
				RELATION('ü', 1), RELATION('ü', 2), RELATION('ý', 1), RELATION('ý', 2),
				RELATION('þ', 1), RELATION('þ', 2), RELATION('ÿ', 1), RELATION('ÿ', 2),
				RELATION('0', 1), RELATION('0', 2), RELATION('1', 1), RELATION('1', 2),
				RELATION('2', 1), RELATION('2', 2), RELATION('3', 1), RELATION('3', 2),
				RELATION('4', 1), RELATION('4', 2), RELATION('5', 1), RELATION('5', 2),
				RELATION('6', 1), RELATION('6', 2), RELATION('7', 1), RELATION('7', 2),
				RELATION('8', 1), RELATION('8', 2), RELATION('9', 1), RELATION('9', 2),
				RELATION(' ', 1), RELATION(' ', 2)),
			NODE(1, RELATION('\'', 3)),
			NODE()
		);
		if (execute(strLit)) {
			return 's';
		}
		FST intLit(
			word,
			2,
			NODE(20, RELATION('0', 0), RELATION('0', 1), RELATION('1', 0), RELATION('1', 1), RELATION('2', 0),
				RELATION('2', 1), RELATION('3', 0), RELATION('3', 1), RELATION('4', 0), RELATION('4', 1),
				RELATION('5', 0), RELATION('5', 1), RELATION('6', 0), RELATION('6', 1), RELATION('7', 0),
				RELATION('7', 1), RELATION('8', 0), RELATION('8', 1), RELATION('9', 0), RELATION('9', 1)),
			NODE()
		);
		if (execute(intLit)) {

			return 'i';
		}



		FST iLex(
			word,
			3,
			NODE(156,
				RELATION('A', 0), RELATION('A', 1), RELATION('A', 2),
				RELATION('B', 0), RELATION('B', 1), RELATION('B', 2),
				RELATION('C', 0), RELATION('C', 1), RELATION('C', 2),
				RELATION('D', 0), RELATION('D', 1), RELATION('D', 2),
				RELATION('E', 0), RELATION('E', 1), RELATION('E', 2),
				RELATION('F', 0), RELATION('F', 1), RELATION('F', 2),
				RELATION('G', 0), RELATION('G', 1), RELATION('G', 2),
				RELATION('H', 0), RELATION('H', 1), RELATION('H', 2),
				RELATION('I', 0), RELATION('I', 1), RELATION('I', 2),
				RELATION('J', 0), RELATION('J', 1), RELATION('J', 2),
				RELATION('K', 0), RELATION('K', 1), RELATION('K', 2),
				RELATION('L', 0), RELATION('L', 1), RELATION('L', 2),
				RELATION('M', 0), RELATION('M', 1), RELATION('M', 2),
				RELATION('N', 0), RELATION('N', 1), RELATION('N', 2),
				RELATION('O', 0), RELATION('O', 1), RELATION('O', 2),
				RELATION('P', 0), RELATION('P', 1), RELATION('P', 2),
				RELATION('Q', 0), RELATION('Q', 1), RELATION('Q', 2),
				RELATION('R', 0), RELATION('R', 1), RELATION('R', 2),
				RELATION('S', 0), RELATION('S', 1), RELATION('S', 2),
				RELATION('T', 0), RELATION('T', 1), RELATION('T', 2),
				RELATION('U', 0), RELATION('U', 1), RELATION('U', 2),
				RELATION('V', 0), RELATION('V', 1), RELATION('V', 2),
				RELATION('W', 0), RELATION('W', 1), RELATION('W', 2),
				RELATION('X', 0), RELATION('X', 1), RELATION('X', 2),
				RELATION('Y', 0), RELATION('Y', 1), RELATION('Y', 2),
				RELATION('Z', 0), RELATION('Z', 1), RELATION('Z', 2),
				RELATION('a', 0), RELATION('a', 1), RELATION('a', 2),
				RELATION('b', 0), RELATION('b', 1), RELATION('b', 2),
				RELATION('c', 0), RELATION('c', 1), RELATION('c', 2),
				RELATION('d', 0), RELATION('d', 1), RELATION('d', 2),
				RELATION('e', 0), RELATION('e', 1), RELATION('e', 2),
				RELATION('f', 0), RELATION('f', 1), RELATION('f', 2),
				RELATION('g', 0), RELATION('g', 1), RELATION('g', 2),
				RELATION('h', 0), RELATION('h', 1), RELATION('h', 2),
				RELATION('i', 0), RELATION('i', 1), RELATION('i', 2),
				RELATION('j', 0), RELATION('j', 1), RELATION('j', 2),
				RELATION('k', 0), RELATION('k', 1), RELATION('k', 2),
				RELATION('l', 0), RELATION('l', 1), RELATION('l', 2),
				RELATION('m', 0), RELATION('m', 1), RELATION('m', 2),
				RELATION('n', 0), RELATION('n', 1), RELATION('n', 2),
				RELATION('o', 0), RELATION('o', 1), RELATION('o', 2),
				RELATION('p', 0), RELATION('p', 1), RELATION('p', 2),
				RELATION('q', 0), RELATION('q', 1), RELATION('q', 2),
				RELATION('r', 0), RELATION('r', 1), RELATION('r', 2),
				RELATION('s', 0), RELATION('s', 1), RELATION('s', 2),
				RELATION('t', 0), RELATION('t', 1), RELATION('t', 2),
				RELATION('u', 0), RELATION('u', 1), RELATION('u', 2),
				RELATION('v', 0), RELATION('v', 1), RELATION('v', 2),
				RELATION('w', 0), RELATION('w', 1), RELATION('w', 2),
				RELATION('x', 0), RELATION('x', 1), RELATION('x', 2),
				RELATION('y', 0), RELATION('y', 1), RELATION('y', 2),
				RELATION('z', 0), RELATION('z', 1), RELATION('z', 2),
				RELATION('_', 0), RELATION('_', 1)
			),
			NODE(126,
				RELATION('0', 1), RELATION('0', 2),
				RELATION('1', 1), RELATION('1', 2),
				RELATION('2', 1), RELATION('2', 2),
				RELATION('3', 1), RELATION('3', 2),
				RELATION('4', 1), RELATION('4', 2),
				RELATION('5', 1), RELATION('5', 2),
				RELATION('6', 1), RELATION('6', 2),
				RELATION('7', 1), RELATION('7', 2),
				RELATION('8', 1), RELATION('8', 2),
				RELATION('9', 1), RELATION('9', 2),
				RELATION('_', 1), RELATION('_', 2),
				RELATION('A', 1), RELATION('A', 2),
				RELATION('B', 1), RELATION('B', 2),
				RELATION('C', 1), RELATION('C', 2),
				RELATION('D', 1), RELATION('D', 2),
				RELATION('E', 1), RELATION('E', 2),
				RELATION('F', 1), RELATION('F', 2),
				RELATION('G', 1), RELATION('G', 2),
				RELATION('H', 1), RELATION('H', 2),
				RELATION('I', 1), RELATION('I', 2),
				RELATION('J', 1), RELATION('J', 2),
				RELATION('K', 1), RELATION('K', 2),
				RELATION('L', 1), RELATION('L', 2),
				RELATION('M', 1), RELATION('M', 2),
				RELATION('N', 1), RELATION('N', 2),
				RELATION('O', 1), RELATION('O', 2),
				RELATION('P', 1), RELATION('P', 2),
				RELATION('Q', 1), RELATION('Q', 2),
				RELATION('R', 1), RELATION('R', 2),
				RELATION('S', 1), RELATION('S', 2),
				RELATION('T', 1), RELATION('T', 2),
				RELATION('U', 1), RELATION('U', 2),
				RELATION('V', 1), RELATION('V', 2),
				RELATION('W', 1), RELATION('W', 2),
				RELATION('X', 1), RELATION('X', 2),
				RELATION('Y', 1), RELATION('Y', 2),
				RELATION('Z', 1), RELATION('Z', 2),
				RELATION('a', 1), RELATION('a', 2),
				RELATION('b', 1), RELATION('b', 2),
				RELATION('c', 1), RELATION('c', 2),
				RELATION('d', 1), RELATION('d', 2),
				RELATION('e', 1), RELATION('e', 2),
				RELATION('f', 1), RELATION('f', 2),
				RELATION('g', 1), RELATION('g', 2),
				RELATION('h', 1), RELATION('h', 2),
				RELATION('i', 1), RELATION('i', 2),
				RELATION('j', 1), RELATION('j', 2),
				RELATION('k', 1), RELATION('k', 2),
				RELATION('l', 1), RELATION('l', 2),
				RELATION('m', 1), RELATION('m', 2),
				RELATION('n', 1), RELATION('n', 2),
				RELATION('o', 1), RELATION('o', 2),
				RELATION('p', 1), RELATION('p', 2),
				RELATION('q', 1), RELATION('q', 2),
				RELATION('r', 1), RELATION('r', 2),
				RELATION('s', 1), RELATION('s', 2),
				RELATION('t', 1), RELATION('t', 2),
				RELATION('u', 1), RELATION('u', 2),
				RELATION('v', 1), RELATION('v', 2),
				RELATION('w', 1), RELATION('w', 2),
				RELATION('x', 1), RELATION('x', 2),
				RELATION('y', 1), RELATION('y', 2),
				RELATION('z', 1), RELATION('z', 2)),
			NODE()
		);

		if (execute(iLex)) {
			return 'I';
		}
	}

	char GetLex(char* word) {
		FST Ilex(
			(char*)word,
			8,
			NODE(1, RELATION('i', 1)),
			NODE(1, RELATION('n', 2)),
			NODE(1, RELATION('t', 3)),
			NODE(1, RELATION('e', 4)),
			NODE(1, RELATION('g', 5)),
			NODE(1, RELATION('e', 6)),
			NODE(1, RELATION('r', 7)),
			NODE()
		);
		FST Slex(
			(char*)word,
			7,
			NODE(1, RELATION('s', 1)),
			NODE(1, RELATION('t', 2)),
			NODE(1, RELATION('r', 3)),
			NODE(1, RELATION('i', 4)),
			NODE(1, RELATION('n', 5)),
			NODE(1, RELATION('g', 6)),
			NODE()
		);
		if (execute(Ilex) || execute(Slex))
			return LEX_INTEGER;
		FST Flex(
			(char*)word,
			9,
			NODE(1, RELATION('f', 1)),
			NODE(1, RELATION('u', 2)),
			NODE(1, RELATION('n', 3)),
			NODE(1, RELATION('c', 4)),
			NODE(1, RELATION('t', 5)),
			NODE(1, RELATION('i', 6)),
			NODE(1, RELATION('o', 7)),
			NODE(1, RELATION('n', 8)),
			NODE());
		if (execute(Flex))
			return LEX_FUNCTION;
		FST Dlex(
			(char*)word,
			8,
			NODE(1, RELATION('d', 1)),
			NODE(1, RELATION('e', 2)),
			NODE(1, RELATION('c', 3)),
			NODE(1, RELATION('l', 4)),
			NODE(1, RELATION('a', 5)),
			NODE(1, RELATION('r', 6)),
			NODE(1, RELATION('e', 7)),
			NODE()
		);
		if (execute(Dlex))
			return LEX_DECLARE;
		FST Plex(
			(char*)word,
			6,
			NODE(1, RELATION('p', 1)),
			NODE(1, RELATION('r', 2)),
			NODE(1, RELATION('i', 3)),
			NODE(1, RELATION('n', 4)),
			NODE(1, RELATION('t', 5)),
			NODE()
		);
		if (execute(Plex))
			return LEX_PRINT;
		FST Mlex(
			(char*)word,
			5,
			NODE(1, RELATION('m', 1)),
			NODE(1, RELATION('a', 2)),
			NODE(1, RELATION('i', 3)),
			NODE(1, RELATION('n', 4)),
			NODE()
		);
		if (execute(Mlex))
			return LEX_MAIN;
	}
	bool check_int(char* word) {
		FST integer_au(
			word,
			8,
			NODE(1, RELATION('i', 1)),
			NODE(1, RELATION('n', 2)),
			NODE(1, RELATION('t', 3)),
			NODE(1, RELATION('e', 4)),
			NODE(1, RELATION('g', 5)),
			NODE(1, RELATION('e', 6)),
			NODE(1, RELATION('r', 7)),
			NODE()
		);
		if (execute(integer_au)) {
			return true;
		}
		return false;
	}


	void GetLexOrID(In::IN in, LT::LexTable& lextable, IT::IdTable& idtable) {
		char* ret = new char[LT_MAXSIZE];
		int symbols = 0;
		const char constlex[] = ";,{}()=";
		const char vlex[] = "+-*////";
		int str = 1;
		int id = 0;
		ret[symbols++] = (str / 10) + '0';
		ret[symbols++] = (str % 10) + '0';
		ret[symbols++] = ' ';
		str = 0;
		bool param = false, func = false;
		for (int i = 0; i < in.count_words - 1; i++) {
			if (in.words[i][0] == (unsigned char)' ' || in.words[i][0] == (unsigned char)'\0') {
				continue;
			}
			bool flag = false;
			for (int j = 0; j < 7; j++) {
				if (in.words[i][0] == constlex[j]) {
					ret[symbols++] = in.words[i][0];
					LT::Add(lextable, { (char)in.words[i][0], str, NULL });
					flag = true;
					break;
				}
				else if (in.words[i][0] == vlex[j]) {
					ret[symbols++] = 'v';
					LT::Add(lextable, { 'v', str, NULL });
					flag = true;
					break;
				}
			}
			if (in.words[i][0] == '|') {
				ret[symbols++] = '\n';
				str++;
				ret[symbols++] = (str / 10) + '0';
				ret[symbols++] = (str % 10) + '0';
				ret[symbols++] = ' ';
				continue;
			}
			if (flag) {
				switch (in.words[i][0])
				{
				case '(':
					param = true;
					break;
				case ')':
					param = false;
					break;
				case '{':
					func = true;
					break;
				case '}':
					func = false;
					break;
				}
				continue;
			}
			
			IT::IDDATATYPE datatypeIT;
			switch (GetLex((char*)in.words[i]))
			{
			case LEX_INTEGER:
				LT::Add(lextable, { 't', str, NULL });
				ret[symbols++] = 't';
				datatypeIT = check_int((char*)in.words[i]) ? IT::INT : IT::STR;
				do {
					i++;
					if (in.words[i][0] == '|') {
						ret[symbols++] = '\n';
						str++;
						ret[symbols++] = (str / 10) + '0';
						ret[symbols++] = (str % 10) + '0';
						ret[symbols++] = ' ';
					}
				} while (in.words[i][0] == (unsigned char)' ' || in.words[i][0] == (unsigned char)'\0' || in.words[i][0] == '|');
				
				if (GetLex((char*)in.words[i]) == LEX_FUNCTION)
				{
					LT::Add(lextable, { 'f', str, NULL });
					ret[symbols++] = 'f';
					do {
						i++;
						if (in.words[i][0] == '|') {
							ret[symbols++] = '\n';
							str++;
							ret[symbols++] = (str / 10) + '0';
							ret[symbols++] = (str % 10) + '0';
							ret[symbols++] = ' ';
						}
					} while (in.words[i][0] == (unsigned char)' ' || in.words[i][0] == (unsigned char)'\0' || in.words[i][0] == '|');
					if (GetID((char*)in.words[i]) == 'I') {
						LT::Add(lextable, { 'i', str, idtable.size });
						IT::Add(idtable, { lextable.size - 1,(char*)in.words[i], datatypeIT, IT::P });
						ret[symbols++] = 'i';
					}
					else {
						ret[symbols] = '\0';
							throw ERROR_THROW_LEX(118, str, in.words[i], (unsigned char*)ret);
					}
				}
				else if (GetID((char*)in.words[i]) == 'I') {
					LT::Add(lextable, { 'i', str, idtable.size });
					IT::Add(idtable, { lextable.size - 1,(char*)in.words[i], datatypeIT, IT::P });
					ret[symbols++] = 'i';
				}
				else {
					ret[symbols] = '\0';
						throw ERROR_THROW_LEX(118, str, in.words[i], (unsigned char*)ret);
				}
				continue;
				break;
			case LEX_DECLARE:
				LT::Add(lextable, { 'd', str, NULL });
				ret[symbols++] = 'd';
				do {
					i++;
					if (in.words[i][0] == '|') {
						ret[symbols++] = '\n';
						str++;
						ret[symbols++] = (str / 10) + '0';
						ret[symbols++] = (str % 10) + '0';
						ret[symbols++] = ' ';
					}
				} while (in.words[i][0] == (unsigned char)' ' || in.words[i][0] == (unsigned char)'\0' || in.words[i][0] == '|');


				if (GetLex((char*)in.words[i]) == LEX_INTEGER) {
					LT::Add(lextable, { 't', str, NULL });
					ret[symbols++] = 't';
					datatypeIT = check_int((char*)in.words[i]) ? IT::INT : IT::STR;
					do {
						i++;
						if (in.words[i][0] == '|') {
							ret[symbols++] = '\n';
							str++;
							ret[symbols++] = (str / 10) + '0';
							ret[symbols++] = (str % 10) + '0';
							ret[symbols++] = ' ';
						}
					} while (in.words[i][0] == (unsigned char)' ' || in.words[i][0] == (unsigned char)'\0' || in.words[i][0] == '|');

					if (GetLex((char*)in.words[i]) == LEX_FUNCTION)
					{
						LT::Add(lextable, { 'f', str, NULL });
						ret[symbols++] = 'f';
						do {
							i++;
							if (in.words[i][0] == '|') {
								ret[symbols++] = '\n';
								str++;
								ret[symbols++] = (str / 10) + '0';
								ret[symbols++] = (str % 10) + '0';
								ret[symbols++] = ' ';
							}
						} while (in.words[i][0] == (unsigned char)' ' || in.words[i][0] == (unsigned char)'\0' || in.words[i][0] == '|');
						if (GetID((char*)in.words[i]) == 'I') {
							LT::Add(lextable, { 'i', str, idtable.size });
							IT::Add(idtable, { lextable.size - 1,(char*)in.words[i], datatypeIT, IT::V });
							ret[symbols++] = 'i';
						}
						else {
							ret[symbols] = '\0';

							throw ERROR_THROW_LEX(118, str, in.words[i], (unsigned char*)ret);
						}
					}
					else if (GetID((char*)in.words[i]) == 'I') {
						LT::Add(lextable, { 'i', str, idtable.size });
						IT::Add(idtable, { lextable.size - 1,(char*)in.words[i], datatypeIT, IT::V });
						ret[symbols++] = 'i';
					}
					else {
						ret[symbols] = '\0';

						throw ERROR_THROW_LEX(118, str, in.words[i], (unsigned char*)ret);
					}

				}
				
				continue;
				break;

			case LEX_RETURN:
				LT::Add(lextable, { 'r', str, NULL });
				ret[symbols++] = 'r';
				continue;
				break;

			case LEX_PRINT:
				LT::Add(lextable, { 'p', str, NULL });
				ret[symbols++] = 'p';
				continue;
				break;

			case LEX_MAIN:
				LT::Add(lextable, { 'm', str, NULL });
				ret[symbols++] = 'm';
				continue;
				break;


			default:
				break;
			}

			IT::IDDATATYPE datatype;
			IT::IDTYPE type;
			IT::Entry entry;
			int j = 0;
			int l = -1;
			char* tmp_literal_name = new char[ID_MAXSIZE];
			char* LiteralSTR = (char*)"LitSTR";
			char* LiteralINT = (char*)"LitINT";
			int g = 0;
			int tmp_lit_number = id;
			char tmp_number[10];
			int h = 0;

			switch (GetID((char*)in.words[i]))
			{
			case 's':
				l = idtable.size;
				for (int k = idtable.size - 1; k >= 0; k--) {
					if (IT::GetEntry(idtable, k).iddatatype == IT::STR && IT::GetEntry(idtable, k).idtype == IT::L && 
						!strcmp(IT::GetEntry(idtable, k).value.vstr.str, (char*)in.words[i]))
					{
						l = k;
						break;
					}
				}
				LT::Add(lextable, { 'l', str, l });


				if (l == idtable.size) {
					datatype = IT::STR;
					type = IT::L; 
					
					while (LiteralSTR[g] != '\0') {
						tmp_literal_name[g] = LiteralSTR[g];
						g++;
					}
					if (tmp_lit_number == 0) {
						tmp_literal_name[g++] = '0';
					}

					else {
						while (tmp_lit_number > 0) {
							tmp_number[h++] = tmp_lit_number % 10 + '0';
							tmp_lit_number /= 10;
						}
						tmp_number[h] = '\0';
						h--;
						while (h >= 0) {

							tmp_literal_name[g++] = tmp_number[h--];
						}
					}

					tmp_literal_name[g] = '\0';
					id++;

					entry = { lextable.size - 1, tmp_literal_name , datatype, type};
					entry.value.vstr = { j - 1, ((char*)in.words[i])};
					IT::Add(idtable, entry);
				}
				ret[symbols++] = 'l';
				//delete[] w
				
				continue;
				break;
			case 'i':
				l = idtable.size;
				for (int k = idtable.size - 1; k >= 0; k--) {
					if (IT::GetEntry(idtable, k).iddatatype == IT::STR && IT::GetEntry(idtable, k).idtype == IT::L &&
						!strcmp(IT::GetEntry(idtable, k).value.vstr.str, (char*)in.words[i])) {
						l = k;
						break;
					}
				}
				LT::Add(lextable, { 'l', str, l });
				if (l == idtable.size) {
					datatype = IT::INT;
					type = IT::L;
					while (LiteralINT[g] != '\0') {
						tmp_literal_name[g] = LiteralINT[g];
						g++;
					}
					if (tmp_lit_number == 0) {
						tmp_literal_name[g++] = '0';
					}

					else {
						while (tmp_lit_number > 0) {
							tmp_number[h++] = tmp_lit_number % 10 + '0';
							tmp_lit_number /= 10;
						}
						tmp_number[h] = '\0';
						h--;
						while (h >= 0) {

							tmp_literal_name[g++] = tmp_number[h--];
						}
					}

					tmp_literal_name[g] = '\0';
					id++;
					IT::Add(idtable, { lextable.size - 1, tmp_literal_name , datatype, type, atoi((char*)in.words[i])});
				}
				ret[symbols++] = 'l';
				//delete[] w
				continue;
				break;
			case 'I':
				for (int k = idtable.size - 1; k >= 0; k--) {
					if (!strcmp(IT::GetEntry(idtable, k).id,(char*)in.words[i])) {
						l = k;
						break;
					}
				}
				LT::Add(lextable, { 'i', str, l});
				ret[symbols++] = 'i';
				//delete[] w
				continue;
				break;
			}

			ret[symbols] = '\0';
			throw ERROR_THROW_LEX(118, str, in.words[i], (unsigned char*)ret);
		}

		delete[] ret;

	}
}
