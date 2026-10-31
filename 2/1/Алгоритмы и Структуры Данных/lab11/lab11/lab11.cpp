#include <iostream>
#include <vector>
#include <algorithm>
#include <random>

bool random_choice(const std::vector<int>& boxes, int prisoner) {
    int attempts = 50;
    std::mt19937 gen(std::random_device{}());
    std::uniform_int_distribution<> random(0, 99);
    std::vector<int> isopen;
    int box;
    for (int i = 0; i < attempts; ++i) {
        box = random(gen);
        if (std::find(isopen.begin(), isopen.end(), box) != isopen.end()) {
            i--;
            continue;
        }
        if (boxes[box] == prisoner) {
            return true;
        }
        isopen.push_back(box);
    }
    return false;
}

bool loop_following(const std::vector<int>& boxes, int prisoner) {
    int attempts = 50;
    int next_box = prisoner;
    for (int i = 0; i < attempts; ++i) {
        if (boxes[next_box - 1] == prisoner) {
            return true;
        }
        next_box = boxes[next_box - 1];
    }
    return false;
}



int main() {
    setlocale(LC_ALL, "RUS");
    int rounds;
    std::cout << "Кол-во раундов: ";
    while (std::cin >> rounds && !(rounds > 0));

    int random_success = 0;
    int loop_success = 0;

    for (int i = 0; i < rounds; ++i) {
        
        std::vector<int> boxes(100);
        for (int j = 0; j < 100; ++j) {
            boxes[j] = j + 1;
        }
        std::shuffle(boxes.begin(), boxes.end(), std::mt19937{ std::random_device{}() });

        
        bool random_all_success = true;
        for (int prisoner = 1; prisoner <= 100; ++prisoner) {
            if (!random_choice(boxes, prisoner)) {
                random_all_success = false;
                break;
            }
        }
        if (random_all_success) {
            ++random_success;
        }

        
        bool loop_all_success = true;
        for (int prisoner = 1; prisoner <= 100; ++prisoner) {
            if (!loop_following(boxes, prisoner)) {
                loop_all_success = false;
                break;
            }
        }
        if (loop_all_success) {
            ++loop_success;
        }


        //std::cout << "Итерация " << i + 1 << ", рандом " << (random_all_success ? "success, " : "failure, ");
        //std::cout << "по выподанию " << (loop_all_success ? "success\n" : "failure\n");
    }

    std::cout << "\n\nРандом successes: " << random_success << "/" << rounds << ", " << ((float)random_success/(float)rounds) * 100 << "%" << std::endl;
    std::cout << "По выподанию successes: " << loop_success << "/" << rounds << ", " << ((float)loop_success / (float)rounds) * 100 << "%" << std::endl;

    return 0;
}