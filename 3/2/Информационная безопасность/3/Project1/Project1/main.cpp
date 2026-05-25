#include <iostream>
#include <vector>
#include <stdexcept>
#include <map>


int gcd(int a, int b) {
    while (b != 0) {
        int t = b;
        b = a % b;
        a = t;
    }
    return a;
}

int gcd(int a, int b, int c) {
    return gcd(gcd(a, b), c);
}

std::vector<int> sieve_steps(int m, int n) {
    if (m < 1) {
        m = 1;
    }
    if (n < m) {
        throw std::invalid_argument("b must be >= a");
    }

    std::vector<int> numbers;
    for (int i = 2; i <= n; ++i) {
        numbers.push_back(i);
    }


    int s = 2;
    int step = 1;

    while (s * s <= n) {

        std::vector<int> next_numbers;
        std::vector<int> removed;

        for (int x : numbers) {
            if (x > s && x % s == 0) {
                removed.push_back(x);
            }
            else {
                next_numbers.push_back(x);
            }
        }

        numbers = next_numbers;

        int new_s = -1;
        for (int x : numbers) {
            if (x > s) {
                new_s = x;
                break;
            }
        }

        if (new_s == -1) {
            break;
        }

        s = new_s;
        ++step;
    }

    std::vector<int> primes_in_range;
    for (int p : numbers) {
        if (p >= m && p <= n) {
            primes_in_range.push_back(p);
        }
    }

    std::cout << "Primes in [" << m << "; " << n << "]: ";
    for (int p : primes_in_range) {
        std::cout << p << " ";
    }
    std::cout << "\nCount: " << primes_in_range.size() << "\n";

    return primes_in_range;
}

std::map<int, int> factorize(int n) {
    std::map<int, int> factors;

    for (int i = 2; i * i <= n; ++i) {
        while (n % i == 0) {
            factors[i]++;
            n /= i;
        }
    }

    if (n > 1) {
        factors[n]++;
    }

    return factors;
}

void print_factors(int n) {
    std::map<int, int> f = factorize(n);

    std::cout << n << " = ";
    bool first = true;

    for (auto& p : f) {
        if (!first) std::cout << " * ";
        std::cout << p.first;
        if (p.second > 1)
            std::cout << "^" << p.second;
        first = false;
    }

    std::cout << "\n";
}

long long concat(int m, int n) {
    long long temp = n;
    long long pow10 = 1;

    while (temp > 0) {
        pow10 *= 10;
        temp /= 10;
    }

    return (long long)m * pow10 + n;
}

bool isPrime(long long x) {
    if (x < 2) return false;

    for (long long i = 2; i * i <= x; ++i) {
        if (x % i == 0)
            return false;
    }
    return true;
}

int main() {
    int m, n;

    std::cout << "Enter m: ";
    if (!(std::cin >> m)) {
        return 1;
    }

    std::cout << "Enter n: ";
    if (!(std::cin >> n)) {
        return 1;
    }

    try {
        std::cout << "\nGCD(m, n):" << gcd(m, n) << "\n";
		std::cout << "GCD(m, n, m + n):" << gcd(m, n, m + n) << "\n\n";

        sieve_steps(2, n);
        std::cout << "n/ln(n): " << n/std::log(n) << "\n\n";
        sieve_steps(m, n);

        print_factors(m);
		print_factors(n);

        std::cout << "\nm||n(" << concat(m,n) << ") is ";
        if (isPrime(concat(m, n))) {
            std::cout << "prime\n";
        }
        else {
            std::cout << "not prime\n";
		}

    }
    catch (const std::exception& e) {
        std::cout << e.what() << "\n";
        return 1;
    }

    return 0;
}