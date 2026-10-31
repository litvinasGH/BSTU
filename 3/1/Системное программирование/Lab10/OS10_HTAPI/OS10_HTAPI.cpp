// OS10_HTAPI.cpp : Определяет функции для статической библиотеки.
//

#include "pch.h"
#include "OS10_HTAPI.h"

#include <iostream>
#include <mutex>

namespace HT {
    Element::Element() :
        key(nullptr),
        keylength(0),
        payload(nullptr),
        payloadlength(0) {
    }

    Element::Element(const void* key, int keylength) :
        key(key),
        keylength(keylength),
        payload(nullptr),
        payloadlength(0) {
    }

    Element::Element(const void* key, int keylength, const void* payload, int  payloadlength) :
        key(key),
        keylength(keylength),
        payload(payload),
        payloadlength(payloadlength) {
    }

    Element::Element(Element* oldelement, const void* newpayload, int  newpayloadlength) :
        key(oldelement->key),
        keylength(oldelement->keylength),
        payload(newpayload),
        payloadlength(newpayloadlength) {
    }


    HTHANDLE::HTHANDLE() :
        Capacity(0),
        SecSnapshotInterval(0),
        MaxKeyLength(0),
        MaxPayloadLength(0),
        File(NULL),
        FileMapping(NULL),
        Addr(NULL),
        lastsnaptime(0) {
        strcpy_s(this->FileName, 512, "");
        strcpy_s(this->LastErrorMessage, 512, "");
    }

    HTHANDLE::HTHANDLE(int Capacity, int SecSnapshotInterval, int MaxKeyLength, int MaxPayloadLength, const char FileName[512])
        : Capacity(Capacity),
        SecSnapshotInterval(SecSnapshotInterval),
        MaxKeyLength(MaxKeyLength),
        MaxPayloadLength(MaxPayloadLength),
        lastsnaptime(0),
        File(NULL),
        FileMapping(NULL),
        Addr(NULL) {
        strcpy_s(this->FileName, 512, FileName);
        strcpy_s(this->LastErrorMessage, 512, "");
    }

    std::mutex ht_mutex;

    HTHANDLE* Create(int Capacity, int SecSnapshotInterval, int MaxKeyLength, int MaxPayloadLength, const char FileName[512]) {
        std::lock_guard<std::mutex>lock(ht_mutex);

        HTHANDLE* ht = new HTHANDLE(Capacity, SecSnapshotInterval, MaxKeyLength, MaxPayloadLength, FileName);
        std::cout << "----------Creation Started----------" << std::endl << std::endl;

        ht->File = CreateFileA(
            FileName,
            GENERIC_READ | GENERIC_WRITE,
            0,
            NULL,
            CREATE_ALWAYS,
            FILE_ATTRIBUTE_NORMAL,
            NULL
        );

        if (ht->File == INVALID_HANDLE_VALUE) {
            std::cout << "File Creation Failed =(\nError: " << GetLastError() << std::endl;
            delete ht;
            return NULL;
        }
        else {
            std::cout << "File Creation Successful!" << std::endl;
        }

        DWORD fileSize = GetFileSize(ht->File, NULL);
        if (fileSize == INVALID_FILE_SIZE) {
            std::cout << "File error: " << GetLastError() << std::endl;
        }

        int slot_size = (ht->MaxKeyLength + ht->MaxPayloadLength);
        std::cout << "Slot size: " << slot_size << std::endl;

        int metadata_offset = 3 * sizeof(int);
        std::cout << "Metadata offset: " << metadata_offset << std::endl;

        std::cout << "Storage capacity: " << ht->Capacity << std::endl;
        int storage_size = metadata_offset + (ht->Capacity * slot_size);
        std::cout << "Storage size: " << storage_size << std::endl;


        ht->FileMapping = CreateFileMappingA(
            ht->File,
            NULL,
            PAGE_READWRITE,
            0,
            storage_size,
            "HtMapping"
        );

        if (ht->FileMapping == NULL) {
            std::cout << "File Mapping Failed(Create) =(\nError: " << GetLastError() << std::endl;
            CloseHandle(ht->File);
            delete ht;
            return NULL;
        }
        else {
            std::cout << "File Mapping Successful(Create)!" << std::endl;
        }

        ht->Addr = MapViewOfFile(
            ht->FileMapping,
            FILE_MAP_ALL_ACCESS,
            0,
            0,
            0
        );

        if (ht->Addr == NULL) {
            std::cout << "Failed to Map View Of File(Create) =(\nError: " << GetLastError() << std::endl;
            CloseHandle(ht->FileMapping);
            CloseHandle(ht->File);
            delete ht;
            return NULL;
        }
        else {
            std::cout << "MapViewOfFile successful(Create)!" << std::endl;
        }

        memcpy(ht->Addr, &Capacity, sizeof(int));
        memcpy(static_cast<char*>(ht->Addr) + sizeof(int), &MaxKeyLength, sizeof(int));
        memcpy(static_cast<char*>(ht->Addr) + 2 * sizeof(int), &MaxPayloadLength, sizeof(int));

        std::cout << "----------Creation Complited----------" << std::endl << std::endl;

        return ht;
    }

    HTHANDLE* Open(const char FileName[512]) {
        std::cout << "----------Opening Started----------" << std::endl << std::endl;

        std::lock_guard<std::mutex>lock(ht_mutex);
        HTHANDLE* ht = new HTHANDLE();
        ht->File = CreateFileA(
            FileName,
            GENERIC_READ | GENERIC_WRITE,
            FILE_SHARE_READ | FILE_SHARE_WRITE,
            NULL,
            OPEN_EXISTING,
            FILE_ATTRIBUTE_NORMAL,
            NULL
        );

        if (ht->File == INVALID_HANDLE_VALUE) {
            std::cout << "File Creation Failed(Open) =(\nError: " << GetLastError() << std::endl;
            delete ht;
            return NULL;
        }
        else {
            std::cout << "File Creation Successful(Open)!" << std::endl;
        }

        ht->FileMapping = CreateFileMappingA(
            ht->File,
            NULL,
            PAGE_READWRITE,
            0,
            0,
            "SharedHTMapping"
        );

        if (ht->FileMapping == NULL) {
            std::cout << "File Mapping Failed(Open) =(\nError: " << GetLastError() << std::endl;
            CloseHandle(ht->File);
            delete ht;
            return NULL;
        }
        else {
            std::cout << "File Mapping Successful(Open)!" << std::endl;
        }

        ht->Addr = MapViewOfFile(
            ht->FileMapping,
            FILE_MAP_ALL_ACCESS,
            0,
            0,
            0
        );

        if (ht->Addr == NULL) {
            std::cout << "Map View Of File Failed(Open) =(\nError: " << GetLastError() << std::endl;
            CloseHandle(ht->FileMapping);
            CloseHandle(ht->File);
            delete ht;
            return NULL;
        }
        else {
            std::cout << "Map View Of File Successful(Open)!" << std::endl;
        }

        memcpy(&ht->Capacity, ht->Addr, sizeof(int));
        memcpy(&ht->MaxKeyLength, static_cast<char*>(ht->Addr) + sizeof(int), sizeof(int));
        memcpy(&ht->MaxPayloadLength, static_cast<char*>(ht->Addr) + 2 * sizeof(int), sizeof(int));

        std::cout << "Current ht->Addr: " << ht->Addr << std::endl;

        int slot_size = ht->MaxKeyLength + ht->MaxPayloadLength;
        std::cout << "Computed slot size: " << slot_size << std::endl;

        std::cout << "Computed ht->Addr: " << ht->Addr << std::endl;

        int total_mem = slot_size * ht->Capacity;
        std::cout << "Total mem: " << total_mem << std::endl;

        std::cout << "Opened HT storage has " << ht->Capacity << " capacity, " << ht->MaxKeyLength
            << " Max key length, " << ht->MaxPayloadLength << " max payload length" << std::endl;

        std::cout << "----------Opening Complited----------" << std::endl << std::endl;

        return ht;
    }

    static const char* CreateSnapshotFileName(HTHANDLE* handle) {
        static char buffer[100];
        char time_buffer[80];
        tm time_info;

        localtime_s(&time_info, &handle->lastsnaptime);
        strftime(time_buffer, sizeof(time_buffer), "%Y%m%d_%H%M%S", &time_info);
        snprintf(buffer, sizeof(buffer), "Snapshot-%s.htsnap", time_buffer);

        return buffer;

    }

    BOOL Snap(HTHANDLE* hthandle) {

        std::cout << std::endl << "----------Snap Started----------" << std::endl;

        if (!hthandle) {
            std::cout << "Snap:Failed to open the handle =(\nError: " << GetLastError() << std::endl;
            return FALSE;
        }

        hthandle->lastsnaptime = time(nullptr);

        HANDLE HTSnapshot = CreateFileA(
            CreateSnapshotFileName(hthandle),
            GENERIC_READ | GENERIC_WRITE,
            0,
            NULL,
            CREATE_ALWAYS,
            FILE_ATTRIBUTE_NORMAL,
            NULL
        );

        if (HTSnapshot == INVALID_HANDLE_VALUE) {
            std::cout << "Snap:Failed to create a snapshot file =(\nError: " << GetLastError() << std::endl;
            return FALSE;
        }
        else {
            std::cout << "--Snap:Snapshot file created--" << std::endl;
        }

        SIZE_T data_size = hthandle->countOfElements * (hthandle->MaxKeyLength + hthandle->MaxPayloadLength);
        std::cout << "Data size to write: " << data_size << std::endl;

        std::cout << "Buffer size: " << sizeof(hthandle->Addr) << std::endl;

        DWORD bytesWritten;

        BOOL writeResult = WriteFile(
            HTSnapshot,
            hthandle->Addr,
            data_size,
            &bytesWritten,
            NULL
        );

        if (!writeResult) {
            std::cout << "Snap:Failed to execute a snapshot(WriteFile error) =(\nError: " << GetLastError() << std::endl;
            CloseHandle(HTSnapshot);
            return FALSE;
        }
        else if (bytesWritten != data_size) {
            std::cout << "Snap:failed to execute a snapshot(Bytes loss) =(\nError: " << GetLastError() << std::endl;
            CloseHandle(HTSnapshot);
            return FALSE;
        }
        else {
            std::cout << "Snap: Snapshot Executed!" << std::endl;
        }

        CloseHandle(HTSnapshot);
        std::cout << std::endl << "----------Snap Complited----------" << std::endl;
        return TRUE;
    }

    BOOL Close(HTHANDLE* hthandle) {

        std::cout << std::endl << "----------Closing Started----------" << std::endl;
        if (!hthandle) {
            std::cout << "Close:Failed To Close(Invalid handle) =(\nError: " << GetLastError() << std::endl;
            return FALSE;
        }

        std::lock_guard<std::mutex>lock(ht_mutex);

        if (Snap(hthandle)) {
            std::cout << "Close:Snapshot taken" << std::endl;
        }
        else {
            std::cout << "Closing:Failed To Take a Snapshot =(\nError: " << GetLastError() << std::endl;
            return FALSE;
        }

        if (hthandle->Addr != NULL) {
            UnmapViewOfFile(hthandle->Addr);
            std::cout << "Close: Unmapped View Of File" << std::endl;
        }
        if (hthandle->FileMapping != NULL) {
            CloseHandle(hthandle->FileMapping);
            std::cout << "Close: File Mapping Handle Closed" << std::endl;
        }
        if (hthandle->File != NULL) {
            BOOL result = CloseHandle(hthandle->File);
            if (!result) {
                std::cout << "Close:Failed To Close The File Handle =(\nError: " << GetLastError() << std::endl;
                return FALSE;
            }
        }
        std::cout << "----------Closing Complited----------" << std::endl << std::endl;
        return TRUE;
    }

    static int hashFunction(const void* key, int keyLength, int capacity) {
        int hash = 5381;
        const char* str = static_cast<const char*>(key);

        for (int i = 0; i < keyLength; ++i) {
            hash = ((hash << 5) + hash) + str[i];
        }

        std::cout << "Hash: current Hash value: " << hash << std::endl;
        return abs(hash % capacity);
    }

    BOOL Insert(HTHANDLE* hthandle, const Element* element) {

        if (hthandle == NULL) {
            std::cout << "Insert: Failed to insert(Invalid handle) =(\nError: " << GetLastError() << std::endl;
            return FALSE;
        }
        else if (element == NULL) {
            std::cout << "Insert: Failed to insert(Invalid element) =(\nError: " << GetLastError() << std::endl;
            return FALSE;
        }
        else if (hthandle->Addr == NULL) {
            std::cout << "Insert: Failed to insert(Address was null) =(" << std::endl;
            return FALSE;
        }

        if (hthandle->countOfElements >= hthandle->Capacity) {
            std::cout << "Insert: Failed to insert(attempted to exceed the handle capacity) =(" << std::endl;
            return FALSE;
        }

        if (element->keylength > hthandle->MaxKeyLength) {
            std::cout << "Insert: Failed to insert(Element's key length is too big) =(" << std::endl;
            return FALSE;
        }

        if (element->payloadlength > hthandle->MaxPayloadLength) {
            std::cout << "Insert: Failed to insert(Element's payload length is too big) =(" << std::endl;
            return FALSE;
        }
        if (element->keylength == NULL) {
            std::cout << "Insert: Failed to insert(key length was NULL) =(" << std::endl;
            return FALSE;
        }
        if (element->payloadlength == NULL) {
            std::cout << "Insert: Failed to insert(payload length was NULL) =(" << std::endl;
            return FALSE;
        }

        std::lock_guard<std::mutex> lock(ht_mutex);

        const int metadata_offset = 3 * sizeof(int);
        const int slot_size = hthandle->MaxKeyLength + hthandle->MaxPayloadLength;
        int hash_index = hashFunction(element->key, element->keylength, hthandle->Capacity);

        std::cout << "Insert: initial hash index: " << hash_index << std::endl;

        char* base = static_cast<char*>(hthandle->Addr) + metadata_offset;

        for (int probe = 0; probe < hthandle->Capacity; ++probe) {
            char* slot_key = base + (hash_index * slot_size);

            bool is_empty = true;

            for (int k = 0; k < hthandle->MaxKeyLength; ++k) {
                if (slot_key[k] != 0) { is_empty = false; break; }
            }

            if (is_empty) {

                memcpy(slot_key, element->key, element->keylength);
                if (element->keylength < hthandle->MaxKeyLength) {
                    memset(slot_key + element->keylength, 0, hthandle->MaxKeyLength - element->keylength);
                }

                char* payload_area = slot_key + hthandle->MaxKeyLength;
                memcpy(payload_area, element->payload, element->payloadlength);
                if (element->payloadlength < hthandle->MaxPayloadLength) {
                    memset(payload_area + element->payloadlength, 0, hthandle->MaxPayloadLength - element->payloadlength);
                }

                hthandle->countOfElements++;
                std::cout << "--Insert: inserted at index " << hash_index << std::endl;
                return TRUE;
            }

            hash_index = (hash_index + 1) % hthandle->Capacity;
        }

        std::cout << "Insert: no free slot found =(" << std::endl;
        return FALSE;
    }


    BOOL Delete(HTHANDLE* handle, const Element* element) {


        std::cout << std::endl << "----------Deletion Started----------" << std::endl << std::endl;
        if (handle == NULL || handle->Addr == NULL) {
            std::cout << "Delete: Failed to delete an element(handle was invalid) =(" << std::endl;
            return FALSE;
        }

        if (element == NULL || element->keylength == NULL) {
            std::cout << "Delete: Failed to delete an element(element was invalid) =(" << std::endl;
            return FALSE;
        }

        std::lock_guard<std::mutex> lock(ht_mutex);

        const size_t slot_size = handle->MaxKeyLength + handle->MaxPayloadLength;
        const int metadata_offset = 3 * sizeof(int);
        char* base = static_cast<char*>(handle->Addr) + metadata_offset;

        int index_to_delete = -1;

        for (int i = 0; i < handle->Capacity; ++i) {
            char* current_slot = base + (i * slot_size);

            bool slot_empty = true;
            for (int k = 0; k < handle->MaxKeyLength; ++k) {
                if (current_slot[k] != 0) {
                    slot_empty = false;
                    break;
                }
            }
            if (slot_empty) {
                continue;
            }

            if (element->keylength <= handle->MaxKeyLength && memcmp(current_slot, element->key, element->keylength) == 0) {
                index_to_delete = i;
                break;
            }
        }

        if (index_to_delete == -1) {
            std::cout << "Delete: Failed to delete an element (element not found) =(" << std::endl;
            return FALSE;
        }

        for (int i = index_to_delete + 1; i < handle->countOfElements; ++i) {
            char* src_location = base + (i * slot_size);
            char* dest_location = base + ((i - 1) * slot_size);
            memcpy(dest_location, src_location, slot_size);
        }


        handle->countOfElements--;

        std::cout << "Delete: Successfully deleted an element with key: " << static_cast<const char*>(element->key) << std::endl;
        std::cout << "----------Deletion Complited----------" << std::endl;

        return TRUE;
    }

    Element* Get(const HTHANDLE* handle, const Element* element) {

        if (handle == NULL || handle->Addr == NULL) {
            std::cout << "Get: Failed to get an element(handle was invalid) =(" << std::endl;
            return NULL;
        }

        if (element == NULL || element->keylength == NULL) {
            std::cout << "Get: Failed to get an element(element was invalid) =(" << std::endl;
            return NULL;
        }
        std::lock_guard<std::mutex>lock(ht_mutex);
        int offset = 3 * sizeof(int);
        size_t slot_size = handle->MaxKeyLength + handle->MaxPayloadLength;
        char* base = static_cast<char*>(handle->Addr) + offset;

        for (int i = 0; i < handle->Capacity; ++i) {

            char* current_slot = base + (i * slot_size);

            bool is_empty = true;

            for (int j = 0; j < handle->MaxKeyLength; ++j) {
                if (current_slot[j] != 0) {
                    is_empty = false;
                    break;
                }
            }

            if (is_empty) {
                continue;
            }

            if (memcmp(current_slot, element->key, element->keylength) == 0) {
                Element* found = new Element(
                    current_slot,
                    element->keylength,
                    current_slot + handle->MaxKeyLength,
                    handle->MaxPayloadLength
                );

                return found;
            }
        }
        std::cout << "Get: Element not found =(" << std::endl;
        return NULL;
    }


    void print(const Element* element) {
        std::cout << "Key: " << static_cast<const char*>(element->key) << " Payload: " << static_cast<const char*>(element->payload) << std::endl;
    }

    BOOL Update(const HTHANDLE* handle, const Element* element, const void* newpayload, int newpayloadlength) {

        if (handle == NULL || handle->Addr == NULL) {
            std::cout << "Update: Failed to update an element(handle was invalid) =(" << std::endl;
            return FALSE;
        }

        if (element == NULL || element->keylength == NULL) {
            std::cout << "Update: Failed to update an element(element was invalid) =(" << std::endl;
            return FALSE;
        }

        if (newpayload == NULL || newpayloadlength == NULL || newpayloadlength > handle->MaxPayloadLength) {
            std::cout << "Update: Failed to update an element(data to update were invalid) =(" << std::endl;
            return FALSE;
        }

        std::lock_guard<std::mutex>lock(ht_mutex);

        size_t slot_size = handle->MaxKeyLength + handle->MaxPayloadLength;
        int metadata_offset = 3 * sizeof(int);
        char* base = static_cast<char*>(handle->Addr) + metadata_offset;

        for (int i = 0; i < handle->Capacity; ++i) {
            char* current_slot = base + (i * slot_size);

            bool is_empty = true;
            for (int j = 0; j < handle->MaxKeyLength; ++j) {
                if (current_slot[j] != 0) {
                    is_empty = false;
                    break;
                }
            }

            if (is_empty) {
                continue;
            }

            if (memcmp(current_slot, element->key, element->keylength) == 0) {
                memcpy(current_slot + handle->MaxKeyLength, newpayload, newpayloadlength);
                std::cout << "Update: Element updated " << hashFunction(element->key, element->keylength, handle->Capacity) << std::endl;
                return TRUE;
            }
        }

        std::cout << "Update: Failed to update(element not found) =(" << std::endl;
        return FALSE;


    }
}
