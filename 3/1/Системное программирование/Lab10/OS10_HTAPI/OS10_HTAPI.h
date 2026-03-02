#pragma once
#include <Windows.h>

namespace HT    // HT API
{
	// API HT - программный интерфейс для доступа к НТ-хранилищу 
	//          НТ-хранилище предназначено для хранения данных в ОП в формате ключ/значение
	//          Персистестеность (сохранность) данных обеспечивается с помощью snapshot-менханизма 
	//          Create - создать  и открыть HT-хранилище для использования   
	//          Open   - открыть HT-хранилище для использования
	//          Insert - создать элемент данных
	//          Delete - удалить элемент данных    
	//          Get    - читать  элемент данных
	//          Update - изменить элемент данных
	//          Snap   - выпонить snapshot
	//          Close  - выполнить Snap и закрыть HT-хранилище для использования
	//          GetLastError - получить сообщение о последней ошибке


	struct HTHANDLE    // блок управления HT
	{
		HTHANDLE();
		HTHANDLE(int Capacity, int SecSnapshotInterval, int MaxKeyLength, int MaxPayloadLength, const char FileName[512]);
		int     Capacity;               // емкость хранилища в количестве элементов 
		int     SecSnapshotInterval;    // переодичность сохранения в сек. 
		int     MaxKeyLength;           // максимальная длина ключа
		int     MaxPayloadLength;       // максимальная длина данных
		char    FileName[512];          // имя файла 
		HANDLE  File;                   // File HANDLE != 0, если файл открыт
		HANDLE  FileMapping;            // Mapping File HANDLE != 0, если mapping создан  
		LPVOID  Addr;                   // Addr != NULL, если mapview выполнен  
		char    LastErrorMessage[512];  // сообщение об последней ошибке или 0x00  
		time_t  lastsnaptime;           // дата последнего snap'a (time())  
		int countOfElements = 0; // кол-во элементов в сторедже
	};
	///
	struct Element   // элемент 
	{
		Element();
		/// <summary>
		///  for Get
		/// </summary>
		Element(const void* key, int keylength);                                             // for Get
		/// <summary>
		/// for Insert
		/// </summary>
		Element(const void* key, int keylength, const void* payload, int  payloadlength);    // for Insert
		/// <summary>
		/// for update
		/// </summary>
		Element(Element* oldelement, const void* newpayload, int  newpayloadlength);         // for update
		const void* key;                 // значение ключа 
		int             keylength;           // рахмер ключа
		const void* payload;             // данные 
		int             payloadlength;       // размер данных
	};

	/// <summary>
	///  создать HT 
	/// </summary>
	/// <param name="Capacity">емкость хранилища</param>
	/// <param name="SecSnapshotInterval">переодичность сохранения в сек.</param>
	/// <param name="MaxKeyLength">максимальный размер ключа</param>
	/// <param name="MaxPayloadLength">максимальный размер данных</param>
	/// <param name="FileName">имя файла</param>
	/// <returns>!= NULL успешное завершение</returns>
	HTHANDLE* Create   //  создать HT             
	(
		int	  Capacity,					   // емкость хранилища
		int   SecSnapshotInterval,		   // переодичность сохранения в сек.
		int   MaxKeyLength,                // максимальный размер ключа
		int   MaxPayloadLength,            // максимальный размер данных
		const char  FileName[512]          // имя файла 
	); 	// != NULL успешное завершение  

	/// <summary>
	/// открыть HT
	/// </summary>
	/// <param name="FileName">имя файла</param>
	/// <returns>!= NULL успешное завершение</returns>
	HTHANDLE* Open     //  открыть HT             
	(
		const char    FileName[512]         // имя файла 
	); 	// != NULL успешное завершение  

	/// <summary>
	/// выполнить Snapshot
	/// </summary>
	/// <param name="hthandle">управление HT (File, FileMapping)</param>
	/// <returns></returns>
	BOOL Snap         // выполнить Snapshot
	(
		const HTHANDLE* hthandle           // управление HT (File, FileMapping)
	);

	/// <summary>
	/// Snap и закрыть HT  и  очистить HTHANDLE
	/// </summary>
	/// <param name="hthandle">управление HT (File, FileMapping)</param>
	/// <returns>== TRUE успешное завершени</returns>
	BOOL Close        // Snap и закрыть HT  и  очистить HTHANDLE
	(
		HTHANDLE* hthandle           // управление HT (File, FileMapping)
	);	//  == TRUE успешное завершение   

	/// <summary>
	/// добавить элемент в хранилище
	/// </summary>
	/// <param name="hthandle">управление HT</param>
	/// <param name="element">элемент</param>
	/// <returns>== TRUE успешное завершение</returns>
	BOOL Insert      // добавить элемент в хранилище
	(
		HTHANDLE* hthandle,            // управление HT
		const Element* element              // элемент
	);	//  == TRUE успешное завершение 

	/// <summary>
	/// удалить элемент в хранилище
	/// </summary>
	/// <param name="hthandle">управление HT (ключ)</param>
	/// <param name="element">элемент</param>
	/// <returns>== TRUE успешное завершение</returns>
	BOOL Delete      // удалить элемент в хранилище
	(
		HTHANDLE* hthandle,            // управление HT (ключ)
		const Element* element              // элемент 
	);	//  == TRUE успешное завершение 

	/// <summary>
	/// читать элемент в хранилище
	/// </summary>
	/// <param name="hthandle">управление HT</param>
	/// <param name="element">элемент</param>
	/// <returns>!= NULL успешное завершение</returns>
	Element* Get     //  читать элемент в хранилище
	(
		const HTHANDLE* hthandle,            // управление HT
		const Element* element              // элемент 
	); 	//  != NULL успешное завершение 

	/// <summary>
	/// именить элемент в хранилище
	/// </summary>
	/// <param name="hthandle">управление HT</param>
	/// <param name="oldelement">старый элемент (ключ, размер ключа)</param>
	/// <param name="newpayload">новые данные</param>
	/// <param name="newpayloadlength">размер новых данных</param>
	/// <returns>!= NULL успешное завершение</returns>
	BOOL Update     //  именить элемент в хранилище
	(
		const HTHANDLE* hthandle,            // управление HT
		const Element* oldelement,          // старый элемент (ключ, размер ключа)
		const void* newpayload,          // новые данные  
		int             newpayloadlength     // размер новых данных
	); 	//  != NULL успешное завершение 

	/// <summary>
	/// получить сообщение о последней ошибке
	/// </summary>
	/// <param name="ht">управление HT</param>
	/// <returns></returns>
	//char* GetLastError  // получить сообщение о последней ошибке
	//(
	//	HTHANDLE* ht                         // управление HT
	//);

	/// <summary>
	/// распечатать элемент
	/// </summary>
	/// <param name="element">элемент</param>
	void print                               // распечатать элемент 
	(
		const Element* element              // элемент 
	);


};
