#include"FST.h"
#include<iostream>
using namespace std;


int main()
{
	setlocale(LC_ALL, "rus");
	FST::FST fst1(
		(char*)"iellr",
		7,
		FST::NODE(1, FST::RELATION('i', 1)),
		FST::NODE(2, FST::RELATION('e', 1), FST::RELATION('e', 2)),
		FST::NODE(4, FST::RELATION('c', 3), FST::RELATION('o', 3), FST::RELATION('n', 3), FST::RELATION('l', 4)),
		FST::NODE(2, FST::RELATION('e', 3), FST::RELATION('e', 4)),
		FST::NODE(5, FST::RELATION('c', 3), FST::RELATION('o', 3), FST::RELATION('n', 3), FST::RELATION('e', 5), FST::RELATION('l', 5)),
		FST::NODE(2, FST::RELATION('e', 5), FST::RELATION('r', 6)),
		FST::NODE()
	);
	if (FST::execute(fst1)) {
		cout << "Цепочка " << fst1.string << " распознана" << endl;
	}
	else
	{
		cout << "Цепочка " << fst1.string << " не распознана" << endl;
	}
	FST::FST fst2(
		(char*)"iecelr",
		7,
		FST::NODE(1, FST::RELATION('i', 1)),
		FST::NODE(2, FST::RELATION('e', 1), FST::RELATION('e', 2)),
		FST::NODE(4, FST::RELATION('c', 3), FST::RELATION('o', 3), FST::RELATION('n', 3), FST::RELATION('l', 4)),
		FST::NODE(2, FST::RELATION('e', 3), FST::RELATION('e', 4)),
		FST::NODE(5, FST::RELATION('c', 3), FST::RELATION('o', 3), FST::RELATION('n', 3), FST::RELATION('e', 5), FST::RELATION('l', 5)),
		FST::NODE(2, FST::RELATION('e', 5), FST::RELATION('r', 6)),
		FST::NODE()
	);
	if (FST::execute(fst2)) {
		cout << "Цепочка " << fst2.string << " распознана" << endl;
	}
	else
	{
		cout << "Цепочка " << fst2.string << " не распознана" << endl;
	}
	FST::FST fst3(
		(char*)"ieoelr",
		7,
		FST::NODE(1, FST::RELATION('i', 1)),
		FST::NODE(2, FST::RELATION('e', 1), FST::RELATION('e', 2)),
		FST::NODE(4, FST::RELATION('c', 3), FST::RELATION('o', 3), FST::RELATION('n', 3), FST::RELATION('l', 4)),
		FST::NODE(2, FST::RELATION('e', 3), FST::RELATION('e', 4)),
		FST::NODE(5, FST::RELATION('c', 3), FST::RELATION('o', 3), FST::RELATION('n', 3), FST::RELATION('e', 5), FST::RELATION('l', 5)),
		FST::NODE(2, FST::RELATION('e', 5), FST::RELATION('r', 6)),
		FST::NODE()
	);
	if (FST::execute(fst3)) {
		cout << "Цепочка " << fst3.string << " распознана" << endl;
	}
	else
	{
		cout << "Цепочка " << fst3.string << " не распознана" << endl;
	}
	FST::FST fst4(
		(char*)"ienelr",
		7,
		FST::NODE(1, FST::RELATION('i', 1)),
		FST::NODE(2, FST::RELATION('e', 1), FST::RELATION('e', 2)),
		FST::NODE(4, FST::RELATION('c', 3), FST::RELATION('o', 3), FST::RELATION('n', 3), FST::RELATION('l', 4)),
		FST::NODE(2, FST::RELATION('e', 3), FST::RELATION('e', 4)),
		FST::NODE(5, FST::RELATION('c', 3), FST::RELATION('o', 3), FST::RELATION('n', 3), FST::RELATION('e', 5), FST::RELATION('l', 5)),
		FST::NODE(2, FST::RELATION('e', 5), FST::RELATION('r', 6)),
		FST::NODE()
	);
	if (FST::execute(fst4)) {
		cout << "Цепочка " << fst4.string << " распознана" << endl;
	}
	else
	{
		cout << "Цепочка " << fst4.string << " не распознана" << endl;
	}
	FST::FST fst5(
		(char*)"ieceeer",
		7,
		FST::NODE(1, FST::RELATION('i', 1)),
		FST::NODE(2, FST::RELATION('e', 1), FST::RELATION('e', 2)),
		FST::NODE(4, FST::RELATION('c', 3), FST::RELATION('o', 3), FST::RELATION('n', 3), FST::RELATION('l', 4)),
		FST::NODE(2, FST::RELATION('e', 3), FST::RELATION('e', 4)),
		FST::NODE(5, FST::RELATION('c', 3), FST::RELATION('o', 3), FST::RELATION('n', 3), FST::RELATION('e', 5), FST::RELATION('l', 5)),
		FST::NODE(2, FST::RELATION('e', 5), FST::RELATION('r', 6)),
		FST::NODE()
	);
	if (FST::execute(fst5)) {
		cout << "Цепочка " << fst5.string << " распознана" << endl;
	}
	else
	{
		cout << "Цепочка " << fst5.string << " не распознана" << endl;
	}
	FST::FST fst6(
		(char*)"ieoeeer",
		7,
		FST::NODE(1, FST::RELATION('i', 1)),
		FST::NODE(2, FST::RELATION('e', 1), FST::RELATION('e', 2)),
		FST::NODE(4, FST::RELATION('c', 3), FST::RELATION('o', 3), FST::RELATION('n', 3), FST::RELATION('l', 4)),
		FST::NODE(2, FST::RELATION('e', 3), FST::RELATION('e', 4)),
		FST::NODE(5, FST::RELATION('c', 3), FST::RELATION('o', 3), FST::RELATION('n', 3), FST::RELATION('e', 5), FST::RELATION('l', 5)),
		FST::NODE(2, FST::RELATION('e', 5), FST::RELATION('r', 6)),
		FST::NODE()
	);
	if (FST::execute(fst6)) {
		cout << "Цепочка " << fst6.string << " распознана" << endl;
	}
	else
	{
		cout << "Цепочка " << fst6.string << " не распознана" << endl;
	}
	FST::FST fst7(
		(char*)"ieneeeer",
		7,
		FST::NODE(1, FST::RELATION('i', 1)),
		FST::NODE(2, FST::RELATION('e', 1), FST::RELATION('e', 2)),
		FST::NODE(4, FST::RELATION('c', 3), FST::RELATION('o', 3), FST::RELATION('n', 3), FST::RELATION('l', 4)),
		FST::NODE(2, FST::RELATION('e', 3), FST::RELATION('e', 4)),
		FST::NODE(5, FST::RELATION('c', 3), FST::RELATION('o', 3), FST::RELATION('n', 3), FST::RELATION('e', 5), FST::RELATION('l', 5)),
		FST::NODE(2, FST::RELATION('e', 5), FST::RELATION('r', 6)),
		FST::NODE()
	);
	if (FST::execute(fst7)) {
		cout << "Цепочка " << fst7.string << " распознана" << endl;
	}
	else
	{
		cout << "Цепочка " << fst7.string << " не распознана" << endl;
	}
	FST::FST fst8(
		(char*)"iengeee",
		7,
		FST::NODE(1, FST::RELATION('i', 1)),
		FST::NODE(2, FST::RELATION('e', 1), FST::RELATION('e', 2)),
		FST::NODE(4, FST::RELATION('c', 3), FST::RELATION('o', 3), FST::RELATION('n', 3), FST::RELATION('l', 4)),
		FST::NODE(2, FST::RELATION('e', 3), FST::RELATION('e', 4)),
		FST::NODE(5, FST::RELATION('c', 3), FST::RELATION('o', 3), FST::RELATION('n', 3), FST::RELATION('e', 5), FST::RELATION('l', 5)),
		FST::NODE(2, FST::RELATION('e', 5), FST::RELATION('r', 6)),
		FST::NODE()
	);
	if (FST::execute(fst8)) {
		cout << "Цепочка " << fst8.string << " распознана" << endl;
	}
	else
	{
		cout << "Цепочка " << fst8.string << " не распознана" << endl;
	}
	FST::FST fst9(
		(char*)"iellre",
		7,
		FST::NODE(1, FST::RELATION('i', 1)),
		FST::NODE(2, FST::RELATION('e', 1), FST::RELATION('e', 2)),
		FST::NODE(4, FST::RELATION('c', 3), FST::RELATION('o', 3), FST::RELATION('n', 3), FST::RELATION('l', 4)),
		FST::NODE(2, FST::RELATION('e', 3), FST::RELATION('e', 4)),
		FST::NODE(5, FST::RELATION('c', 3), FST::RELATION('o', 3), FST::RELATION('n', 3), FST::RELATION('e', 5), FST::RELATION('l', 5)),
		FST::NODE(2, FST::RELATION('e', 5), FST::RELATION('r', 6)),
		FST::NODE()
	);
	if (FST::execute(fst9)) {
		cout << "Цепочка " << fst9.string << " распознана" << endl;
	}
	else
	{
		cout << "Цепочка " << fst9.string << " не распознана" << endl;
	}
	FST::FST fst10(
		(char*)"ieneee",
		7,
		FST::NODE(1, FST::RELATION('i', 1)),
		FST::NODE(2, FST::RELATION('e', 1), FST::RELATION('e', 2)),
		FST::NODE(4, FST::RELATION('c', 3), FST::RELATION('o', 3), FST::RELATION('n', 3), FST::RELATION('l', 4)),
		FST::NODE(2, FST::RELATION('e', 3), FST::RELATION('e', 4)),
		FST::NODE(5, FST::RELATION('c', 3), FST::RELATION('o', 3), FST::RELATION('n', 3), FST::RELATION('e', 5), FST::RELATION('l', 5)),
		FST::NODE(2, FST::RELATION('e', 5), FST::RELATION('r', 6)),
		FST::NODE()
	);
	if (FST::execute(fst10)) {
		cout << "Цепочка " << fst10.string << " распознана" << endl;
	}
	else
	{
		cout << "Цепочка " << fst10.string << " не распознана" << endl;
	}

	

	return 0;
}