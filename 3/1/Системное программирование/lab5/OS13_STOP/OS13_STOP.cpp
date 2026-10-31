#define _CRT_SECURE_NO_WARNINGS

#include <windows.h>
#include <iostream>

wchar_t* GetWC(const char* c)
{
    wchar_t* wc = new wchar_t[strlen(c) + 1];
    mbstowcs(wc, c, strlen(c) + 1);
    return wc;
}

int main(int argc, char* argv[])
{
    HANDLE hStopEvent;
    if (argc == 0)
    {
        hStopEvent = CreateEvent(NULL,
            TRUE,
            FALSE,
            L"Stop");
        SetEvent(hStopEvent);
    }
    else
    {
        std::wstring eventName{ GetWC(argv[1]) };
        eventName += L"E";
        hStopEvent = CreateEvent(NULL, TRUE, FALSE, eventName.c_str());
        SetEvent(hStopEvent);
    }
    return 0;
}
