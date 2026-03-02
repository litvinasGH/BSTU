#pragma once
#include "../../Lab_4_HT/Lab_4_HT/Lab_4_HT.h"
#include "Unknwn.h"

// {ED2A7B38-4B6D-4CC2-BA69-68E3D82000B3}
static const GUID IID_IHT =
{ 0xed2a7b38, 0x4b6d, 0x4cc2, { 0xba, 0x69, 0x68, 0xe3, 0xd8, 0x20, 0x0, 0xb3 } };


__interface IHT :IUnknown {
	virtual HRESULT __stdcall COM_Create(HT::HTHANDLE** ppHandle, int Capacity, int SecSnapshotInterval, int MaxKeyLength, int MaxPayloadLength, const char FileName[512])PURE;
	virtual HRESULT __stdcall COM_Open(HT::HTHANDLE** ppHandle, const char FileName[512])PURE;
	virtual HRESULT __stdcall COM_Snap(BOOL& rc, HT::HTHANDLE* Handle)PURE;
	virtual HRESULT __stdcall COM_Close(BOOL& rc, HT::HTHANDLE* Handle)PURE;
	virtual HRESULT __stdcall COM_Insert(BOOL& rc, HT::HTHANDLE* Handle, HT::Element* Element)PURE;
	virtual HRESULT __stdcall COM_Delete(BOOL& rc, HT::HTHANDLE* Handle, HT::Element* Element)PURE;
	virtual HRESULT __stdcall COM_Get(HT::Element** ppElement, HT::HTHANDLE* Handle, HT::Element* ELement)PURE;
	virtual HRESULT __stdcall COM_Update(BOOL& rc, HT::HTHANDLE* Handle, HT::Element* Element, const void* NewPayload, int NewPayloadLength)PURE;
	virtual HRESULT __stdcall COM_Print(HT::Element* Element)PURE;
	virtual HRESULT __stdcall COM_ConstructInsertElement(HT::Element** ppElement, const void* Key, int KeyLength, const void* Payload, int PayloadLength);
	virtual HRESULT __stdcall COM_ConstructGetElement(HT::Element** ppElement, const void* Key, int KeyLength);
	virtual HRESULT __stdcall COM_ConstructUpdateElement(HT::Element** ppElement, HT::Element* OldElement, const void* NewPayload, int NewPayloadLength);
};
