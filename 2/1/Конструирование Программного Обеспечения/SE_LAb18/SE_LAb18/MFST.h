#pragma once
#include "stdafx.h"
#include "Log.h"
#include "GRB.h"
//#include "Rules.h"
//#include "GRB.h"

class my_stack_SHORT : public stack<short> {
public:
	using std::stack<short>::c; // доступ к базовому классу
};

#define MFST_DIAGN_MAXSIZE 2 * ERROR_MAXSIZE_MESSAGE
#define MFST_DIAGN_NUMBER 3

static int FST_TRACE_n = -1;
static char rbuf[205], sbuf[205], lbuf[1024];


#ifdef FULLINFO
#define MFST_TRACE_START cout << setw(4) << left << "Этап" << ":"\
                                << setw(20) << left << " Правило"\
                                << setw(30) << left <<" Входная лента"\
                                << left <<" Стек\n";

#define MFST_TRACE1         cout << setw(4) << left << ++FST_TRACE_n << ": "\
                                    << setw(20) << left << rule.getCRule(rbuf,nrulechain)\
                                    << setw(30) << left << getCLenta(lbuf,lenta_position)\
                                    << setw(20) << left << getCSt(sbuf)\
                                    << endl;

#define MFST_TRACE2         cout << setw(4) << left << FST_TRACE_n << ": "\
                                    << setw(20) << left << " "\
                                    << setw(30) << left << getCLenta(lbuf,lenta_position)\
                                    << setw(20) << left << getCSt(sbuf)\
                                    << endl;

#define MFST_TRACE3         cout << setw(4) << left << ++FST_TRACE_n << ": "\
                                    << setw(20) << left << " "\
                                    << setw(30) << left << getCLenta(lbuf,lenta_position)\
                                    << setw(20) << left << getCSt(sbuf)\
                                    << endl;

#define MFST_TRACE4(c)      cout << setw(4) << left << ++FST_TRACE_n << ": "<< setw(20) << left << c << endl;
#define MFST_TRACE5(c)      cout << setw(4) << left << FST_TRACE_n << ": "<< setw(20) << left << c << endl;
#define MFST_TRACE6(c,k)    cout << setw(4) << left << FST_TRACE_n << ": "<< setw(20) << left << c << k << endl;
#else
#define MFST_TRACE_START
#define MFST_TRACE1
#define MFST_TRACE2
#define MFST_TRACE3
#define MFST_TRACE4(c)
#define MFST_TRACE5(c)
#define MFST_TRACE6(c,k)
#endif

#define MFST_TRACE7         cout << setw(4) << left << state.lenta_position << ": "\
                                    << setw(20) << left << rule.getCRule(rbuf,state.nrulechain) << endl;

typedef my_stack_SHORT MFSTSTSTACK;

namespace MFST
{
	struct MfstState               // состояние автомата для работы
	{
		short lenta_position;       // позиция ленты для работы
		short nrule;               // индекс текущего правила
		short nrulechain;          // индекс текущей цепочки, текущего правила
		MFSTSTSTACK st;            // стек автомата
		MfstState();
		MfstState(short pposition, MFSTSTSTACK pst, short pnrulechain); // (позиция на ленте; стек автомата; индекс текущей цепочки текущего правила)
		MfstState(short pposition, MFSTSTSTACK pst, short pnrule, short pnrulechain); // (позиция на ленте; стек автомата; индекс текущего правила; индекс текущей цепочки текущего правила)

	};
	class my_stack_MfstState : public stack<MfstState> {
	public:
		using std::stack<MfstState>::c;
	};
	struct Mfst                     // млакационнный автомат
	{
		enum RC_STEP {              // типы возвращаемого значения функции step
			NS_OK,                  // найдено правило и цепочка, цепочка добавлена в стек
			NS_NORULE,              // не найдено правило грамматики (ошибка в грамматике)
			NS_NORULECHAIN,         // не найдена подходящая цепочка правила (ошибка в исходном коде)
			NS_ERROR,               // несоответствующий идентификатор грамматики
			TS_OK,                  // текущее состояние ленты == ожидаемое состояние, продвигаем ленту, pop стек
			TS_NOK,                 // текущее состояние ленты != ожидаемое состояние, продвигаем ленту, pop стек
			LENTA_END,              // текущая позиция ленты >= lenta_size
			SURPRISE,               // неожиданный тип возврата (ошибка в step)
		};
		struct MfstDiagnosis        // диагностика
		{
			short lenta_position;    // позиция на ленте
			RC_STEP rc_step;         // тип возвращаемого значения этапа
			short nrule;            // индекс правила
			short nrule_chain;      // индекс цепочки правила
			MfstDiagnosis();        //==
			MfstDiagnosis(short plenta_position, RC_STEP prc_step, short pnrule, short pnrule_chain);
		} diagnosis[MFST_DIAGN_NUMBER]; // массив фиксированных диагностик

		GRBALPHABET* lenta;          // подлежащая (TN/NS) лента (из LEX)
		short lenta_position;        // текущая позиция на ленте
		short nrule;                // индекс текущего правила
		short nrulechain;           // индекс текущей цепочки, текущего правила
		short lenta_size;           // размер ленты
		GRB::Greibach grebach;      // грамматика Грейбах
		LT::LexTable lex;           // таблица для хранения лексем
		MFSTSTSTACK st;             // стек автомата
		my_stack_MfstState storestate; // стек для хранения текущего состояния
		Mfst();
		Mfst(LT::LexTable& plex, GRB::Greibach pgrebach);
		char* getCSt(char* buf);    // получить содержимое стека
		char* getCLenta(char* buf, short pos, short n = 25);  // лента: n символов с pos
		char* getDiagnosis(short n, char* buf, Log::LOG log); // получить n-ую строку диагностики или 0
		bool savestate();            // сохранить текущее состояние автомата
		bool resetstate();          // восстановить текущее состояние автомата
		bool push_chain(GRB::Rule::Chain chain); // поместить цепочку правила в стек
		RC_STEP step();              // выполнить шаг автомата
		bool start(Log::LOG log);               // запустить автомат
		bool savediagnosis(RC_STEP pprc_step); // тип возвращаемого значения этапа
		void printrules(Log::LOG log);          // выводит промежуточную информацию по правилам

		struct Deducation
		{
			short size;
			short* nrules;
			short* nrulechains;
			Deducation()
			{
				size = 0;
				nrules = 0;
				nrulechains = 0;
			};
		} deducation;

		bool savededucation();
	};
}
