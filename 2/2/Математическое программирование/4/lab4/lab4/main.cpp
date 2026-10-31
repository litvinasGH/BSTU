#include <iostream>
#include <string>
#include <cstdlib>
#include <ctime>
#include <vector>
#include <chrono>
#include <unordered_map>
using namespace std;


string generateRandomString(int length) {

    string randomString;
    randomString.reserve(length);

    for (int i = 0; i < length; ++i) {
        randomString += rand() % 57 + 65;
    }

    return randomString;
}

int findmin(int a, int b, int c) {
    return (a < b) ? ((a < c) ? a : c) : ((b < c) ? b : c);
}


int levenshteinDistanceDP(const string& str1, const string& str2, int len1, int len2) {
    vector<vector<int>> dp(len1 + 1, vector<int>(len2 + 1, 0));

    for (int i = 0; i <= len1; ++i) {
        for (int j = 0; j <= len2; ++j) {
            if (i == 0) {
                dp[i][j] = j;
            }
            else if (j == 0) {
                dp[i][j] = i;
            }
            else if (str1[i - 1] == str2[j - 1]) {
                dp[i][j] = dp[i - 1][j - 1];
            }
            else {
                dp[i][j] = 1 + findmin(dp[i][j - 1],
                    dp[i - 1][j], dp[i - 1][j - 1]);
            }
        }
    }
    return dp[len1][len2];
}


unordered_map<string, int> cache;

int levenshteinDistanceRecursive(const string& s1, int len1, const string& s2, int len2) {
    string key = to_string(len1) + "," + to_string(len2);

    if (cache.find(key) != cache.end()) {
        return cache[key];
    }

    if (len1 == 0) return len2;
    if (len2 == 0) return len1;

    int cost = (s1[len1 - 1] == s2[len2 - 1]) ? 0 : 1;

    int result = findmin(
        levenshteinDistanceRecursive(s1, len1 - 1, s2, len2) + 1,
        levenshteinDistanceRecursive(s1, len1, s2, len2 - 1) + 1,
        levenshteinDistanceRecursive(s1, len1 - 1, s2, len2 - 1) + cost
    );

    cache[key] = result;
    return result;
}


int main() {
    setlocale(LC_CTYPE, "Russian");

    srand(time(0));


    string S1 = generateRandomString(300);
    string S2 = generateRandomString(200);


    cout << "Случайная строка S1 (длина 300): " << S1 << endl;
    cout << "Случайная строка S2 (длина 200): " << S2 << endl;

    double buf[] = { 1.0 / 25.0, 1.0 / 20.0, 1.0 / 15.0, 1.0 / 10.0, 1.0 / 5.0, 1.0 / 2.0, 1 };
    for (int i = 0; i < 7; i++)
    {

        double k = buf[i];

        std::cout << "k:" << k << std::endl;

        auto startRecursive = std::chrono::high_resolution_clock::now();
        int distance = levenshteinDistanceRecursive(S1, S1.length() * k, S2, S2.length() * k);
        auto endRecursive = std::chrono::high_resolution_clock::now();
        std::chrono::duration<double, std::milli> elapsedRecursive = endRecursive - startRecursive;
        std::cout << "Расстояние Левенштейна между строками: " << distance << std::endl;
        std::cout << "Время выполнения (рекурсивный метод): " << elapsedRecursive.count() << " мсек" << std::endl;

        auto startDP = std::chrono::high_resolution_clock::now();
        int distanceDyn = levenshteinDistanceDP(S1, S2, S1.length() * k, S2.length() * k);
        auto endDP = std::chrono::high_resolution_clock::now();
        std::chrono::duration<double, std::milli> elapsedDP = endDP - startDP;
        std::cout << "Расстояние Левенштейна между строками: " << distanceDyn << std::endl;
        std::cout << "Время выполнения (динамическое программирование): " << elapsedDP.count() << " мсек" << std::endl;
    }
    return 0;
}
