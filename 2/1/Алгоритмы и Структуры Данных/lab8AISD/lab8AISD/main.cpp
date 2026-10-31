#include <iostream>
#include <string>
#include <Windows.h>

struct Item {
    std::string name;
    int weight;
    int value;
};

int main() {
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);
    int N; 
    int itemCount; 

    std::cout << "Введите вместимость рюкзака: ";
    std::cin >> N;

    std::cout << "Введите количество предметов: ";
    std::cin >> itemCount;

    Item* items = new Item[itemCount];

    for (int i = 0; i < itemCount; ++i) {
        std::cout << '[' << i + 1 << "]Введите предмет (название вес стоимость): ";
        std::cin >> items[i].name >> items[i].weight >> items[i].value;
    }

    
    int** dp = new int* [itemCount + 1];
    for (int i = 0; i <= itemCount; ++i) {
        dp[i] = new int[N + 1](); 
    }

    
    for (int i = 1; i <= itemCount; ++i) {
        for (int w = 0; w <= N; ++w) {
            if (items[i - 1].weight <= w) {
                dp[i][w] = max(dp[i - 1][w], dp[i - 1][w - items[i - 1].weight] + items[i - 1].value);
            }
            else {
                dp[i][w] = dp[i - 1][w];
            }
        }
    }

    
    int maxValue = dp[itemCount][N];
    std::cout << "Максимальная стоимость предметов в рюкзаке: " << maxValue << std::endl;

    
    std::cout << "Предметы в рюкзаке: " << std::endl;
    int w = N;
    for (int i = itemCount; i > 0 && maxValue > 0; --i) {
        if (maxValue != dp[i - 1][w]) {
            std::cout << items[i - 1].name << " " << items[i - 1].weight << " " << items[i - 1].value << std::endl;
            maxValue -= items[i - 1].value;
            w -= items[i - 1].weight;
        }
    }
    std::cout << std::endl;

    
    for (int i = 0; i <= itemCount; ++i) {
        delete[] dp[i];
    }
    delete[] dp;
    delete[] items;

    return 0;
}
