#pragma once
#include <cstddef>
namespace FST {

	struct RELATION {
		char symbol;
		short nnode;
		RELATION(
			char c = 0x00,
			short ns = NULL
		);
	};

	struct NODE {
		short n_relation;
		RELATION* relations;
		NODE();
		NODE(
			short n,
			RELATION rel, ...
		);
	};
	struct FST {
		const char* string;
		short position;
		short nstates;
		NODE* nodes;
		short* rstates;
		FST(
			const char* s,
			short ns,
			NODE n, ...
		);
	};
	bool execute(
		FST& fst
	);
}