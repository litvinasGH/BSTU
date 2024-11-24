#include "bin_to_ten.h"

int convertingBinToTen(char* code_symbol) {
	int result = 0, len = strlen(code_symbol) - 1;
	for (int i = 0; i <= len; i++) {
		result += pow(2, i) * (*(code_symbol + (len - i)) - '0');
	}
	return result;
}