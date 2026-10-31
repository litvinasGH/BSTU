#pragma once
#include <Unknwn.h>
#include <Windows.h>
	
// {9FF81562-1CB3-44DA-B6F3-1738D9735550}
static const GUID IID_IAdder =
{ 0x9ff81562, 0x1cb3, 0x44da, { 0xb6, 0xf3, 0x17, 0x38, 0xd9, 0x73, 0x55, 0x50 } };


// {FB90C637-EE03-462E-8AF6-6C3C1DA56904}
static const GUID IID_IMultiplier =
{ 0xfb90c637, 0xee03, 0x462e, { 0x8a, 0xf6, 0x6c, 0x3c, 0x1d, 0xa5, 0x69, 0x4 } };

// {BE2DD593-1DFC-4525-92E0-D75655BD3CEC}
static const GUID CLSID_OS12 =
{ 0xbe2dd593, 0x1dfc, 0x4525, { 0x92, 0xe0, 0xd7, 0x56, 0x55, 0xbd, 0x3c, 0xec } };

//// {0AF56D4B-68CA-41E8-BF89-9A934DDC822C}
//static const GUID CLSID_OS12 =
//{ 0xaf56d4b, 0x68ca, 0x41e8, { 0xbf, 0x89, 0x9a, 0x93, 0x4d, 0xdc, 0x82, 0x2c } };

//// {F95249A9-2CEC-4112-9A58-05DA2737B877}
//static const GUID CLSID_OS12 =
//{ 0xf95249a9, 0x2cec, 0x4112, { 0x9a, 0x58, 0x5, 0xda, 0x27, 0x37, 0xb8, 0x77 } };


__interface IAdder :public IUnknown {
	
	virtual HRESULT __stdcall Add(const double x, const double y, double& z) = 0;
	virtual HRESULT __stdcall Sub(const double x, const double y, double& z) = 0;
};

__interface IMultiplier :public IUnknown {
	virtual HRESULT __stdcall Mul(const double x, const double y, double& z) = 0;
	virtual HRESULT __stdcall Div(const double x, const double y, double& z) = 0;
};


HRESULT CreateOS12Instance(REFIID riid, void** ppv);

extern LONG g_cObjects;
extern LONG g_cServerLocks;
