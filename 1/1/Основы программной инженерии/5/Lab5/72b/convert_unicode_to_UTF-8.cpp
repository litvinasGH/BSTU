#include "convert_unicode_to_UTF-8.h"

void convertUnicodeToUTF8(char *code_symbol) {
	char convert_code[33];
	if (strlen(code_symbol)<8) {
		convert_code[0] = '0';
		for (int i = 1; i <= strlen(code_symbol)+1; i++) {
			convert_code[i] = *(code_symbol + (i - 1));
		}
	}
	else {
		int i, n;
		for (i = 0; i <= ceil(strlen(code_symbol) / 8); i++) {
			convert_code[i] = '1';
		}
		convert_code[i] = '0';
		i++;
		int len = strlen(code_symbol), difference = i;
		for (; i <= len + difference; i++) {
			convert_code[i] = *(code_symbol + (i - difference));
			if (i == 7) {
				convert_code[i + 1] = '1';
				convert_code[i + 2] = '0';
				difference += 2;
				i += 2;
			}
		}
	}
	for (int j = 0; j <= strlen(convert_code); j++) {
		*(code_symbol + j) = convert_code[j];
	}
}