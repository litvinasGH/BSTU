#pragma once
#define LEXEMA_FIXSIZE 1
#define	LT_MAXSIZE 4096
#define LT_TI_NULLIDX 0XFFFFFFFF
#define LEX_INTEGER 't' //int
#define LEX_STRING 't' //str
#define LEX_ID 'i'//id
#define LEX_LITERAL 'l'//lit
#define LEX_FUNCTION 'f'//func
#define LEX_DECLARE 'd'//declare
#define LEX_RETURN 'r'//ret
#define LEX_PRINT 'p'//print
#define LEX_MAIN 'm'//main
#define LEX_SEMICOLON ';'
#define LEX_COMMA ','
#define LEX_LEFTBRACE '{'
#define LEX_BRACELET '}'
#define LEX_LEFTTHESIS '('
#define LEX_RIGHTTHESIS ')'
#define LEX_PLUS 'v'//+
#define LEX_MINUS 'v'//-
#define LEX_STAR 'v'//*
#define LEX_DIRSLASH 'v'///

namespace LT
{
	struct Entry
	{
		char lexema;
		int sn;
		int idxTI;
	};
	struct LexTable
	{
		int maxsize;
		int size;
		Entry* table;
	};
	LexTable Create(int size);
	void Add(LexTable& lextable, Entry entry);
	Entry GetEntry(LexTable& lextable, int n);
	void Delete(LexTable& lextable);
}