#include <iostream>
#include <stack>


void function(int q, int from, int to, int buf) {
    if (q == 0) {
        return;
    }
    else {
        function(q - 1, from, buf, to);
        std::cout << "Переместить диск " << q << " с " << from << " на " << to << " стержень" << std::endl;
        function(q - 1, buf, to, from);
    }
}

int main()
{
    setlocale(LC_ALL, "RUS");
    int N, k, i;
    do {
        std::cout << "N(колличество дисков), i(начальный шест), k(конечный шест): ";
        std::cin >> N >> i >> k;
    } while (!(N > 3 && k != i && k <= 3 && k >= 1 && i <= 3 && i >= 1));
    int b = 6 - k - i;
    function(N, i, k, b);
}
