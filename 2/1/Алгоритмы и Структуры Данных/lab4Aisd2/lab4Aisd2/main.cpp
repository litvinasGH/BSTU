#include <iostream>
#include <vector>
#include <iomanip>


std::vector<std::vector<int>> D = {
        {0, 28, 21, 59, 12, 27},
        {7, 0, 24, INT32_MAX, 21, 9},
        {9, 32, 0, 13, 11, INT32_MAX},
        {8, INT32_MAX, 5, 0, 16, INT32_MAX},
        {14, 13, 15, 10, 0, 22},
        {15, 18, INT32_MAX, INT32_MAX, 6, 0}
};

std::vector<std::vector<int>> S = {
        {0, 2, 3, 4, 5, 6},
        {1, 0, 3, 4, 5, 6},
        {1, 2, 0, 4, 5, 6},
        {1, 2, 3, 0, 5, 6},
        {1, 2, 3, 4, 0, 6},
        {1, 2, 3, 4, 5, 0}
};


void Floyd_Warshall() {
    for (int m = 0; m < 6; m++) {
        for (int i = 0; i < 6; i++) {
            for (int j = 0; j < 6; j++) {
                if (D[i][j] > (D[i][m] + D[m][j])) {
                    D[i][j] = (D[i][m] + D[m][j]);
                    S[i][j] = m + 1;
                }
            }
        }
    }
}

void main() {
    Floyd_Warshall();
    for (int i = 0; i < 6; i++) {
        for (int j = 0; j < 6; j++)
        {
            std::cout << std::setw(2) << D[i][j] << ' ';
        }
        std::cout << std::endl;
    }
    std::cout << std::endl; std::cout << std::endl;
    for (int i = 0; i < 6; i++) {
        for (int j = 0; j < 6; j++)
        {
            std::cout << std::setw(2) << S[i][j] << ' ';
        }
        std::cout << std::endl;
    }
}