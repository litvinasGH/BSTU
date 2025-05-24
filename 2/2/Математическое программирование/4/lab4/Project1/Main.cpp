#include <iostream>
#include <ctime>
#include <vector>
#include <chrono>
#include "Matrix.h"

int main() {
    setlocale(LC_ALL, "rus");
    std::vector<int> dimensions = { 20, 15, 30, 53, 10, 20, 11 };
    int n = dimensions.size();

    // Измерение времени для рекурсивного решения
    auto startRecursive = std::chrono::high_resolution_clock::now();
    int result_recursive = MatrixChainOrderRecursive(dimensions, 1, n - 1);
    auto endRecursive = std::chrono::high_resolution_clock::now();
    std::chrono::duration<double, std::milli> time_recursive = endRecursive - startRecursive;

    // Измерение времени для решения с использованием динамического программирования
    std::vector<std::vector<int>> s(n, std::vector<int>(n, 0));
    auto startDP = std::chrono::high_resolution_clock::now();
    int result_dp = MatrixChainOrderDP(dimensions, s);
    auto endDP = std::chrono::high_resolution_clock::now();
    std::chrono::duration<double, std::milli> time_dp = endDP - startDP;

    // Вывод результатов
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

    std::cout << "Время выполнения рекурсивного решения: " << time_recursive.count() << " мс" << std::endl;
    std::cout << "Время выполнения решения с использованием динамического программирования: " << time_dp.count() << " мс" << std::endl;

    return 0;
}
