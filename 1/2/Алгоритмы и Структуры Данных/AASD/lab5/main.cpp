#include <iostream>
#include <string>
#include <stack>  // подключаем библиотеку для использования стека
using namespace std;

struct skob
{
	char open;
	char close;
};

int main() {
	setlocale(LC_ALL, "RUS");
	skob skobs[] =
	{
		{'(', ')'},
		{'{', '}'},
		{'[', ']'}
	};
	string str;
	int i = 0;
	char a;
	getline(cin, str);
	stack <char> steck;
	bool b = true;
	for (int i = 0; i < str.length(); i++) {
		for (int j = 0; j < 3; j++) {
			if (str[i] == skobs[j].open) {
				steck.push(skobs[j].close);
				break;
			}
			else if (str[i] == skobs[j].close) {
				if (!steck.empty()) {
					if (str[i] == steck.top()) {
						steck.pop();
						break;
					}
				}
				b = false;
				break;
			}
		}
	}
	if (b && !steck.empty()) {
		b = false;
	}
	cout << "Скобки раставлены" << (b ? " " : " не ") << "верно" << endl;
	return 0;
}