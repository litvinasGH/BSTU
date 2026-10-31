#include <iostream>
//KachinsaksVatslovas2006
//4b 61 63 68 69 6e 73 61 6b 73 56 61 74 73 6c 6f 76 61 73 32 30 30 36 00(Windows-1251)
//004b 0061 0063 0068 0069 006e 0073 0061 006b 0073 0056 0061 0074 0073 006c 006f 0076 0061 0073 0032 0030 0030 0036 0000(UTF-8)
//004b 0061 0063 0068 0069 006e 0073 0061 006b 0073 0056 0061 0074 0073 006c 006f 0076 0061 0073 0032 0030 0030 0036 0000(UTF-16)

//КачинскасВацловасВацловович2006
//ca e0 f7 e8 ed f1 ea e0 f1 c2 e0 f6 eb ee e2 e0 f1 c2 e0 f6 eb ee e2 ee e2 e8 f7 32 30 30 36 00(Windows-1251)
//d09a d0b0 d187 d0b8 d0bd d181 d0ba d0b0 d181 d092 d0b0 d186 d0bb d0be d0b2 d0b0 d181 d092 d0b0 d186 d0bb d0be d0b2 d0be d0b2 d0b8 d187 32 30 30 36 00(UTF-8)
//041a 0430 0447 0438 043d 0441 043a 0430 0441 0412 0430 0446 043b 043e 0432 0430 0441 0412 0430 0446 043b 043e 0432 043e 0432 0438 0447 0032 0030 0030 0036 0000(UTF-16)

//Качинскас2006Vatslovas
//ca e0 f7 e8 ed f1 ea e0 f1 32 30 30 36 56 61 74 73 6c 6f 76 61 73 0a 00(Windows-1251)
//d09a d0b0 d187 d0b8 d0bd d181 d0ba d0b0 d181 32 30 30 36 56 61 74 73 6c 6f 76 61 73 00(UTF-8)
//041a 0430 0447 0438 043d 0441 043a 0430 0441 0032 0030 0030 0036 0056 0061 0074 0073 006c 006f 0076 0061 0073 0000(UTF-16)

int main()
{
	int number = 0x12345678;
	char hello[] = "Hello, ";
	char lfie[] = "KachinsaksVatslovas2006";
	char rfie[] = "КачинскасВацловасВацловович2006";
	char lr[] = "Качинскас2006Vatslovas";

	wchar_t Lfie[] = L"KachinsaksVatslovas2006";
	wchar_t Rfie[] = L"КачинскасВацловасВацловович2006";
	wchar_t LR[] = L"Качинскас2006Vatslovas";

	std::cout << hello << lfie << std::endl;
	
	return 0;
}