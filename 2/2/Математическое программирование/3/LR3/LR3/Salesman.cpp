#include "Salesman.h"

// Реализация функций combi::permutation
combi::permutation::permutation(short n) {
    // Реализация конструктора
}

__int64 combi::permutation::getfirst() {
    // Реализация метода getfirst
    return 0;  // Пример возврата значения
}

__int64 combi::permutation::getnext() {
    // Реализация метода getnext
    return 0;  // Пример возврата значения
}

int sum(int x1, int x2) // Суммирование с учетом бесконечности 
{
    return (x1 == INF || x2 == INF) ? INF : (x1 + x2);
}

int* firstpath(int n) // Формирование первого маршрута 0, 1, 2, ..., n-1, 0
{
    int* rc = new int[n + 1]; rc[n] = 0;
    for (int i = 0; i < n; i++) rc[i] = i;
    return rc;
}

int* source(int n)   // Формирование исходного массива 1, 2, ..., n-1
{
    int* rc = new int[n - 1];
    for (int i = 1; i < n; i++) rc[i - 1] = i;
    return rc;
}

void copypath(int n, int* r1, const int* r2)  // Копировать маршрут
{
    for (int i = 0; i < n; i++)  r1[i] = r2[i];
}

int distance(int n, int* r, const int* d)       // Длина маршрута 
{
    int rc = 0;
    for (int i = 0; i < n - 1; i++) rc = sum(rc, d[r[i] * n + r[i + 1]]);
    return  sum(rc, d[r[n - 1] * n + 0]);    // + последняя дуга (n-1,0) 
}

void indx(int n, int* r, const int* s, const short* ntx)
{
    for (int i = 1; i < n; i++)  r[i] = s[ntx[i - 1]];
}

int salesman(
    int n,         // [in]  количество городов 
    const int* d,  // [in]  массив [n*n] расстояний 
    int* r         // [out] массив [n] маршрут 0 x x x x 
)
{
    int* s = source(n), * b = firstpath(n), rc = INF, dist = 0;
    combi::permutation p(n - 1);
    int k = p.getfirst();
    while (k >= 0)  // Цикл генерации перестановок
    {
        indx(n, b, s, p.sset);        // Новый маршрут 
        if ((dist = distance(n, b, d)) < rc)
        {
            rc = dist; copypath(n, r, b);
        }
        k = p.getnext();
    }
    return rc;
}
