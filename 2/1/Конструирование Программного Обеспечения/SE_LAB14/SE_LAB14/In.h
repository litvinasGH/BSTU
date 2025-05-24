#pragma once
#define IN_MAX_LEN_TEXT 1024*1024	
#define IN_CTDE_ENDL '\n'
//                                                                             0A = /n
//	      0       1      2      3      4      5      6      7      8      9      A      B      C      D      E      F
#define IN_CTDE_TABLE {\
/*0*/	IN::F,	IN::F, IN::F, IN::F, IN::F,	IN::F, IN::F, IN::F, IN::F,	IN::F,  '|' , IN::F, IN::F,	IN::F, IN::F, IN::F,\
/*1*/	IN::F,	IN::F, IN::F, IN::F, IN::F,	IN::F, IN::F, IN::F, IN::F,	IN::F, IN::F, IN::F, IN::F,	IN::F, IN::F, IN::F,\
/*2*/	IN::S,	IN::F, IN::F, IN::F, IN::F,	IN::F, IN::F, IN::F, IN::F,	IN::F, IN::F, IN::F, IN::F,	IN::T, IN::F, IN::F,\
/*3*/	IN::T,	IN::F, IN::T, IN::F, IN::F, IN::F, IN::T, IN::F, IN::F,	IN::F, IN::F, IN::F, IN::F,	IN::F, IN::F, IN::F,\
/*4*/	IN::F,	IN::F, IN::F, IN::F, IN::F,	IN::F, IN::F, IN::F, IN::F,	IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F,\
/*5*/	IN::F,	IN::F, IN::F, IN::F, IN::F,	IN::F, IN::F, IN::F, IN::F,	IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F,\
/*6*/	IN::F,  IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F,\
/*7*/	IN::F,	IN::F, IN::F, IN::F, IN::F,	IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F,\
/*8*/	IN::F,	IN::F, IN::F, IN::F, IN::F,	IN::F, IN::F, IN::F, IN::F,	IN::F, IN::F, IN::F, IN::F,	IN::F, IN::F, IN::F,\
/*9*/	IN::F,	IN::F, IN::F, IN::F, IN::F,	IN::F, IN::F, IN::F, IN::F,	IN::F, IN::F, IN::F, IN::F,	IN::F, IN::F, IN::F,\
/*A*/	IN::F,	IN::F, IN::F, IN::F, IN::F,	IN::F, IN::F, IN::F, IN::F,	IN::F, IN::F, IN::F, IN::F,	IN::F, IN::F, IN::F,\
/*B*/	IN::F,	IN::F, IN::F, IN::F, IN::F,	IN::F, IN::F, IN::F, IN::F,	IN::F, IN::F, IN::F, IN::F,	IN::F, IN::F, IN::F,\
/*C*/    '-' ,  IN::F, IN::T, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F,	IN::F, IN::T, IN::F, IN::F, IN::F, IN::F, IN::F,\
/*D*/	IN::F,  IN::F, IN::F, IN::F, IN::F, IN::I, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F, IN::F,	IN::F, IN::F, IN::F,\
/*E*/   IN::T,	IN::F, IN::T, IN::F, IN::F, IN::F, IN::F, IN::F, IN::T, IN::F, IN::T, IN::T, IN::F, IN::T, IN::T, IN::F,\
/*F*/   IN::F,  IN::T, IN::F, IN::F, IN::F, IN::F, IN::T, IN::T, IN::F,	IN::F, IN::F, IN::F, IN::F,	IN::F, IN::F, IN::F \
}

namespace In
{
	struct IN
	{
		enum { T = 1024, F = 2048, I = 4096 , S = 8192 };
		int size; 

		int lines; 

		int ignor; 

		unsigned char* text; 

		int code[256] = IN_CTDE_TABLE; 
	};
	IN getin(wchar_t infile[]); 
}