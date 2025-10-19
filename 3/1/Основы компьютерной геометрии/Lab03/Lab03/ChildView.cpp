
// ChildView.cpp: реализация класса CChildView
//

#include "stdafx.h"
#include "lab3.h"
#include "ChildView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CChildView

CChildView::CChildView()
{
}

CChildView::~CChildView()
{
}


BEGIN_MESSAGE_MAP(CChildView, CWnd)
	ON_WM_PAINT()
	ON_COMMAND(ID_TESTS_F1, &CChildView::OnTestsF1)
	ON_COMMAND(ID_TESTS_F2, &CChildView::OnTestsF2)
	ON_COMMAND(ID_TESTS_F3, &CChildView::OnTestsF3)
END_MESSAGE_MAP()



// Обработчики сообщений CChildView

BOOL CChildView::PreCreateWindow(CREATESTRUCT& cs)
{
	if (!CWnd::PreCreateWindow(cs))
		return FALSE;

	cs.dwExStyle |= WS_EX_CLIENTEDGE;
	cs.style &= ~WS_BORDER;
	cs.lpszClass = AfxRegisterWndClass(CS_HREDRAW | CS_VREDRAW | CS_DBLCLKS,
		::LoadCursor(nullptr, IDC_ARROW), reinterpret_cast<HBRUSH>(COLOR_WINDOW + 1), nullptr);

	return TRUE;
}

void CChildView::OnPaint()
{
	CPaintDC dc(this); // контекст устройства для рисования
	if (Index == 1)
	{
		Graph.Draw(dc, 1, 1);
	}

	if (Index == 2)
	{
		Graph.GetRS(RS);
		SetMyMode(dc, RS, RW);	      // Устанавливает режим отображения MM_ANISOTROPIC 
		Graph.Draw1(dc, 1, 1);
		dc.SetMapMode(MM_TEXT); //ось Х вправо и ось У вниз
	}

	if (Index == 3) {
		CChildView::The3Draw(dc);
	}

	
}

double CChildView::MyF1(double x)
{
	double y = sin(x) / x;
	return y;
}

double CChildView::MyF2(double x)
{
	double y = sqrt(abs(x))* sin(x);
	return y;
}

void CChildView::OnTestsF1()	// MM_TEXT
{
	/*Invalidate();
	CPaintDC dc(this);*/
	double xL = -3 * pi;
	double xH = -xL;
	double dx = pi / 36;		//шаг
	int N = (xH - xL) / dx;
	X.RedimMatrix(N + 1);	// Изменяет размер матрицы с уничтожением данных
	Y.RedimMatrix(N + 1);
	for (int i = 0; i <= N; i++)
	{
		X(i) = xL + i * dx;
		Y(i) = MyF1(X(i));
	}
	PenLine.Set(PS_SOLID, 1, RGB(255, 0, 0));
	PenAxis.Set(PS_SOLID, 2, RGB(0, 0, 255));
	RW.SetRect(200, 200, 600, 600);
	Graph.SetParams(X, Y, RW);
	Graph.SetPenLine(PenLine);
	Graph.SetPenAxis(PenAxis);
	Index = 1;
	this->Invalidate();
	/*Graph.Draw(dc, 1, 1);*/
}


void CChildView::OnTestsF2()
{
	/*Invalidate();
	CPaintDC dc(this);*/
	double xH = 4 * pi;
	double xL = -xH;
	double dx = pi / 36;
	int N = (xH - xL) / dx;
	X.RedimMatrix(N + 1);
	Y.RedimMatrix(N + 1);
	for (int i = 0; i <= N; i++)
	{
		X(i) = xL + i * dx;
		Y(i) = MyF2(X(i));
	}
	PenLine.Set(PS_DASH, 3, RGB(255, 0, 0));
	PenAxis.Set(PS_SOLID, 2, RGB(0, 0, 0));
	RW.SetRect(200, 200, 600, 600);
	Graph.SetParams(X, Y, RW);
	Graph.SetPenLine(PenLine);
	Graph.SetPenAxis(PenAxis);
	Index = 1;
	this->Invalidate();
	/*Graph.Draw(dc, 1, 1);*/
}



void CChildView::OnTestsF3()
{
	Index = 3;
	Invalidate();

} 


void CChildView::The3Draw(CPaintDC& dc) {

	// Установка параметров пера для отображения фигуры
	CPen penFigure(PS_SOLID, 3, RGB(255, 0, 0));
	dc.SelectObject(&penFigure);

	// Установка параметров пера для отображения окружности
	CPen penCircle(PS_SOLID, 2, RGB(0, 0, 255));

	// Расчет размеров квадрата
	CRect rect;
	GetClientRect(&rect); //размер клиентской области окна
	const int squareSize = min(rect.Width(), rect.Height()); //квадрат в котором должна находиться окружность. выбираем меньшую сторону чтобы он вместился в окно

	// Расчет координат точек восьмиугольника
	const double radius = squareSize / 2.0; //радиус = половина стороны квадрата
	//const double radius = 10.0;
	const double angle = 45.0;
	const double centerX = rect.left + squareSize / 2.0; // Координаты центра квадрата
	const double centerY = rect.top + squareSize / 2.0;
	const double circleRadius = radius; // Радиус окружности, в которую вписан восьмиугольник
	const double circleLeft = centerX - circleRadius;
	const double circleTop = centerY - circleRadius;
	const double circleRight = centerX + circleRadius;
	const double circleBottom = centerY + circleRadius;

	// Отображение окружности
	dc.SelectObject(&penCircle);
	dc.Ellipse(circleLeft, circleTop, circleRight, circleBottom);

	// Отображение восьмиугольника
	const int sides = 8;
	const double angleRadians = angle * 3.14159 / 180.0;
	const double startX = centerX + radius;
	const double startY = centerY;
	double currentX = startX;
	double currentY = startY;

	for (int i = 1; i < sides; i++)
	{
		double nextX = centerX + radius * cos(angleRadians * i);
		double nextY = centerY - radius * sin(angleRadians * i);

		// Установка стиля линии для каждой стороны восьмиугольника
		if (i % 2 == 0)
			dc.SelectObject(&penFigure);
		else
			dc.SelectObject(&penFigure);

		dc.MoveTo((int)currentX, (int)currentY);
		dc.LineTo((int)nextX, (int)nextY);

		currentX = nextX;
		currentY = nextY;
	}

	// Отображение последней стороны восьмиугольника
	dc.SelectObject(&penFigure);
	dc.MoveTo((int)currentX, (int)currentY);
	dc.LineTo((int)startX, (int)startY);
}