// dllmain.cpp : Определяет точку входа для приложения DLL.
#include "pch.h"

HMODULE g_hModule; //дескриптор dll                                         // Описатель модуля
const wchar_t* g_szFriendlyName = L"OS15.ComponentHT.COM";    // Дружественное имя компонента
const wchar_t* g_szVerIndProgID = L"OS15.ComponentHT.1";      // Не зависящий от версии ProgID
const wchar_t* g_szProgID = L"OS15.ComponentHT";              // ProgID
long g_cComponents = 0;		                                // Количество активных компонентов
long g_cServerLocks = 0;                                    // Счетчик блокировок


BOOL APIENTRY DllMain(HMODULE hModule,
    DWORD  ul_reason_for_call,
    LPVOID lpReserved
)
{
    switch (ul_reason_for_call)//флаг, почему вызвана функция
    {
    case DLL_PROCESS_ATTACH://dll загружается
        g_hModule = hModule;
        break;
    case DLL_THREAD_ATTACH://создание потоков
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

