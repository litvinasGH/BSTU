
// MainFrm.cpp: реализация класса CMainFrame
//

#include "stdafx.h"
#include "lab3.h"

#include "MainFrm.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

// CMainFrame
// Получение указателя на главное меню окна
//CMenu* pMainMenu = GetMenu();
//if (pMainMenu != nullptr)
//{
//	// С оздание подменю "Tests_F"
//	CMenu menuTestsF;
//	menuTestsF.CreateMenu();
//
//	// Добавление пунктов меню "F1", "F2" и "F3" в подменю "Tests_F"
//	menuTestsF.AppendMenu(MF_STRING, ID_TESTS_F1, _T("F1"));
//	menuTestsF.AppendMenu(MF_STRING, ID_TESTS_F2, _T("F2"));
//	menuTestsF.AppendMenu(MF_STRING, ID_TESTS_F3, _T("F3"));
//
//	// Добавление подменю "Tests_F" в основное меню
//	pMainMenu->AppendMenu(MF_POPUP, (UINT_PTR)menuTestsF.m_hMenu, _T("Tests_F"));
//}
//
//// Обновление меню окна
//SetMenu(pMainMenu);


IMPLEMENT_DYNAMIC(CMainFrame, CFrameWnd)

BEGIN_MESSAGE_MAP(CMainFrame, CFrameWnd)
	ON_WM_CREATE()
	ON_WM_SETFOCUS()
END_MESSAGE_MAP()

// Создание или уничтожение CMainFrame

CMainFrame::CMainFrame()
{
}

CMainFrame::~CMainFrame()
{
}

int CMainFrame::OnCreate(LPCREATESTRUCT lpCreateStruct)
{
	if (CFrameWnd::OnCreate(lpCreateStruct) == -1)
		return -1;

	// создать представление для размещения рабочей области рамки
	if (!m_wndView.Create(nullptr, nullptr, AFX_WS_DEFAULT_VIEW,
		CRect(0, 0, 0, 0), this, AFX_IDW_PANE_FIRST, nullptr))
	{
		TRACE0("Не удалось создать окно представлений\n");
		return -1;
	}
	return 0;
}

BOOL CMainFrame::PreCreateWindow(CREATESTRUCT& cs)
{
	if( !CFrameWnd::PreCreateWindow(cs) )
		return FALSE;
	// Размер окна = 1/3 размера экрана, и окно будет расположено в центре
	cs.cy = 850;
	cs.cx = 810;
	cs.y = 150;
	cs.x = 550;
	cs.dwExStyle &= ~WS_EX_CLIENTEDGE;
	cs.lpszClass = AfxRegisterWndClass(0);
	return TRUE;
}

// Диагностика CMainFrame

#ifdef _DEBUG
void CMainFrame::AssertValid() const
{
	CFrameWnd::AssertValid();
}

void CMainFrame::Dump(CDumpContext& dc) const
{
	CFrameWnd::Dump(dc);
}
#endif //_DEBUG


// Обработчики сообщений CMainFrame

void CMainFrame::OnSetFocus(CWnd* /*pOldWnd*/)
{
	// передача фокуса окну представления
	m_wndView.SetFocus();
}

BOOL CMainFrame::OnCmdMsg(UINT nID, int nCode, void* pExtra, AFX_CMDHANDLERINFO* pHandlerInfo)
{
	// разрешить ошибки в представлении при выполнении команды
	if (m_wndView.OnCmdMsg(nID, nCode, pExtra, pHandlerInfo))
		return TRUE;

	// в противном случае выполняется обработка по умолчанию
	return CFrameWnd::OnCmdMsg(nID, nCode, pExtra, pHandlerInfo);
}

