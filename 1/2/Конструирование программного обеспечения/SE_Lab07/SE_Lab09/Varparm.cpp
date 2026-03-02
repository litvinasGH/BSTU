#include "varparm.h"
#include <float.h>

namespace Varparm {
	int ivarparm(int n, ...) {
		if (n == 0)
			return 0;
		int pr = 1;
		int* p = &n + 1;
		for (int i = 0; i < n; i++)
			pr *= *(p + i);
		return pr;
	}

	int svarparm(short n, ...) {
		int max = 0;
		int* p = (int*) & n + 1;
		for (int i = 0; i < n; i++) {
			if (*(p + i) > max)
				max = *(p + i);
		}
		return max;
	}

	double fvarparm(float n, ...) {
		if (n == FLT_MAX)
			return 0;
		double sum = (double)n;
		double* ptr = (double*)(&n + 1);
		for (int i = 0; ptr[i] != (double)FLT_MAX; i++) {
			sum += ptr[i];
		}
		return sum;
	}

	double dvarparm(double n, ...) {
		if (n == DBL_MAX)
			return 0;
		double sum = n;
		double* ptr = &n + 1;
		for (int i = 0; ptr[i] != DBL_MAX; i++) {
			sum += ptr[i];
		}
		return sum;
	}
}