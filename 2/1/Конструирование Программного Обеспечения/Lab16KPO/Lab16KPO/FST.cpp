#include "FST.h"
#include <iostream>
#include <stdarg.h>

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

}