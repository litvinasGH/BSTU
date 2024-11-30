#include "Dictionary.h"
#include "stdafx.h"
using namespace Dictionary;

#if (defined(TEST_CREATE_01)+defined(TEST_CREATE_02)+defined(TEST_ADDENTRY_03)+defined(TEST_ADDENTRY_04)+defined(TEST_GETENTRY_05)+defined(TEST_DELENTRY_06)+defined(TEST_UPDENTRY_07)+defined(TEST_UPDENTRY_08)+defined(TEST_DICTIONARY))>1
#error "Определено более одного макроса теста"
#endif
int main() {
	setlocale(LC_ALL, "rus");
	try {
#ifdef TEST_CREATE_01                                                        
		Instance test1 = Create((char*)"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", 5);
#endif
#ifdef TEST_CREATE_02
		Instance test2 = Create((char*)"test", 100000000000);
#endif
#ifdef TEST_ADDENTRY_03
		Instance test3 = Create((char*)"test", 0);
		Entry en_test3 = { 1,"AAAAAA" };
		AddEntry(test3, en_test3);
#endif
#ifdef TEST_ADDENTRY_04 
		Instance test4 = Create((char*)"test", 2);
		Entry en_test4 = { 1,"AAAAAA" };
		AddEntry(test4, en_test4);
		AddEntry(test4, en_test4);
#endif
#ifdef TEST_GETENTRY_05
		Instance test5 = Create((char*)"test", 2);
		Entry en_test5 = { 1,"AAAAAA" };
		AddEntry(test5, en_test5);
		GetEntry(test5, 2);
#endif
#ifdef TEST_DELENTRY_06
		Instance test6 = Create((char*)"test", 2);
		Entry en_test6 = { 1,"AAAAAA" };
		AddEntry(test6, en_test6);
		DelEntry(test6, 2);
#endif
#ifdef TEST_UPDENTRY_07
		Instance test7 = Create((char*)"TEST", 5);
		Entry test_e7 = { 1,"AAAAAAAAA" }, test_upd_e7 = { 2,"AAAAAAAA" };
		AddEntry(test7, test_e7);
		UpdEntry(test7, 3, test_upd_e7);
#endif
#ifdef TEST_UPDENTRY_08
		Instance test8 = Create((char*)"TEST", 5);
		Entry test_e8 = { 1,"AAAAAAAAA" }, test_upd_e8 = { 1,"AAAAAAAA" };
		AddEntry(test8, test_e8);
		UpdEntry(test8, 1, test_upd_e8);
#endif
#ifdef TEST_DICTIONARY
		Instance d1 = Create("Преподаватели", 7);
		Entry e1 = { 1, "Гладкий" }, e2 = { 2, "Веялкин" },
			e3 = { 3, "Смелов" }, e4 = { 4, "Урбанович" }, e5 = { 5, "Наркевич" }, 
			e6 = { 6, "Белодед" }, e7 = { 7, "Барковский" };
		AddEntry(d1, e1);
		AddEntry(d1, e2);
		AddEntry(d1, e3);
		AddEntry(d1, e4);
		AddEntry(d1, e5);
		AddEntry(d1, e6);
		AddEntry(d1, e7);
		Entry ex2 = GetEntry(d1, 4);
		Print(d1);
		DelEntry(d1, 2);
		Print(d1);
		UpdEntry(d1, 3, { 8, "Гурин" });
		Print(d1);

		Instance d2 = { "Cтуденты", 7 };
		Entry s1 = { 1, "Иванов" }, s2 = { 2, "Петров" }, s3 = { 3, "Сидоров" }, s4 = { 4, "Фёдоров" },
			s5 = { 5, "Качинскас" }, s6 = { 6, "Фамилия" }, s7 = { 7, "Филипюк" };
		AddEntry(d2, s1);
		AddEntry(d2, s2);
		AddEntry(d2, s3);
		AddEntry(d2, s4);
		AddEntry(d2, s5);
		AddEntry(d2, s6);
		AddEntry(d2, s7);
		Print(d2);
		UpdEntry(d2, 3, { 8, "Николаев" });
		Print(d2);
		DelEntry(d2, 2);
		Print(d2);
		Delete(d1);
		Delete(d2);
#endif
	}
	catch (char* e) {
		std::cout << e << std::endl;
	}
}