#pragma once

typedef short GRBALPHABET;
namespace GRB
{
	struct Rule							//правило в грамматике √рейбах
	{
		GRBALPHABET nn;							//нетерминалы (левый символ правила) < 0
		int iderror;							//идентификатор диагностического сообщени€
		short size;								//количество цепочек - правых частей правила
		struct Chain							//цепочка(права€ часть правила)
		{
			short size;						//длина цепочки
			GRBALPHABET* nt;				//цепочка терминалов (>0) и нетермеминалов (<0) 
			Chain()
			{
				this->size = 0; 				//количество символов в цепочке
				this->nt = 0;					//символы (терминал или нетерминал)
			};
			Chain(short psize, GRBALPHABET s, ...);						//кол-во символов в цепочке/символы(терминалы и нетерминалы)
			char* getCChain(char* b);									//получить правую сторону правила
			static GRBALPHABET T(char t) { return GRBALPHABET(t); };	//терминал
			static GRBALPHABET N(char n) { return -GRBALPHABET(n); };	//не терминал
			static bool isT(GRBALPHABET s) { return s > 0; };			//терминал?
			static bool isN(GRBALPHABET s) { return !isT(s); };			//не терминал?
			static char alphabet_to_char(GRBALPHABET s)					//GRBALPHABET->char (обращение по указателю к полю символа)
			{
				return isT(s) ? char(s) : char(-s);
			};
		}*chains;			//массив цепочек - правых частей правила
		Rule()
		{
			this->nn = 0x00;
			this->size = 0;
		}
		Rule(GRBALPHABET pnn, int iderroe, short psize, Chain c, ...);
		//(нетерминал(< 0); идентификатор диагностического сообщ - €; кол - во цепочек - правых частей правила; множество цепочек - правых частей правила)
		char* getCRule(char* b, short nchain); //получить парвило в виде N->цепочка (дл€ распечатки) (буфер;номер цепочки(правой части) в правиле)
		short getNextChain(GRBALPHABET t, Rule::Chain& pchain, short j); //получить следующую за j подход€щую цепочку, вернуть еЄ номер или -1 (первый символ цепочки;возвращаема€ цепочка; номер цепочки)
	};
	struct Greibach			//грамматика √рейбах
	{
		short size;						//количество правил
		GRBALPHABET startN;				//стартовый символ
		GRBALPHABET stbottomT;			//дно стека
		Rule* rules;					//множество правил
		Greibach() { this->size = 0; this->startN = 0; this->stbottomT = 0; this->rules = 0; };
		Greibach(GRBALPHABET pstartN, GRBALPHABET pstbottomT, short psize, Rule r, ...);
		short getRule(GRBALPHABET pnn, Rule& prule);
		Rule getRule(short n); //получить правиль по номеру
	};
	Greibach getGreibach(); //получить граматику
}
