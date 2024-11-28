#include"IT.h"
#include "Error.h"
#include"stdafx.h"



namespace IT
{
	IdTable Create(int size)
	{
		if (size > IT_MAXSIZE)
		{
			throw ERROR_THROW(116);
		}
		IdTable tabl;
		tabl.maxsize = size;
		tabl.size = 0;
		tabl.table = new Entry[size];
		return tabl;
	}

	Entry GetEntry(IdTable& idtable, int n)
	{
		return idtable.table[n];
	}

	void Add(IdTable& idTable, Entry entry)
	{
		if (idTable.size > IT_MAXSIZE)
		{
			throw ERROR_THROW(120);
		}
		idTable.table[idTable.size] = entry;
		idTable.size++;
	}

	void Delete(IdTable& idtable) {
		delete[] idtable.table;
		idtable.size = 0;
		idtable.maxsize = 0;
	}
	
}