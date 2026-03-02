#include <iostream>
#include <string>
#include <list>
#include <chrono>
#include <iomanip>
using namespace std;

struct Contact {
    int phoneNumber;
    string fullName;
};

int hashFunction(int phoneNumber, int tableSize) {
    return phoneNumber % tableSize;
}


const int tableSize = 16; 
list<Contact>* hashTable[tableSize];

void initializeHashTable() {
    for (int i = 0; i < tableSize; ++i) {
        hashTable[i] = new list<Contact>();
    }
}

void insert(int phoneNumber, const string& fullName) {
    int index = hashFunction(phoneNumber, tableSize);
    Contact newContact = { phoneNumber, fullName };
    hashTable[index]->push_back(newContact);
}

void remove(int phoneNumber) {
    int index = hashFunction(phoneNumber, tableSize);
    for (auto it = hashTable[index]->begin(); it != hashTable[index]->end(); ++it) {
        if (it->phoneNumber == phoneNumber) {
            hashTable[index]->erase(it);
            cout << "Contact removed." << std::endl;
            return;
        }
    }
    cout << "Contact not found." << std::endl;
}

Contact* search(int phoneNumber) {
    int index = hashFunction(phoneNumber, tableSize);
    for (auto& contact : *hashTable[index]) {
        if (contact.phoneNumber == phoneNumber) {
            return &contact;
        }
    }
    return nullptr;
}

void printTable() {
    for (int i = 0; i < tableSize; ++i) {
        cout << std::setfill('0') << std::setw(2) << i << ": ";
        for (const auto& contact : *hashTable[i]) {
            cout << contact.phoneNumber << " (" << contact.fullName << ") -> ";
        }
        cout << "NULL" << std::endl;
    }
}

int main() {
    initializeHashTable();
    int i = 0;
    string name;
    short number;
    do {
        cout << "Number: ";
        cin >> number;
        cout << "Name: ";
        cin.ignore(cin.rdbuf()->in_avail());
        cin.sync();
        getline(cin, name);
        if (number != 0 && name != "0") {
            insert(number, "Name: " + name);
        }
    } while (number != 0 && name != "0");
    cout << "Serch number: ";
    cin >> number;

    auto start_time = chrono::high_resolution_clock::now();
    Contact* ptr = search(number);
    auto end_time = chrono::high_resolution_clock::now();
    if (ptr != nullptr) {
        cout << ptr->phoneNumber << " (" << ptr->fullName << ")" << endl;
    }
    else {
        cout << 404 << endl;
    }
    auto res = std::chrono::duration_cast<std::chrono::nanoseconds>(end_time - start_time).count();
    cout << res / 1000000000 << "sec "
        << (res % 1000000000) / 1000000 << "msec "
        << (res % 1000000) / 1000 << "micsec "
        << res % 1000 << "nanosec" << endl;

    cout << "Del number: ";
    cin >> number;
    remove(number);


    printTable();

   

    return 0;
}