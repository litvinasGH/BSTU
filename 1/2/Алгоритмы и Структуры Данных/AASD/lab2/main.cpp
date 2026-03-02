#include <iostream>
using namespace std;

void main() {
	cout << "Max: ";
	unsigned long long max;
	cin >> max;
	unsigned long long answer = NULL;
	unsigned long long buf = max / 2;
	unsigned long long bufmin = 0, bufmax = max;
	
	char bufC;
	unsigned long long maxSteps = int(log(max) + 1);
	for (unsigned long long i = 0; i <= maxSteps; i++) {
		cout << buf << ' ';
		cin >> bufC;
		if (bufC == '=') {
			answer = buf;
			break;
		}
		else if (bufC == '>') {
			bufmax = buf;
			buf = bufmin + (bufmax - bufmin) / 2;
		}
		else if (bufC == '<') {
			bufmin = buf;
			buf = bufmin + (bufmax - bufmin) / 2;
		}
	}
	if (answer != NULL) {
		cout << "Max steps: " << maxSteps;
	}
	else {
		cout << "You're cheating!";
	}
}