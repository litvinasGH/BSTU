#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include "HT_LIB.h"
#include <wchar.h>

wchar_t* GetWC(const char* c)
{
	wchar_t* wc = new wchar_t[strlen(c) + 1];
	mbstowcs(wc, c, strlen(c) + 1);

	return wc;
}

int main(int argc, char* argv[])
{
	OS13HANDEL ht = nullptr;

	wchar_t* fileName{ nullptr };
	const std::wstring directoryPath = L"";
	std::wstring filePath(L"test.ht");
	wchar_t* HTUsers{nullptr};
	wchar_t* HTPassword{nullptr};

	try {

		if (argv[1]) 
		{
			fileName = GetWC(argv[1]);
			std::wstring s(directoryPath);
			s += L"" + std::wstring(fileName);
			filePath = s;
			if(argc == 4)
			{
				HTUsers = GetWC(argv[2]);
				std::wcout << HTUsers << std::endl;
				HTPassword = GetWC(argv[3]);
			}
		}
		else
			std::cout << "Using default file path" << std::endl;

		ht = HT_LIB::Init();
		if (ht == NULL)
			throw "Error init com";
		if (HTUsers == nullptr || HTPassword == nullptr)
		{
			if (!HT_LIB::HT::Open(ht, filePath.c_str()))
				throw "Error open";
		}
		else
		{  
			if (!HT_LIB::HT::Open(ht, HTUsers, HTPassword, filePath))
				throw "Error open";
		}

		std::cout << "HT-Storage Open " << std::endl;

		std::wstring eventName{ fileName };
		eventName += L"E";
		HANDLE hStopEvent = CreateEvent(NULL,
			TRUE, 
			FALSE,
			eventName.c_str());

		while (WaitForSingleObject(hStopEvent, INFINITE) == WAIT_TIMEOUT)
		{
		}

		HT_LIB::Dispose(ht);
		return 0;
	}
	catch (const char* err)
	{
		std::cout << err << std::endl;
		return 1;
	}
}
