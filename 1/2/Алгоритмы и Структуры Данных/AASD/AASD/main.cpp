#include <iostream>
#include <chrono>
using namespace std;

int fabanachi_calc(unsigned int number, unsigned int i, unsigned long long fabanchi[]);

void main() {
	unsigned int number;
	cin >> number;

	auto start_time = chrono::high_resolution_clock::now();
	if (number == 1)
		cout << 0 << endl;
	else {
		unsigned long long fabanchi[3] = { 0, 1, 1 };
		for (int i = 2; i <= number - 1; i++) {
			fabanchi[2] = fabanchi[0] + fabanchi[1];
			fabanchi[0] = fabanchi[1];
			fabanchi[1] = fabanchi[2];
		}

		cout << fabanchi[2] << endl;
	}
	auto end_time = chrono::high_resolution_clock::now();
	auto res = std::chrono::duration_cast<std::chrono::nanoseconds>(end_time - start_time).count();
	cout << res / 1000000000 << "sec "
		<< (res % 1000000000) / 1000000 << "msec "
		<< (res % 1000000) / 1000 << "micsec "
		<< res % 1000 << "nanosec" << endl;

	unsigned long long fabanchiSec[3] = {0, 1, 1};
	start_time = chrono::high_resolution_clock::now();
	cout << fabanachi_calc(number, 2, fabanchiSec) << endl;
	end_time = chrono::high_resolution_clock::now();
	res = std::chrono::duration_cast<std::chrono::nanoseconds>(end_time - start_time).count();
	cout << res / 1000000000 << "sec "
		<< (res % 1000000000) / 1000000 << "msec "
		<< (res % 1000000) / 1000 << "micsec "
		<< res % 1000 << "nanosec" << endl;
}

int fabanachi_calc(unsigned int number, unsigned int i, unsigned long long fabanchi[]) {
	if (number == 1)
		return 0;
	unsigned long long answer;
	fabanchi[2] = fabanchi[0] + fabanchi[1];
	fabanchi[0] = fabanchi[1];
	fabanchi[1] = fabanchi[2];
	answer = fabanchi[2];
	i++;
	if (i <= number - 1) {
		answer = fabanachi_calc(number, i, fabanchi);
	}
	return answer;
}