#include <iostream>

int function(int arr[], int n, int result[]) {

    int* lenghths = new int[n];
    int* connections = new int[n];
    int maxLength = 0;
    int maxIndex = -1;

    std::fill_n(lenghths, n, 1);
    std::fill_n(connections, n, -1);

    for (int i = 1; i < n; i++) {
        for (int j = 0; j < i; j++) {
            if (arr[j] < arr[i] && lenghths[j] + 1 > lenghths[i]) {
                lenghths[i] = lenghths[j] + 1;
                connections[i] = j;
            }
        }
        if (lenghths[i] > maxLength) {
            maxLength = lenghths[i];
            maxIndex = i;
        }
    }

    int index = maxLength - 1;
    for (int i = maxIndex; i >= 0; i = connections[i]) {
        result[index--] = arr[i];
        if (connections[i] == -1) break;
    }
    delete[] lenghths;
    delete[] connections;
    return maxLength;
}

int main() {
    setlocale(LC_ALL, "rus");
    int arr[100];
    int n;
    do {
        std::cout << "Введите n элементов в массиве (max: 100): ";
        std::cin >> n;
    } while (n < 0 && n > 100);

    std::cout << "Введите массив: ";
    for (int i = 0; i < n; i++) {
        std::cin >> arr[i];
    }

    int  *result = new int[n];
    int resultLength = function(arr, n, result);

    std::cout << "Длина: " << resultLength << std::endl;
    std::cout << "Возрастающая подпоследовательность: ";
    for (int i = 0; i < resultLength; i++) {
        std::cout << result[i] << ' ';
    }
    std::cout << std::endl;
    delete[] result;

    return 0;
}