


#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include "HT_LIB.h"
#include <string>
#include <sstream>
#include <wchar.h>
#include <thread>
#include <ctime>
#include <chrono>
#include <iomanip>

HANDLE mutexName;
wchar_t* GetWC(const char* c)
{
	const size_t cSize = strlen(c) + 1;
	wchar_t* wc = new wchar_t[cSize];
	mbstowcs(wc, c, cSize);

	return wc;
}

void printStr(char* str) {
	std::cout << "ERROR:\t";
	int i = 0;
	while (str[i]) {
		std::cout << str[i];
		i++;
	}
	std::cout << std::endl;
}

int main(int argc, char* argv[])
{
	srand(time(0));

	OS13HANDEL ht = nullptr;

	wchar_t* fileName{nullptr};
	const wchar_t* directoryPath = L"";
	std::wstring filePath(L"test.ht");
	std::wstring HTUsers = L"";
	std::wstring HTPassword = L"";

	try {
		if (argv[1]) {
			fileName = GetWC(argv[1]);
			std::wstring s(directoryPath);
			s += L"\\" + std::wstring(fileName);
			filePath = s;
			if(argc == 4)
			{
				HTUsers = GetWC(argv[2]);
				HTPassword = GetWC(argv[3]);
			}
		}

		ht = HT_LIB::Init();

		HTHANDLE* HT;

		if (HTUsers.empty() || HTUsers.empty())
		{
			HT = HT_LIB::HT::OpenExist(ht, filePath.c_str());
			if (HT == NULL)
				throw "Invalid handle";
		}
		else
		{
			HT = HT_LIB::HT::OpenExist(ht, HTUsers, HTPassword, filePath);
			if (HT == NULL)
				throw "Invalid handle";
		}

		std::wstring eventName{ fileName };
		eventName += L"E";
		HANDLE hStopEvent = CreateEvent(NULL,
			TRUE, 
			FALSE,
			eventName.c_str());

		while (WaitForSingleObject(hStopEvent, 0) == WAIT_TIMEOUT)
		{
			int numberKey = rand() % 50 + 1;
			std::string key{};

			std::stringstream convert;
			convert << numberKey;
			key = convert.str();

			Element* el = HT_LIB::HT::CreateNewFullElement(ht, key.c_str(), key.length() + 1, "0", 2);

			bool success = true;
			if (!HT_LIB::HT::Insert(ht, HT, el))
			{
				std::cout << HT_LIB::HT::GetLastError(ht, HT) << std::endl;
				success = false;
			}
			else {
				std::cout << "Inserted:" << std::endl;
				HT_LIB::HT::print(ht, el);
			}

			Sleep(1000);
		}
	}
	catch (const char* err) 
	{
		std::cout << err << std::endl;
		return 1;
	}
}