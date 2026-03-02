#include "Dictionary.h"
#include "stdafx.h"
#include <iostream>
#include <cstring>
using namespace std;
namespace Dictionary {
	Instance Create(const char name[DICTNAMEMAXSIZE], int size) {

		if (strlen(name) > DICTNAMEMAXSIZE) {
			throw(char*)THROW01;
		}
		if (size > DICTMAXSIZE) {
			throw(char*)THROW02;
		}
		Instance newDict{};
		int lenght = strlen(name);
		for (int i = 0; i < lenght; i++) {
			newDict.name[i] = name[i];
		}
		newDict.name[lenght] = '\0';

		newDict.maxsize = size;
		newDict.size = 0;
		return newDict;
	}
	void AddEntry(Instance& inst, Entry ed) {
		if (inst.size == inst.maxsize)
			throw (char*)THROW03;
		for (int i = 0; i < inst.size; i++) {
			if (ed.id == inst.dictionary[i]->id)
				throw (char*)THROW04;
		}
		inst.dictionary[inst.size] = new Entry();
		inst.dictionary[inst.size]->id = ed.id;
		int lenght = strlen(ed.name);
		for (int i = 0; i < lenght; i++) {
			inst.dictionary[inst.size]->name[i] = ed.name[i];
		}
		inst.dictionary[inst.size]->name[lenght] = '\0';
		inst.size++;
	}
	void DelEntry(Instance& inst, int id) {
		bool isFound = false;
		for (int i = 0; i < inst.size; i++) {
			if (inst.dictionary[i]->id == id) {
				isFound = true;
				for (int j = i; j < inst.size; j++) {
					inst.dictionary[j] = inst.dictionary[j + 1];
				}
				inst.size--;
				delete inst.dictionary[inst.size];
				break;
			}
		}
		if (!isFound) throw (char*)THROW06;
	}
	Entry GetEntry(Instance inst, int id) {
		Entry getElement{};
		for (int i = 0; i < inst.size; i++) {
			if (id == inst.dictionary[i]->id) {
				getElement.id = inst.dictionary[i]->id;
				int lenght = strlen(inst.dictionary[i]->name);
				for (int j = 0; j < lenght; j++)
					getElement.name[j] = inst.dictionary[i]->name[j];
				getElement.name[lenght] = '\0';
				return getElement;
			}
		}
		throw (char*)THROW05;
	}
	void UpdEntry(Instance& inst, int id, Entry new_ed) {
		bool isFound = false;
		for (int i = 0; i < inst.size; i++) {
			if (inst.dictionary[i]->id == id) {
				for (int j = i; j < inst.size; j++) {
					if (new_ed.id == inst.dictionary[j]->id) {
						throw (char*)THROW08;
					}
				}
				isFound = true;
				inst.dictionary[i]->id = new_ed.id;
				strcpy_s(inst.dictionary[i]->name, new_ed.name);
				break;
			}
			else if (new_ed.id == inst.dictionary[i]->id) {
				throw (char*)THROW08;
			}
		}
		if (!isFound) {
			throw (char*)THROW07;
		}
	}
	void Delete(Instance& inst) {
		for (int i = 0; i < inst.size; i++) {
			delete[] inst.dictionary[i];
		}
		inst.size = 0;
	}
	void Print(Instance inst) {
		cout << "-------" << inst.name << "-------" << endl;
		for (int i = 0; i < inst.size; i++) {
			cout << inst.dictionary[i]->id << ' ';
			int lenght = strlen(inst.dictionary[i]->name);
			for (int j = 0; j < lenght; j++) {
				cout << inst.dictionary[i]->name[j];
			}
			cout << endl;
		}
	}
}