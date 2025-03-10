#ifndef SALESMAN_H
#define SALESMAN_H

#include <climits>

const int INF = INT_MAX;

int sum(int x1, int x2);  // ������������ � ������ �������������
int* firstpath(int n);    // ������������ ������� �������� 0, 1, 2, ..., n-1, 0
int* source(int n);       // ������������ ��������� ������� 1, 2, ..., n-1
void copypath(int n, int* r1, const int* r2);  // ���������� �������
int distance(int n, int* r, const int* d);     // ����� ��������
void indx(int n, int* r, const int* s, const short* ntx);
int salesman(int n, const int* d, int* r);     // ���������� ������ ������������

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

