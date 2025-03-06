#include <iostream>
#include <ctime>
#include <vector>
#include "Matrix.h"

int main() {
    setlocale(LC_ALL, "rus");
    std::vector<int> dimensions = { 7, 10, 18, 21, 28, 38, 49 };
    int n = dimensions.size();

    clock_t start_r = clock();
    int result_recursive = MatrixChainOrderRecursive(dimensions, 1, n - 1);
    clock_t end_r = clock();
    double time_recursive = double(end_r - start_r) / CLOCKS_PER_SEC;

    std::vector<std::vector<int>> s(n, std::vector<int>(n, 0));
    clock_t start_dp = clock();
    int result_dp = MatrixChainOrderDP(dimensions, s);
    clock_t end_dp = clock();
    double time_dp = double(end_dp - start_dp) / CLOCKS_PER_SEC;

    std::cout << "-- Расстановка скобок (рекурсивное решение) --" << std::endl;
    std::cout << "Размерности матриц: ";
    for (int i = 0; i < n - 1; ++i) {
        std::cout << "(" << dimensions[i] << "," << dimensions[i + 1] << ") ";
    }
    std::cout << std::endl;
    std::cout << "Минимальное количество операций умножения: " << result_recursive << std::endl;

    std::cout << "-- Расстановка скобок (динамическое программирование) --" << std::endl;
    std::cout << "Размерности матриц: ";
    for (int i = 0; i < n - 1; ++i) {
        std::cout << "(" << dimensions[i] << "," << dimensions[i + 1] << ") ";
    }
    std::cout << std::endl;
    std::cout << "Минимальное количество операций умножения: " << result_dp << std::endl;
    std::cout << "Матрица S: " << std::endl;
    for (int i = 1; i < n - 1; ++i) {
        for (int j = i + 1; j < n; ++j) {
            std::cout << s[i][j] << " ";
        }
        std::cout << std::endl;
    }

    std::cout << "Оптимальный порядок умножения матриц: ";
    PrintOptimalParens(s, 1, n - 1);
    std::cout << std::endl;

    std::cout << "Время выполнения рекурсивного решения: " << time_recursive << " секунд" << std::endl;
    std::cout << "Время выполнения решения с использованием динамического программирования: " << time_dp << " секунд" << std::endl;

    return 0;
}
