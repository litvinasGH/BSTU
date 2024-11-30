#pragma once

namespace Call {
	int _cdecl cdeml(int x, int y, int z);
	int _stdcall cstd(int& x, int y, int z);
	int _fastcall cfst(int x, int y, int z, int s);
}
