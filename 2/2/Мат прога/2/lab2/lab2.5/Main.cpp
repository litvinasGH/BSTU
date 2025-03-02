#include "stdafx.h"
#include <iostream>
#include <iomanip> 
#include "Boat.h"
#define MM 8
#define SPACE(n) std::setw(n)<<" "
#define NN 9

void start()                          // старт  генератора сл. чисел
{
    srand((unsigned)time(NULL));
};
double dget(double rmin, double rmax) // получить случайное число
{
    return ((double)rand() / (double)RAND_MAX) * (rmax - rmin) + rmin;
};
int iget(int rmin, int rmax)         // получить случайное число

{
    return (int)dget((double)rmin, (double)rmax);
};
int main()
{
    setlocale(LC_ALL, "rus");

    start();
    int v[NN];
    int c[NN];
    int minv[MM];
    int maxv[MM];
    for (int i = 0; i < NN; i++) {
        v[i] = iget(100, 200);
        c[i] = iget(10, 100);
    }
    for (int i = 0; i < MM; i++) {
        minv[i] = iget(50, 120);
        maxv[i] = iget(150, 850);
    }
    short r[MM];
    clock_t t1, t2;
    t1 = clock();
    int cc = boat_с(
        MM,    // [in]  количество мест для контейнеров
        minv,  // [in]  максимальный вес контейнера на каждом  месте 
        maxv,  // [in]  минимальный вес контейнера на каждом  месте  
        NN,    // [in]  всего контейнеров  
        v,     // [in]  вес каждого контейнера  
        c,     // [in]  доход от перевозки каждого контейнера   
        r      // [out] номера  выбранных контейнеров  
    );
    t2 = clock();
    std::cout << std::endl << (t2 - t1);

    //std::cout << std::endl << "- Задача о размещении контейнеров на судне -";
    //std::cout << std::endl << "- общее количество контейнеров   : " << NN;
    //std::cout << std::endl << "- количество мест для контейнеров  : " << MM;
    //std::cout << std::endl << "- минимальный  вес контейнера  : ";
    //for (int i = 0; i < MM; i++) std::cout << std::setw(3) << minv[i] << " ";
    //std::cout << std::endl << "- максимальный вес контейнера  : ";
    //for (int i = 0; i < MM; i++) std::cout << std::setw(3) << maxv[i] << " ";
    //std::cout << std::endl << "- вес контейнеров      : ";
    //for (int i = 0; i < NN; i++) std::cout << std::setw(3) << v[i] << " ";
    //std::cout << std::endl << "- доход от перевозки     : ";
    //for (int i = 0; i < NN; i++) std::cout << std::setw(3) << c[i] << " ";
    //std::cout << std::endl << "- выбраны контейнеры (0,1,...,m-1) : ";
    //for (int i = 0; i < MM; i++) std::cout << r[i] << " ";
    //std::cout << std::endl << "- доход от перевозки     : " << cc;
    //std::cout << std::endl << std::endl;
    system("pause");
    return 0;
}
