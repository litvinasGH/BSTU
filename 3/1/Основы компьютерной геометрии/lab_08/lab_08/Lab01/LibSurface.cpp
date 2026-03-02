#include "stdafx.h"

double Function1(double x, double y)
{
    return x * x + y * y; //z=x^2+y^2
};

double Function2(double x, double y)
{
    return x * x - y * y; //z=x^2-y^2
};

double Function3(double x, double y) {
    double r_sq = x * x + y * y; //z=sqrt(9-x^2-y^2) т.е. x^2+y^2<=9
    if (r_sq >= 8.99) return 0;  // Плавный переход у границы
    return sqrt(9.0 - r_sq);
}

CPlot3D::CPlot3D()
{
    pFunc = NULL;                    // Указатель на функцию
    ViewPoint.RedimMatrix(3);        // Параметры камеры [r, φ, θ]
    WinRect.SetRect(0, 0, 200, 200); // Область отрисовки в окне
    ViewPoint(0) = 10;               // Расстояние камеры
    ViewPoint(1) = 30;               // Азимутальный угол φ
    ViewPoint(2) = 45;               // Зенитный угол θ
};

void CPlot3D::SetFunction(pfunc2 pF, CRectD RS, double dx, double dy)
{
    pFunc = pF;
    SpaceRect.SetRectD(RS.left, RS.top, RS.right, RS.bottom);
    MatrF.clear();
    CreateMatrF(dx, dy);//вызов//////////////////
}
//установка положения камеры
void CPlot3D::SetViewPoint(double r, double fi, double q)
{
    ViewPoint(0) = r;
    ViewPoint(1) = fi;
    ViewPoint(2) = q;

    MatrView.clear();
    CreateMatrView();

    MatrWindow.clear();
    CreateMatrWindow();//видовые в оконные
}

CMatrix CPlot3D::GetViewPoint()
{
    return ViewPoint;
}

void CPlot3D::SetWinRect(CRect Rect)
{
    WinRect = Rect;
    MatrWindow.clear();
    CreateMatrWindow();
}
//---------------------- создание матрицы точек поверхности
void CPlot3D::CreateMatrF(double dx, double dy) //dx, dy — шаги дискретизации по осям X и Y (чем меньше, тем точнее поверхность).
{
    //SpaceRect — границы области (left, right, bottom, top)
    double xL = SpaceRect.left;
    double xH = SpaceRect.right;
    double yL = SpaceRect.bottom;
    double yH = SpaceRect.top;
    CVecMatrix VecMatrix;
    CMatrix V(4);
    V(3) = 1;

    for (double x = xL; x <= xH; x += dx) //Заполняет матрицу MatrF координатами точек поверхности в мировой системе координат (МСК).
    {
        VecMatrix.clear();
        for (double y = yL; y <= yH; y += dy)
        {
            V(0) = x;
            V(1) = y;
            V(2) = pFunc(x, y);
            VecMatrix.push_back(V);
        }
        MatrF.push_back(VecMatrix);
    }
}
//---------------------------- Определяет, как нужно рисовать полигоны (зависит от положения камеры).
int CPlot3D::GetNumberRegion()
{
    CMatrix CartPoint = SphereToCart(ViewPoint); //Переводит сферические координаты камеры в декартовы (SphereToCart)
    double xView = CartPoint(0);
    double yView = CartPoint(1);

    double xL = SpaceRect.left;
    double xH = SpaceRect.right;
    double yL = SpaceRect.bottom;
    double yH = SpaceRect.top;

    double y1 = yL + (yH - yL) * (xView - xL) / (xH - xL);
    double y2 = yH - (yH - yL) * (xView - xL) / (xH - xL);
    //Определяет, с какой стороны камера смотрит на объект:
    if ((yView <= y1) && (yView <= y2)) return 1; // Сверху-слева → рисуем справа-налево, снизу-вверх.
    if ((yView > y2) && (yView < y1)) return 2; // Сверху-справа → слева-направо, снизу-вверх.
    if ((yView >= y1) && (yView >= y2)) return 3; // Снизу-справа → слева-направо, сверху-вниз.
    if ((yView > y1) && (yView < y2)) return 4; //  Снизу-слева → справа-налево, сверху-вниз.

    return 1; // default case
}
 //применение: мировая → видовая система координат
void CPlot3D::CreateMatrView() 
{
    CMatrix MV = CreateViewCoord(ViewPoint(0), ViewPoint(1), ViewPoint(2));
    CVecMatrix VecMatrix;
    CMatrix VX(4), V(3);
    V(2) = 1;
    double xmin = DBL_MAX;
    double xmax = -DBL_MAX;
    double ymin = DBL_MAX;
    double ymax = -DBL_MAX;

    for (size_t i = 0; i < MatrF.size(); i++)
    {
        VecMatrix.clear();
        for (size_t j = 0; j < MatrF[i].size(); j++)
        {
            VX = MatrF[i][j];
            VX = MV * VX;
            V(0) = VX(0);
            V(1) = VX(1);
            VecMatrix.push_back(V);

            if (V(0) < xmin) xmin = V(0);
            if (V(0) > xmax) xmax = V(0);
            if (V(1) < ymin) ymin = V(1);
            if (V(1) > ymax) ymax = V(1);
        }
        MatrView.push_back(VecMatrix);
    }
    ViewRect.SetRectD(xmin, ymax, xmax, ymin);
}
// применение: видовая → оконная система координат
void CPlot3D::CreateMatrWindow() 
{
    CMatrix MW = SpaceToWindow(ViewRect, WinRect);
    CVecPoint VecPoint;
    CMatrix V(3);

    for (size_t i = 0; i < MatrView.size(); i++)
    {
        VecPoint.clear();
        for (size_t j = 0; j < MatrView[i].size(); j++)
        {
            V = MatrView[i][j];
            V = MW * V;
            CPoint P(static_cast<int>(V(0)), static_cast<int>(V(1)));
            VecPoint.push_back(P);
        }
        MatrWindow.push_back(VecPoint);
    }
}

//--------------Метод GetNumberRegion() определяет, с какой стороны находится камера,
// и возвращает номер региона (1-4). Это влияет на порядок отрисовки полигонов
void CPlot3D::Draw(CDC& dc) // Рисует поверхность как набор четырёхугольников.
{
    if (MatrWindow.empty()) //Проверяется, что MatrWindow не пуста.
    {
        AfxMessageBox(_T("Массив данных для рисования в окне пуст!"));
        return;
    }

    int kRegion = GetNumberRegion(); //Определяется порядок отрисовки (GetNumberRegion).
    int nRows = static_cast<int>(MatrWindow.size());
    int nCols = static_cast<int>(MatrWindow[0].size());

    CPoint pt[4]; //Берутся 4 соседние точки, формируя полигон. Заливается цветом с использованием CPen и CBrush.
    CPen pen(PS_SOLID, 1, RGB(0, 0, 0));
    CBrush brush(HS_DIAGCROSS, RGB(200, 200, 255));
    dc.SelectObject(&pen);
    dc.SelectObject(&brush);

    dc.SetPolyFillMode(WINDING);  // WINDING — сложные фигуры. ALTERNATE — стандартная заливка.
    //dc.SetBkMode(TRANSPARENT);

    CPen* pOldPen = dc.SelectObject(&pen);
    CBrush* pOldBrush = dc.SelectObject(&brush);

    //Рисуются 4 - угольные полигоны на основе точек из MatrWindow
    switch (kRegion)
    {
    case 1:
        for (int j = nCols - 1; j > 0; j--)
            for (int i = 0; i < nRows - 1; i++)
            {
                pt[0] = MatrWindow[i][j];
                pt[1] = MatrWindow[i][j - 1];
                pt[2] = MatrWindow[i + 1][j - 1];
                pt[3] = MatrWindow[i + 1][j];
                dc.Polygon(pt, 4);
            }
        break;
    case 2:
        for (int i = 0; i < nRows - 1; i++)
            for (int j = 0; j < nCols - 1; j++)
            {
                pt[0] = MatrWindow[i][j];
                pt[1] = MatrWindow[i][j + 1];
                pt[2] = MatrWindow[i + 1][j + 1];
                pt[3] = MatrWindow[i + 1][j];
                dc.Polygon(pt, 4);
            }
        break;
    case 3:
        for (int j = 0; j < nCols - 1; j++)
            for (int i = 0; i < nRows - 1; i++)
            {
                pt[0] = MatrWindow[i][j];
                pt[1] = MatrWindow[i][j + 1];
                pt[2] = MatrWindow[i + 1][j + 1];
                pt[3] = MatrWindow[i + 1][j];
                dc.Polygon(pt, 4);
            }
        break;
    case 4:
        for (int i = nRows - 1; i > 0; i--)
            for (int j = 0; j < nCols - 1; j++)
            {
                pt[0] = MatrWindow[i][j];
                pt[1] = MatrWindow[i][j + 1];
                pt[2] = MatrWindow[i - 1][j + 1];
                pt[3] = MatrWindow[i - 1][j];
                dc.Polygon(pt, 4);
            }
        break;
    }

    dc.SelectObject(pOldPen);
    dc.SelectObject(pOldBrush);
}