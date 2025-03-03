#include "stdafx.h"
#include <iostream>
#include <iomanip> 
#include "Boat.h"
#define MM 8
#define SPACE(n) std::setw(n)<<" "
#define NN 9

void start()                          // �����  ���������� ��. �����
{
    srand((unsigned)time(NULL));
};
double dget(double rmin, double rmax) // �������� ��������� �����
{
    return ((double)rand() / (double)RAND_MAX) * (rmax - rmin) + rmin;
};
int iget(int rmin, int rmax)         // �������� ��������� �����

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
    int cc = boat_�(
        MM,    // [in]  ���������� ���� ��� �����������
        minv,  // [in]  ������������ ��� ���������� �� ������  ����� 
        maxv,  // [in]  ����������� ��� ���������� �� ������  �����  
        NN,    // [in]  ����� �����������  
        v,     // [in]  ��� ������� ����������  
        c,     // [in]  ����� �� ��������� ������� ����������   
        r      // [out] ������  ��������� �����������  
    );
    t2 = clock();
    std::cout << std::endl << (t2 - t1);

    //std::cout << std::endl << "- ������ � ���������� ����������� �� ����� -";
    //std::cout << std::endl << "- ����� ���������� �����������   : " << NN;
    //std::cout << std::endl << "- ���������� ���� ��� �����������  : " << MM;
    //std::cout << std::endl << "- �����������  ��� ����������  : ";
    //for (int i = 0; i < MM; i++) std::cout << std::setw(3) << minv[i] << " ";
    //std::cout << std::endl << "- ������������ ��� ����������  : ";
    //for (int i = 0; i < MM; i++) std::cout << std::setw(3) << maxv[i] << " ";
    //std::cout << std::endl << "- ��� �����������      : ";
    //for (int i = 0; i < NN; i++) std::cout << std::setw(3) << v[i] << " ";
    //std::cout << std::endl << "- ����� �� ���������     : ";
    //for (int i = 0; i < NN; i++) std::cout << std::setw(3) << c[i] << " ";
    //std::cout << std::endl << "- ������� ���������� (0,1,...,m-1) : ";
    //for (int i = 0; i < MM; i++) std::cout << r[i] << " ";
    //std::cout << std::endl << "- ����� �� ���������     : " << cc;
    //std::cout << std::endl << std::endl;
    system("pause");
    return 0;
}
