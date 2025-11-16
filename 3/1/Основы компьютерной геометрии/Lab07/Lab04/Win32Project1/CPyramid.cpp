#include "CPyramid.h"
#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#define pi 3.14159265;
using namespace std;

CPyramid::CPyramid()
{
	this->Vertices.RedimMatrix(4, 6);	// создание матрицы с координатами точек пирамиды
	//	A	A1	B	B1	C	C1	
	//	3	1	0	0	0	0
	//	0	0	0	0	3	1
	//	0	3	0	3	0	3
	//	1	1	1	1	1	1
	this->Vertices(0, 0) = 3;		// A (ось х)
	this->Vertices(0, 1) = 1;		// A1 (ось х)
	this->Vertices(2, 1) = 3;		// A1 (ось z)
	this->Vertices(2, 3) = 3;		// B1 (ось z)
	this->Vertices(1, 4) = 3;		// C (ось y)
	this->Vertices(1, 5) = 1;		// C1 (ось у)
	this->Vertices(2, 5) = 3;		// C1 (ось z)
	for (int i = 0; i < 6; i++)
	{
		this->Vertices(3, i) = 1;
	}
}

CMatrix CalculateNormal(CPoint* vertices, int count) {//вычисляет нормаль к грани через векторное произведение двух векторов, лежащих в плоскости грани
	CMatrix v1(3), v2(3);
	v1(0) = vertices[1].x - vertices[0].x;
	v1(1) = vertices[1].y - vertices[0].y;
	v1(2) = 0;

	v2(0) = vertices[count - 1].x - vertices[0].x;
	v2(1) = vertices[count - 1].y - vertices[0].y;
	v2(2) = 0;

	return VectorMult(v1, v2);
}

CMatrix CalculateViewDirection(double fi, double q) {//преобразует сферические координаты камеры (азимут fi, зенит q) в вектор направления взгляда в декартовой системе
	double fi_rad = fi * 3.14159265 / 180.0;
	double q_rad = q * 3.14159265 / 180.0;

	CMatrix dir(3);
	dir(0) = cos(fi_rad) * sin(q_rad);
	dir(1) = sin(fi_rad) * sin(q_rad);
	dir(2) = cos(q_rad);
	return dir;
}

bool ShouldDrawFace(CPoint* face, int count, CMatrix& viewDir) {//определяет, должна ли быть видна грань, используя скалярное произведение нормали грани и вектора направления камеры
	CMatrix normal = CalculateNormal(face, count);
	return ScalarMult(normal, viewDir) < 0;
}

CMatrix CreateViewCoord(double r, double fi, double q)			// МСК -> ВСК
{
	// сферические координаты точки наблюдения
	double fi_sphere = (fi / 180) * pi;		// азимут
	double q_sphere = (q / 180) * pi;		// зенит

	CMatrix K(4, 4);
	K(0, 0) = -sin(fi_sphere);
	K(0, 1) = cos(fi_sphere);
	K(1, 0) = -cos(q_sphere) * cos(fi_sphere);
	K(1, 1) = -cos(q_sphere) * sin(fi_sphere);
	K(1, 2) = sin(q_sphere);
	K(2, 0) = -sin(q_sphere) * cos(fi_sphere);
	K(2, 1) = -sin(q_sphere) * sin(fi_sphere);
	K(2, 2) = -cos(q_sphere);
	K(2, 3) = r;
	K(3, 3) = 1;
	return K;
}


void CPyramid::Draw1(CDC &dc, CMatrix&PView, CRect &RW)			// с удалением ребер
{
	CMatrix XV = CreateViewCoord(PView(0), PView(1), PView(2));		// получаем матрицу пересчета
	CMatrix ViewVert = XV * this->Vertices;							// Kview * PIR
	CRectD RectView;
	GetRect(this->Vertices, RectView);								// задание области

	CMatrix MW = SpaceToWindow(RectView, RW);
	CPoint MasVert[6], A1B1C1[3], ABC[3];
	CMatrix V(3);
	V(2) = 1;
	for (int i = 0; i < 6; i++)
	{
		V(0) = ViewVert(0, i);
		V(1) = ViewVert(1, i);
		V = MW * V;
		MasVert[i].x = (int)V(0);
		MasVert[i].y = (int)V(1);
	}
	ABC[0] = MasVert[0];
	ABC[1] = MasVert[2];
	ABC[2] = MasVert[4];
	A1B1C1[0] = MasVert[1];
	A1B1C1[1] = MasVert[3];
	A1B1C1[2] = MasVert[5];

	char buf[50] = "";
	sprintf(buf, "%.*f",0, PView(1));
	dc.TextOut(10, 10, buf);
	sprintf(buf, "%.*f",0, PView(2));
	dc.TextOut(10, 30, buf);

	CPen Pen(PS_SOLID, 2, RGB(0, 0, 0));
	CPen *pOldPen = dc.SelectObject(&Pen);
	CBrush BottopBrush(RGB(145, 237, 24));				// цвет нижней грани
	CBrush TopBrush(RGB(145, 237, 24));					// цвет верхней грани
	CBrush BaseBrush(RGB(255, 255, 255));				// цвет остальных граней
	CBrush *pOldBrush = dc.SelectObject(&BottopBrush);
	dc.SelectObject(&BaseBrush);
	CPoint ABB1A1[4];
	ABB1A1[0] = ABC[0];
	ABB1A1[1] = ABC[1];
	ABB1A1[2] = A1B1C1[1];
	ABB1A1[3] = A1B1C1[0];

	CPoint BCB1C1[4];
	BCB1C1[0] = ABC[1];
	BCB1C1[1] = ABC[2];
	BCB1C1[2] = A1B1C1[2];
	BCB1C1[3] = A1B1C1[1];

	CPoint ACC1A1[4];
	ACC1A1[0] = ABC[2];
	ACC1A1[1] = ABC[0];
	ACC1A1[2] = A1B1C1[0];
	ACC1A1[3] = A1B1C1[2];


	// 1. Вычисляем вектор направления камеры
	double fi_rad = PView(1) * 3.14159265 / 180.0;
	double q_rad = PView(2) * 3.14159265 / 180.0;
	CMatrix viewDir(3);
	viewDir(0) = cos(fi_rad) * sin(q_rad);
	viewDir(1) = sin(fi_rad) * sin(q_rad);
	viewDir(2) = cos(q_rad);

	// 2. Функция для проверки видимости грани
	auto isFaceVisible = [&](CPoint* face, int count) {
		// Вычисляем два вектора в плоскости грани
		CMatrix v1(3), v2(3);
		v1(0) = face[1].x - face[0].x;
		v1(1) = face[1].y - face[0].y;
		v1(2) = 0;

		v2(0) = face[count - 1].x - face[0].x;
		v2(1) = face[count - 1].y - face[0].y;
		v2(2) = 0;

		// Вычисляем нормаль через векторное произведение
		CMatrix normal = VectorMult(v1, v2);

		// Скалярное произведение нормали и вектора к камере
		return ScalarMult(normal, viewDir) < 0;
		};

	// Проверяем видимость каждой грани
	bool drawABB1A1 = ShouldDrawFace(ABB1A1, 4, viewDir);
	bool drawBCB1C1 = ShouldDrawFace(BCB1C1, 4, viewDir);
	bool drawACC1A1 = ShouldDrawFace(ACC1A1, 4, viewDir);

	// Определение видимости граней
	if (PView(2) > 90) { // Если камера снизу
		if (PView(1) >= 45 && PView(1) < 135) {
			// Видны BB1C1C и ABB1A1 при виде снизу (45-135°)
			drawABB1A1 = true;
			drawBCB1C1 = true;
			drawACC1A1 = false;
		}
		else if (PView(1) >= 315 || PView(1) < 45) {
			// Особый случай для 315-45° при виде снизу
			drawABB1A1 = true;
			drawBCB1C1 = false;  
			drawACC1A1 = true;
		}
	}
	else { // Если камера сверху
		if (PView(1) >= 315 || PView(1) < 45) {
			// Видны ABB1A1 и ACC1A1 при (315-45°)
			drawABB1A1 = true;
			drawBCB1C1 = false;  
			drawACC1A1 = true;
		}
		else if (PView(1) >= 45 && PView(1) < 135) {
			drawACC1A1 = false;
		}
		else if (PView(1) >= 135 && PView(1) < 225) {
			drawBCB1C1 = false;
		}
		else if (PView(1) >= 225 && PView(1) < 315) {
			drawABB1A1 = false;
		}
	}

	// Рисование видимых граней
	if (drawABB1A1) {
		dc.SelectObject(&BaseBrush);
		dc.Polygon(ABB1A1, 4);  // Рисуем ABB1A1
	}

	if (drawBCB1C1) {
		dc.SelectObject(&BaseBrush);
		dc.Polygon(BCB1C1, 4);  // Рисуем BB1C1C 
	}

	if (drawACC1A1) {
		dc.SelectObject(&BaseBrush);
		dc.Polygon(ACC1A1, 4);  // Рисуем ACC1A1
	}

	// Всегда рисуем верхнюю грань
	dc.SelectObject(&TopBrush);
	dc.Polygon(A1B1C1, 3);
}
void CPyramid::Draw(CDC &dc, CMatrix&PView, CRect &RW)			// без удаления граней
{
	CMatrix XV = CreateViewCoord(PView(0), PView(1), PView(2));
	CMatrix ViewVert = XV * this->Vertices;
	CRectD RectView;
	GetRect(Vertices, RectView);
	CMatrix MW = SpaceToWindow(RectView, RW);
	CPoint MasVert[6], a1b1c1[3], abc[3];
	CMatrix V(3);
	V(2) = 1;
	for (int i = 0; i < 6; i++)
	{
		V(0) = ViewVert(0, i);
		V(1) = ViewVert(1, i);
		V = MW * V;
		MasVert[i].x = (int)V(0);
		MasVert[i].y = (int)V(1);
	}
	abc[0] = MasVert[0];
	abc[1] = MasVert[2];
	abc[2] = MasVert[4];
	a1b1c1[0] = MasVert[1];
	a1b1c1[1] = MasVert[3];
	a1b1c1[2] = MasVert[5];
	CPen Pen(PS_SOLID, 2, RGB(0, 0, 0));
	CPen *pOldPen = dc.SelectObject(&Pen);
	char buf[50] = "";
	sprintf(buf, "%.*f", 0, PView(1));
	dc.TextOut(10, 10, buf);
	sprintf(buf, "%.*f", 0, PView(2));
	dc.TextOut(10, 30, buf);
	dc.MoveTo(abc[0]);
	dc.LineTo(abc[1]);
	dc.MoveTo(abc[0]);
	dc.LineTo(abc[2]);
	dc.MoveTo(abc[2]);
	dc.LineTo(abc[1]);

	dc.MoveTo(a1b1c1[0]);
	dc.LineTo(a1b1c1[1]);
	dc.MoveTo(a1b1c1[0]);
	dc.LineTo(a1b1c1[2]);
	dc.MoveTo(a1b1c1[2]);
	dc.LineTo(a1b1c1[1]);

	dc.MoveTo(abc[0]);
	dc.LineTo(a1b1c1[0]);
	dc.MoveTo(abc[1]);
	dc.LineTo(a1b1c1[1]);
	dc.MoveTo(abc[2]);
	dc.LineTo(a1b1c1[2]);

	dc.SelectObject(pOldPen);
}

void  CPyramid::GetRect(CMatrix& Vert, CRectD&RectView)//определяет ограничивающий прямоугольник объекта в пространстве
{
	RectView.top = Vert.GetRow(2).MinElement();
	RectView.bottom = Vert.GetRow(2).MaxElement();
	RectView.left = Vert.GetRow(0).MinElement();
	RectView.right = Vert.GetRow(0).MaxElement();
}

CMatrix VectorMult(CMatrix& V1, CMatrix& V2)//Векторное произведение
{
	if (V1.rows() != V2.rows() || V1.cols() > 1 || V2.cols() > 1) // Число столбцов больше одного 
	{
		char* error = "CMatrix VectorMult(CMatrix& V1, CMatrix& V2) объект не вектор - число столбцов больше 1 ";
		MessageBoxA(NULL, error, "Ошибка", MB_ICONSTOP);
		exit(1);
	}

	CMatrix Temp = V1;
	Temp(0) = V1(1)*V2(2) - V1(2)*V2(1);
	Temp(1) = V1(2)*V2(0) - V1(0)*V2(2);
	Temp(2) = V1(0)*V2(1) - V1(1)*V2(0);

	return Temp;
}

double ScalarMult(CMatrix& V1, CMatrix& V2)//double ScalarMult(CMatrix& V1, CMatrix& V2)
{
	if (V1.rows() != V2.rows() || V1.cols() > 1 || V2.cols() > 1) // Число столбцов больше одного 
	{
		char* error = "double ScalarMult(CMatrix& V1, CMatrix& V2) объект не вектор - число столбцов больше 1 ";
		MessageBoxA(NULL, error, "Ошибка", MB_ICONSTOP);
		exit(1);
	}

	return V1(0)*V2(0) + V1(1)*V2(1) + V1(2)*V2(2);
}