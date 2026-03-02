#pragma once
#define ID_MAXSIZE 10
#define IT_MAXSIZE 4096
#define IT_INT_DEFAULT 0x00000000
#define IT_STR_DEFAULT 0x00
#define IT_NULLIDX 0xffffffff
#define IT_STR_MAXSIZE 255
namespace IT
{
	enum IDDATATYPE { INT = 1, STR = 2 };
	enum IDTYPE { V = 1, F = 2, P = 3, L = 4 };

	struct Entry		
	{
		int			idxfirstLE;			
		char		*id;		
		IDDATATYPE	iddatatype;			
		IDTYPE		idtype;				
		union
		{
			int		vint;				
			struct
			{
				int len;						
				char *str;	
			}	vstr;			
		} value;	
	};
	struct IdTable			
	{
		int maxsize;		
		int size;			
		Entry* table;		
	};
	IdTable Create(		
		int size		
	);

	Entry GetEntry(			
		IdTable& idtable,	
		int n				
	);

	void Add(IdTable& idTable, Entry entry);
	void Delete(IdTable& idtable);	
}