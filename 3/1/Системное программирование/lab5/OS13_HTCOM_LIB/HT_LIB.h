#pragma once
#pragma comment(lib, "ole32.lib")

#define OS13HANDEL void*
/*Это заголовочный файл HT_LIB.h, который определяет интерфейс вашей статической библиотеки-обертки. Он служит «мостиком» для клиентского приложения, позволяя ему использовать COM-компонент как обычный набор C++ функций, не вникая в детали работы с GUID, интерфейсами и подсчетом ссылок.*/
#include <Windows.h>

struct HTHANDLE;
struct Element;
class IHT;

namespace HT_LIB
{
	OS13HANDEL Init();
	BOOL Create();
	void Dispose(OS13HANDEL);
	void OutputErrorHandle(int);

	namespace HT
	{
		HTHANDLE* Create(OS13HANDEL h,int Capacity, int SecSnapshotInterval, int MaxKeyLength, int MaxPayloadLength,const wchar_t HTUsersGroup[512], const wchar_t FileName[512]);
		BOOL Open(OS13HANDEL h, const std::wstring FileName);
		BOOL Open(OS13HANDEL h, const std::wstring HTUser, const std::wstring HTPassword, const std::wstring FileName);
		HTHANDLE* OpenExist(OS13HANDEL h, std::wstring FileName);
		HTHANDLE* OpenExist(OS13HANDEL h, std::wstring HTUser, std::wstring HTPassword, std::wstring FileName);
		BOOL Insert(OS13HANDEL h, HTHANDLE*, Element*);
		Element* Get(OS13HANDEL h, HTHANDLE*, Element*);
		BOOL Delete(OS13HANDEL h, HTHANDLE*, Element*);
		BOOL Update(OS13HANDEL h, HTHANDLE* hthandle, Element* oldelement, Element*);
		void Close(OS13HANDEL, const HTHANDLE*);
		char* GetLastError(OS13HANDEL h, HTHANDLE*);
		void print(OS13HANDEL h, const Element*);

		void printHT(const HTHANDLE*);
		int* GetPayload(Element*);
		Element* CreateNewFullElement(OS13HANDEL h, const void* key, int keylength, const void* payload, int payloadlength);
		Element* CreateNewKeyElement(OS13HANDEL h, const void* key, int keylength);
		DWORD WINAPI RecordingWork(LPVOID point);
	}

	static HANDLE Mutex;
	IHT* p_Ht;
}