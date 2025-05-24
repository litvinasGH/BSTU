#include <iostream>
#include "stdafx.h"

using namespace std;

#define N 4 

int dist[5][5] = {
    {INF, 2 * N, 21 + N, INF, N},
    {N, INF, 15 + N, 68 - N, 84 - N},
    {2 + N, 3 * N, INF, 86, 49 + N},
    {17 + N, 58 - N, 4 * N, INF, 3 * N},
    {93 - N, 66 + N, 52, 13 + N, INF}
};

int branchAndBound(int n, vector<int>& path, vector<bool>& visited, int cost, vector<int>& bestPath) {
    if (path.size() == n) {
        if (cost + dist[path.back()][path[0]] < INF) {
            bestPath = path;
            bestPath.push_back(path[0]); 
        }
        return cost + dist[path.back()][path[0]];
    }
    int minCost = INF;
    for (int i = 0; i < n; ++i) {
        if (!visited[i]) {
            visited[i] = true;
            path.push_back(i);
            int newCost = cost + dist[path[path.size() - 2]][i];
            if (newCost < INF) {  
                int result = branchAndBound(n, path, visited, newCost, bestPath);
                if (result < minCost) {
                    minCost = result;
                }
            }
            path.pop_back();
            visited[i] = false;
        }
    }
    return minCost;
}

void printMatrix(int n, int matrix[5][5]) {
    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < n; ++j) {
            if (matrix[i][j] == INF) {
                cout << "INF ";
            }
            else {
                cout << matrix[i][j] << " ";
            }
        }
        cout << endl;
    }
}

int main() {
    setlocale(LC_ALL, "rus");
    cout << "-- Задача коммивояжера --" << endl;
    cout << "-- количество городов: 5" << endl;
    cout << "-- матрица расстояний:" << endl;
    printMatrix(5, dist);

    vector<int> path = { 0 };
    vector<bool> visited(5, false);
    visited[0] = true;
    vector<int> bestPath;
    int result = branchAndBound(5, path, visited, 0, bestPath);

    if (result >= INF) {
        cout << "Путь не найден или ошибка в данных." << endl;
    }
    else {
        cout << "-- оптимальный маршрут: ";
        for (size_t i = 0; i < bestPath.size(); ++i) {
            cout << bestPath[i] + 1;
            if (i < bestPath.size() - 1) {
                cout << "-->";
            }
        }
        cout << endl;
        cout << "-- длина маршрута      : " << result << endl;
    }

    return 0;
}
