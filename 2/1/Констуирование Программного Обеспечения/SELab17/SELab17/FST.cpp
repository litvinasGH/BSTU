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
	NODE::NODE(short n, RELATION re1, ...) {
		n_relation = n;
		relations = new RELATION[n];

		va_list args;
		va_start(args, re1);
		relations[0] = re1;

		for (int i = 1; i < n; i++) {
			relations[i] = va_arg(args, RELATION);
		}

		va_end(args);
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
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
				RELATION('�', 1), RELATION('�', 2), RELATION('�', 1), RELATION('�', 2),
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
			FST Ilex(
				(char*)in.words[i],
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
				(char*)in.words[i],
				7,
				NODE(1, RELATION('s', 1)),
				NODE(1, RELATION('t', 2)),
				NODE(1, RELATION('r', 3)),
				NODE(1, RELATION('i', 4)),
				NODE(1, RELATION('n', 5)),
				NODE(1, RELATION('g', 6)),
				NODE()
			);
			if (execute(Ilex)) {
				LT::Add(lextable, { 't', str, NULL });
				ret[symbols++] = 't';
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
				FST Flex(
					(char*)in.words[i],
					9,
					NODE(1, RELATION('f', 1)),
					NODE(1, RELATION('u', 2)),
					NODE(1, RELATION('n', 3)),
					NODE(1, RELATION('c', 4)),
					NODE(1, RELATION('t', 5)),
					NODE(1, RELATION('i', 6)),
					NODE(1, RELATION('o', 7)),
					NODE(1, RELATION('n', 8)),
					NODE()
				);
				if (execute(Flex))
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
						IT::Add(idtable, { lextable.size - 1,(char*)in.words[i], IT::INT, IT::P });
						ret[symbols++] = 'i';
					}
					else {
						ret[symbols] = '\0';

						throw ERROR_THROW_LEX(118, str, in.words[i], (unsigned char*)ret);
					}
				}
				else if (GetID((char*)in.words[i]) == 'I') {
					LT::Add(lextable, { 'i', str, idtable.size });
					IT::Add(idtable, { lextable.size - 1,(char*)in.words[i], IT::INT, IT::P });
					ret[symbols++] = 'i';
				}
				else {
					ret[symbols] = '\0';

					throw ERROR_THROW_LEX(118, str, in.words[i], (unsigned char*)ret);
				}
				continue;
			}
			else if (execute(Slex)) {
				LT::Add(lextable, { 't', str, NULL });
				ret[symbols++] = 't';
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
				FST Flex(
					(char*)in.words[i],
					9,
					NODE(1, RELATION('f', 1)),
					NODE(1, RELATION('u', 2)),
					NODE(1, RELATION('n', 3)),
					NODE(1, RELATION('c', 4)),
					NODE(1, RELATION('t', 5)),
					NODE(1, RELATION('i', 6)),
					NODE(1, RELATION('o', 7)),
					NODE(1, RELATION('n', 8)),
					NODE()
				);
				if (execute(Flex))
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
						IT::Add(idtable, { lextable.size - 1,(char*)in.words[i], IT::STR, IT::P });
						ret[symbols++] = 'i';
					}
					else {
						ret[symbols] = '\0';

						throw ERROR_THROW_LEX(118, str, in.words[i], (unsigned char*)ret);
					}
				}
				else if (GetID((char*)in.words[i]) == 'I') {
					LT::Add(lextable, { 'i', str, idtable.size });
					IT::Add(idtable, { lextable.size - 1,(char*)in.words[i], IT::STR, IT::P });
					ret[symbols++] = 'i';
				}
				else
				{
					ret[symbols] = '\0';

					throw ERROR_THROW_LEX(118, str, in.words[i], (unsigned char*)ret);
				}
				continue;
			}




				FST Dlex(
					(char*)in.words[i],
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
				if (execute(Dlex)) {
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
					FST Ilex1(
						(char*)in.words[i],
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
					FST Slex1(
						(char*)in.words[i],
						7,
						NODE(1, RELATION('s', 1)),
						NODE(1, RELATION('t', 2)),
						NODE(1, RELATION('r', 3)),
						NODE(1, RELATION('i', 4)),
						NODE(1, RELATION('n', 5)),
						NODE(1, RELATION('g', 6)),
						NODE()
					);
					if (execute(Ilex1)) {
						LT::Add(lextable, { 't', str, NULL });
						ret[symbols++] = 't';
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
						FST Flex(
							(char*)in.words[i],
							9,
							NODE(1, RELATION('f', 1)),
							NODE(1, RELATION('u', 2)),
							NODE(1, RELATION('n', 3)),
							NODE(1, RELATION('c', 4)),
							NODE(1, RELATION('t', 5)),
							NODE(1, RELATION('i', 6)),
							NODE(1, RELATION('o', 7)),
							NODE(1, RELATION('n', 8)),
							NODE()
						);
						if (execute(Flex))
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
								IT::Add(idtable, { lextable.size - 1,(char*)in.words[i], IT::INT, IT::V });
								ret[symbols++] = 'i';
							}
							else {
								ret[symbols] = '\0';

								throw ERROR_THROW_LEX(118, str, in.words[i], (unsigned char*)ret);
							}
						}
						else if (GetID((char*)in.words[i]) == 'I') {
							LT::Add(lextable, { 'i', str, idtable.size });
							IT::Add(idtable, { lextable.size - 1,(char*)in.words[i], IT::INT, IT::V });
							ret[symbols++] = 'i';
						}
						else {
							ret[symbols] = '\0';

							throw ERROR_THROW_LEX(118, str, in.words[i], (unsigned char*)ret);
						}
						
					}
					else if (execute(Slex1)) {
						LT::Add(lextable, { 't', str, NULL });
						ret[symbols++] = 't';
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
						FST Flex(
							(char*)in.words[i],
							9,
							NODE(1, RELATION('f', 1)),
							NODE(1, RELATION('u', 2)),
							NODE(1, RELATION('n', 3)),
							NODE(1, RELATION('c', 4)),
							NODE(1, RELATION('t', 5)),
							NODE(1, RELATION('i', 6)),
							NODE(1, RELATION('o', 7)),
							NODE(1, RELATION('n', 8)),
							NODE()
						);
						if (execute(Flex))
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
								IT::Add(idtable, { lextable.size - 1,(char*)in.words[i], IT::STR, IT::V });
								ret[symbols++] = 'i';
							}
							else {
								ret[symbols] = '\0';

								throw ERROR_THROW_LEX(118, str, in.words[i], (unsigned char*)ret);
							}
						}
						else if (GetID((char*)in.words[i]) == 'I') {
							LT::Add(lextable, { 'i', str, idtable.size });
							IT::Add(idtable, { lextable.size - 1,(char*)in.words[i], IT::STR, IT::V });
							ret[symbols++] = 'i';
						}
						else
						{
							ret[symbols] = '\0';

							throw ERROR_THROW_LEX(118, str, in.words[i], (unsigned char*)ret);
						}
						
					}
					continue;
				}

				FST Rlex(
					(char*)in.words[i],
					7,
					NODE(1, RELATION('r', 1)),
					NODE(1, RELATION('e', 2)),
					NODE(1, RELATION('t', 3)),
					NODE(1, RELATION('u', 4)),
					NODE(1, RELATION('r', 5)),
					NODE(1, RELATION('n', 6)),
					NODE()
				);
				if (execute(Rlex)) {
					LT::Add(lextable, { 'r', str, NULL });
					ret[symbols++] = 'r';
					continue;
				}


				FST Plex(
					(char*)in.words[i],
					6,
					NODE(1, RELATION('p', 1)),
					NODE(1, RELATION('r', 2)),
					NODE(1, RELATION('i', 3)),
					NODE(1, RELATION('n', 4)),
					NODE(1, RELATION('t', 5)),
					NODE()
				);
				if (execute(Plex)) {
					LT::Add(lextable, { 'p', str, NULL });
					ret[symbols++] = 'p';
					continue;
				}

				FST Mlex(
					(char*)in.words[i],
					5,
					NODE(1, RELATION('m', 1)),
					NODE(1, RELATION('a', 2)),
					NODE(1, RELATION('i', 3)),
					NODE(1, RELATION('n', 4)),
					NODE()
				);
				if (execute(Mlex)) {
					LT::Add(lextable, { 'm', str, NULL });
					ret[symbols++] = 'm';
					continue;
				}

				char* w = new char[sizeof(in.words[i]) / sizeof(char)];
				IT::IDDATATYPE datatype;
				IT::IDTYPE type;
				IT::Entry entry;
				int j = 0;
				int l = -1;
				char Lexid[20];
				switch (GetID((char*)in.words[i]))
				{
				case 's':
					for (int k = 1; in.words[i][k] != '\''; k++) {
						w[j++] = in.words[i][k];
					}
					w[j++] = '\0';
					l = idtable.size;
					for (int k = idtable.size - 1; k >= 0; k--) {
						if (IT::GetEntry(idtable, k).iddatatype == IT::STR && IT::GetEntry(idtable, k).idtype == IT::L &&
							!strcmp(IT::GetEntry(idtable, k).value.vstr.str, w)) {
							l = k;
							break;
						}
					}
					LT::Add(lextable, { 'l', str, l });
					if (l == idtable.size) {
						datatype = IT::STR;
						type = IT::L;
						entry = { lextable.size - 1, , datatype, type};
						entry.value.vstr = { j - 1, w };
						IT::Add(idtable, entry);
					}
					ret[symbols++] = 'l';
					continue;
					break;
				case 'i':
					l = idtable.size;
					for (int k = idtable.size - 1; k >= 0; k--) {
						if (IT::GetEntry(idtable, k).iddatatype == IT::STR && IT::GetEntry(idtable, k).idtype == IT::L &&
							!strcmp(IT::GetEntry(idtable, k).value.vstr.str, w)) {
							l = k;
							break;
						}
					}
					LT::Add(lextable, { 'l', str, l });
					if (l == idtable.size) {
						datatype = IT::INT;
						type = IT::L;
						IT::Add(idtable, { lextable.size - 1, , datatype, type, atoi((char*)in.words[i])});
					}
					ret[symbols++] = 'l';
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
					continue;
					break;
				}


				ret[symbols] = '\0';

				throw ERROR_THROW_LEX(118, str, in.words[i], (unsigned char*)ret);
			}
			ret[symbols] = '\0';

		}
	}
