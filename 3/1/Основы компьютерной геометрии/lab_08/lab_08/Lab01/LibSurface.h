#pragma once
#ifndef LIBSURFACE
#define LIBSURFACE 1

#include <vector>
#include "CMatrix.h"
#include <afxwin.h>

////////////// === ОПРЕДЕЛЕНИЕ ТИПОВ === /////////////////////////////////

typedef std::vector<CMatrix> CVecMatrix;
typedef std::vector<CVecMatrix> CMasMatrix;

typedef std::vector<CPoint> CVecPoint;
typedef std::vector<CVecPoint> CMatrPoint;

//----------------- Тестовая функция -----------------------------------

double Function1(double x, double y);
double Function2(double x, double y);
double Function3(double x, double y);

//-------------------- class CPlot3D ----------------------------------
class CPlot3D
{
    pfunc2 pFunc;            // Указатель на функцию f(x,y), описывающую поверхность
    CRectD SpaceRect;        // Прямоугольная область, на которую опирается поверхность в мировой СК
    CMasMatrix MatrF;        // Матрица для хранения координат точек (x,y,z,1) поверхности в мировой СК
    CMasMatrix MatrView;    // Матрица для хранения координат точек (x,y,1) проекции поверхности на плоскость XY видовой СК
    CRectD ViewRect;        // Прямоугольная область, охватывающая проекцию поверхности на плоскость XY видовой СК
    CRect WinRect;            // Прямоугольная область в оконной системе координат для рисования        
    CMatrix ViewPoint;        // Вектор (3x1) - координаты точки наблюдения в мировой                                         
    // сферической системе координат (r,fi(град.), q(град.))
    CMatrPoint MatrWindow;  // Матрица для хранения оконных координат P(xi,yi) точек изображения 

public:
    CPlot3D();
    virtual ~CPlot3D() { pFunc = NULL; };
    void SetFunction(pfunc2 pF, CRectD RS, double dx, double dy);    // Устанавливает функцию f(x,y)
    void SetViewPoint(double r, double fi, double q);        // Устанавливает положение точки наблюдения в МИРОВОЙ СК
    CMatrix GetViewPoint();                // Возвращает вектор ViewPoint
    void SetWinRect(CRect Rect);        // Устанавливает область в окне для рисования
    void CreateMatrF(double dx, double dy); // Заполняет матрицу MatrF координатами точек поверхности
    int GetNumberRegion();
    void CreateMatrView();        // Заполняет матрицу MatrView координатами точек проекции поверхности на плоскость XY видовой СК
    void CreateMatrWindow();    // Заполняет матрицу MatrWindow 
    void Draw(CDC& dc);
};

#endif