#ifndef SALESMAN_H
#define SALESMAN_H

#include <climits>

const int INF = INT_MAX;

int sum(int x1, int x2);  // Суммирование с учетом бесконечности
int* firstpath(int n);    // Формирование первого маршрута 0, 1, 2, ..., n-1, 0
int* source(int n);       // Формирование исходного массива 1, 2, ..., n-1
void copypath(int n, int* r1, const int* r2);  // Копировать маршрут
int distance(int n, int* r, const int* d);     // Длина маршрута
void indx(int n, int* r, const int* s, const short* ntx);
int salesman(int n, const int* d, int* r);     // Реализация задачи коммивояжера

namespace combi {
    class permutation {
    public:
        permutation(short n);
        __int64 getfirst();
        __int64 getnext();
        short* sset;
    };
}

#endif 

